﻿using AspNet.Security.OAuth.GitHub;
using Freelance.Application.Auth.Commands.AuthenticateUserOAuth;
using Freelance.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Security.Claims;

namespace Freelance.WebApi.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller {

        [HttpGet("/")]
        public IActionResult Index() {
            return View();
        }
        [HttpGet("/order/details/{orderId}")]
        public IActionResult DetailsOrder(string orderId) {
            ViewData["orderId"] = orderId;
            return View();
        }




        [HttpGet("profile-git")] // пока не нашел применения данному методу
        public IActionResult GetUserProfile() {
            // Получение текущего пользователя из контекста HTTP
            var user = HttpContext.User;

            // Проверка, что пользователь аутентифицирован
            if (user.Identity.IsAuthenticated) {
                // Получение идентификационных данных пользователя GitHub из Claims
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = user.FindFirst(ClaimTypes.Name)?.Value;
                var userLogin = user.FindFirst("urn:github:login")?.Value;
                var userProfileUrl = user.FindFirst("urn:github:url")?.Value;
                var userAvatarUrl = user.FindFirst("urn:github:avatar")?.Value;

                // Возвращаем данные о пользователе
                return Ok(new {
                    UserId = userId,
                    UserName = userName,
                    UserLogin = userLogin,
                    UserProfileUrl = userProfileUrl,
                    UserAvatarUrl = userAvatarUrl
                });
            }

            // Возвращаем ошибку, если пользователь не аутентифицирован
            return Unauthorized();
        }
    }
}
