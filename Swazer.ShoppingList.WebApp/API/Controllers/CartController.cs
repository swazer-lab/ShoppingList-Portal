﻿using Swazer.ShoppingList.Core;
using Swazer.ShoppingList.Domain;
using Swazer.ShoppingList.WebApp.API.Models;
using Swazer.ShoppingList.WebApp.Infrastructure;
using Swazer.ShoppingList.WebApp.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;

namespace Swazer.ShoppingList.WebApp.API.Controllers
{
    [RoutePrefix("api/cart")]
    [AllowApiUser]
    public class CartController : BaseApiController
    {
        [Route("newFetch")]
        [HttpGet]
        public IHttpActionResult FetchCard([FromUri]CartSearchCriteriaBindingModel model)
        {
            User user = GetCurrentUser();

            CartMobileSearchCriteria cartMobileSearchCriteria = model.ToSearchCriteria(user.Id);

            QueryResult<CartObject> carts = CartMobileService.Obj.FindNew(cartMobileSearchCriteria);

            var result = new PagingBindingModel<CartIndexBindingModel>()
            {
                Items = carts.Items.Select(x => x.ToCartObjectIndexBindingModel()).ToList(),
                TotalCount = carts.TotalCount
            };

            return Ok(result);
        }

        [Route("fetch")]
        [HttpGet]
        public IHttpActionResult FetchCards([FromUri]CartSearchCriteriaBindingModel model)
        {
            User user = GetCurrentUser();

            CartMobileSearchCriteria cartMobileSearchCriteria = model.ToSearchCriteria(user.Id);

            IQueryResult<Cart> carts = CartMobileService.Obj.Find(cartMobileSearchCriteria);

            var result = new PagingBindingModel<CartIndexBindingModel>()
            {
                Items = carts.Items.Select(x => x.ToCartIndexBindingModel()).ToList(),
                TotalCount = carts.TotalCount
            };

            foreach (var cart in result.Items.ToList())
            {
                cart.Items = ItemMobileService.Obj.GetItemsByCard(cart.Cart.CartId).Select(x => x.ToCartItemBindingModel(ItemMobileService.Obj.GetById(x.ItemId))).ToList();
                cart.Users = CartOwnerMobileService.Obj.GetUsersByCart(cart.Cart.CartId).Select(x => x.ToUserProfileBindingModel(UserService.Obj.FindById(x.UserId), ImageService.Obj.FindByUserId(x.UserId))).ToList();
            }

            return Ok(result);
        }

