﻿using Swazer.ShoppingList.Core;
using Swazer.ShoppingList.Domain;
using Swazer.ShoppingList.WebApp.API.Models;
using Swazer.ShoppingList.WebApp.Infrastructure;
using Swazer.ShoppingList.WebApp.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Swazer.ShoppingList.WebApp.API.Controllers
{
    [RoutePrefix("api/cart")]
    [AllowApiUser]
    public class CartController : BaseApiController
    {
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
                cart.Items = ItemMobileService.Obj.GetItemsByCard(cart.Cart.CartId).Select(x => x.ToCartItemBindingModel(ItemService.Obj.GetById(x.ItemId))).ToList();
                cart.Users = CartService.Obj.GetUsersByCart(cart.Cart.CartId).Select(x => x.ToUserProfileBindingModel(UserService.Obj.FindById(x.UserId))).ToList();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateCard(CreateCartBindingModel model)
        {
            User user = GetCurrentUser();

            Cart cart = null;

            if (!model.CartId.HasValue)
            {
                cart = Cart.Create(model.Title, model.Notes, model.Date);

                List<CartItem> items = model.Items?.Select(x => CartItem.Create(cart, Item.Create(x.Title), x.Status)).ToList();

                cart = CartMobileService.Obj.Create(cart, user, items);
            }

            else
            {
                cart = CartMobileService.Obj.GetById(model.CartId.Value);
                cart.Update(model.Title, model.Notes, model.Date);

                List<CartItem> items = model.Items?.Select(x => CartItem.Create(cart, Item.Create(x.Title), x.Status)).ToList();

                CartMobileService.Obj.Update(cart, items);
            }

            CartIndexBindingModel bindingModel = cart.ToCartIndexBindingModel();

            bindingModel.Items = ItemMobileService.Obj.GetItemsByCard(bindingModel.Cart.CartId).Select(x => x.ToCartItemBindingModel(ItemService.Obj.GetById(x.ItemId))).ToList();
            bindingModel.Users = CartService.Obj.GetUsersByCart(bindingModel.Cart.CartId).Select(x => x.ToUserProfileBindingModel(UserService.Obj.FindById(x.UserId))).ToList();

            return Ok(bindingModel);
        }

        [HttpPost]
        [Route("remove")]
        public IHttpActionResult RemoveCard(int cartId)
        {
            CartMobileService.Obj.Delete(cartId);

            return Ok();
        }

        [Route("generateShareUrl")]
        [HttpPost]
        public IHttpActionResult GenerateShareUrl(CartGenerateShareBindingModel model)
        {
            string codedUrl = UserCodeOperation.ProduceCode(new int[] { model.CartId, (int)model.AccessLevel });

            string fullUrl = $"http://localhost:63493/Cart/GetAccess/" + $"{codedUrl}";

            return Ok(fullUrl);
        }

        [HttpGet]
        [Route("getAccess")]
        public IHttpActionResult GetAccess(string encodedUrl)
        {
            int[] parameters = UserCodeOperation.DecodeCode(encodedUrl);
            int cartId = parameters[0];
            AccessLevel accessLevel = (AccessLevel)parameters[1];

            User user = GetCurrentUser();

            Cart cart = CartService.Obj.GetById(parameters[0]);

            CartOwner cartOwner = CartOwner.Create(cart, user, accessLevel);

            CartService.Obj.Create(cartOwner);

            CartBindingModel cartBindingModel = cart.ToCartBindingModel();

            return Ok(cartBindingModel);
        }
    }
}