        [Route("getCarts")]
        [HttpGet]
        public IHttpActionResult GetCarts(string title = "")
        {
            User user = GetCurrentUser();

            List<Cart> carts = CartMobileService.Obj.Find(title, user.Id);

            var result = new PagingBindingModel<CartIndexBindingModel>()
            {
                Items = carts.Select(x => x.ToCartIndexBindingModel()).ToList(),
                TotalCount = 3
            };

            foreach (var cart in result.Items.ToList())
            {
                cart.Items = ItemMobileService.Obj.GetItemsByCard(cart.Cart.CartId).Select(x => x.ToCartItemBindingModel(ItemMobileService.Obj.GetById(x.ItemId))).ToList();
                cart.Users = CartOwnerMobileService.Obj.GetUsersByCart(cart.Cart.CartId).Select(x => x.ToUserProfileBindingModel(UserService.Obj.FindById(x.UserId), ImageService.Obj.FindByUserId(x.UserId))).ToList();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateCard(CreateCartBindingModel model)
        {
            User user = GetCurrentUser();

            Cart cart = null;

            if (!model.CartId.HasValue)
            {
                cart = Cart.Create(model.Title, model.Notes, model.Date);

                List<CartItem> items = model.Items?.Select(x => CartItem.Create(cart, Item.Create(x.Title, user), x.Status.Value)).ToList();

                cart = CartMobileService.Obj.Create(cart, user, items);

                // Make Sharing

                if (model.Users?.Count != 0 && model.Users != null)
                {
                    string userCode = UserCodeOperation.ProduceCode(cart.CartId, (int)AccessLevel.ReadWrite);

                    List<string> emails = model.Users.Select(x => x.Email).ToList();

                    User userToFind = null;

                    foreach (var email in emails)
                    {
                        if (email != user.Email)
                        {
                            userToFind = UserService.Obj.FindByEmail(email);

                            if (userToFind != null)
                            {
                                Cart cartToFind = CartMobileService.Obj.GetById(cart.CartId);

                                CartOwner cartOwner = CartOwner.Create(cartToFind, userToFind, AccessLevel.ReadWrite);

                                CartOwnerMobileService.Obj.Create(cartOwner);

                                await UserService.Obj.SendEmailToShareCard(userToFind.Email, userCode);
                            }

                            else
                            {
                                await UserService.Obj.SendEmailToShareCard(email, userCode);
                            }
                        }
                    }
                }
            }

            else
            {
                cart = CartMobileService.Obj.GetById(model.CartId.Value);
                cart.Update(model.Title, model.Notes, model.Date);

                List<CartOwner> users = new List<CartOwner>();

                if (model.Users != null)
                    users = model.Users.Select(x => CartOwner.Create(cart, UserService.Obj.FindById(x.UserId), x.AccessLevel)).ToList();

                List<CartItem> items = model.Items?.Select(x => CartItem.Create(cart, Item.Create(x.Title, user), x.Status.Value)).ToList();

                users.Add(CartOwner.Create(cart, user, AccessLevel.Owner));

                CartMobileService.Obj.Update(cart, items, users);
            }

            CartIndexBindingModel bindingModel = getCartIndexBindingModel(cart);

            return Ok(bindingModel);
        }

        [HttpPost]
        [Route("remove")]
        public IHttpActionResult RemoveCard(int cartId)
        {
            User user = GetCurrentUser();

            CartOwner cartOwner = CartOwnerMobileService.Obj.GetCartUser(cartId, user.Id);

            if (cartOwner.AccessLevel == AccessLevel.Owner)
                CartMobileService.Obj.Delete(cartId);

            else
                CartOwnerMobileService.Obj.LeaveFromCurrentCart(cartOwner.CartId);

            return Ok();
        }

        [HttpPost]
        [Route("updateOrder")]
        public IHttpActionResult UpdateOrder(UpdateOrderBindingModel model)
        {
            User user = GetCurrentUser();

            CartMobileService.Obj.UpdateOrder(user.Id, model.CartId, model.Destination);

            return Ok();
        }

        [HttpPost]
        [Route("makeArchived")]
        public IHttpActionResult MakeArchived(MakeArchivedBindingModel model)
        {
            Cart cart = CartMobileService.Obj.GetById(model.CartId);

            cart.MakeArchived();
            CartMobileService.Obj.Update(cart);

            return Ok();
        }

        [HttpPost]
        [Route("revokeArchived")]
        public IHttpActionResult RevokeArchived(MakeArchivedBindingModel model)
        {
            Cart cart = CartMobileService.Obj.GetById(model.CartId);

            cart.RevokeArchived();
            CartMobileService.Obj.Update(cart);

            return Ok();
        }

        [HttpGet]
        [Route("getArchivedCarts")]
        public IHttpActionResult GetArchivedCarts([FromUri]CartSearchCriteriaBindingModel model)
        {
            User user = GetCurrentUser();

            CartMobileSearchCriteria cartMobileSearchCriteria = model.ToSearchCriteria(user.Id);

            IQueryResult<Cart> carts = CartMobileService.Obj.FindArchivedCarts(cartMobileSearchCriteria);

            var result = new PagingBindingModel<CartIndexBindingModel>()
            {
                Items = carts.Items.Select(x => x.ToCartIndexBindingModel()).ToList(),
                TotalCount = carts.TotalCount
            };

            foreach (var cart in result.Items.ToList())
            {
                cart.Items = ItemMobileService.Obj.GetItemsByCard(cart.Cart.CartId).Select(x => x.ToCartItemBindingModel(ItemMobileService.Obj.GetById(x.ItemId))).ToList();
                cart.Users = CartOwnerMobileService.Obj.GetUsersByCart(cart.Cart.CartId).Select(x => x.ToUserProfileBindingModel(UserService.Obj.FindById(x.UserId), ImageService.Obj.FindByUserId(x.UserId))).ToList();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("shareCart")]
        public async Task<IHttpActionResult> MakeSharing(ShareBindingModel model)
        {
            string userCode = UserCodeOperation.ProduceCode(model.CartId, (int)model.AccessLevel);

            foreach (var email in model.Emails)
            {
                User user = UserService.Obj.FindByEmail(email);

                if (user != null)
                {
                    Cart cart = CartMobileService.Obj.GetById(model.CartId);

                    CartOwner cartOwner = CartOwner.Create(cart, user, AccessLevel.ReadWrite);

                    CartOwnerMobileService.Obj.Create(cartOwner);

                    await UserService.Obj.SendEmailToShareCard(user.Email, userCode);
                }

                else
                {
                    await UserService.Obj.SendEmailToShareCard(email, userCode);
                }
            }

            var users = CartOwnerMobileService.Obj.GetUsersByCart(model.CartId).Select(x => x.ToUserProfileBindingModel(UserService.Obj.FindById(x.UserId), ImageService.Obj.FindByUserId(x.UserId))).ToList();

            return Ok(users);
        }

        [HttpPost]
        [Route("getAccess")]
        public IHttpActionResult GetAccess([FromBody]GetAccessBindingModel model)
        {
            int[] parameters = UserCodeOperation.DecodeCode(model.Id);
            int cartId = parameters[0];
            AccessLevel accessLevel = (AccessLevel)parameters[1];

            User user = GetCurrentUser();

            FriendService.Obj.CreateFriends(user.Id, cartId);

            Cart cart = CartMobileService.Obj.GetById(cartId);

            CartOwner currentCartUser = CartOwnerMobileService.Obj.GetCartUser(cartId, user.Id);

            if (currentCartUser != null)
                if (currentCartUser.AccessLevel == AccessLevel.Owner)
                {
                    CartIndexBindingModel earlyBindingModel = getCartIndexBindingModel(cart);

                    return Ok(earlyBindingModel);
                }

            CartOwner cartOwner = CartOwner.Create(cart, user, accessLevel);

            double cartIndex = CartOwnerMobileService.Obj.GetLastIndex(user.Id);
            cartOwner.SetCartIndex(cartIndex);

            CartOwnerMobileService.Obj.Create(cartOwner);

            CartIndexBindingModel bindingModel = getCartIndexBindingModel(cart);

            return Ok(bindingModel);
        }

        private CartIndexBindingModel getCartIndexBindingModel(Cart cart)
        {
            var bindingModel = cart.ToCartIndexBindingModel();

            bindingModel.Items = ItemMobileService.Obj.GetItemsByCard(bindingModel.Cart.CartId).Select(x => x.ToCartItemBindingModel(ItemMobileService.Obj.GetById(x.ItemId))).ToList();
            bindingModel.Users = CartOwnerMobileService.Obj.GetUsersByCart(bindingModel.Cart.CartId).Select(x => x.ToUserProfileBindingModel(UserService.Obj.FindById(x.UserId), ImageService.Obj.FindByUserId(x.UserId))).ToList();

            return bindingModel;
        }
    }
}
