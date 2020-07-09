﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Uebungsprojekt.Models
{
    public class UserManager
    {
        private object user_dao_impl;
        public UserManager(object user_dao_impl)
        {
            this.user_dao_impl = user_dao_impl;
        }

        public async void SignIn(HttpContext httpContext, User user)
        {
        
            // User matching_user = user_dao_impl.GetDao().GetUserByEmail(user.email); // TODO: 1. Replace line when UserDaoImpl is implemented; 2. Add GetUserByEmail to diagramm
            User matching_user = new User()
            {
                email = "radi.achkik@gmail.com",
                id = 2,
                name = "Radi Achkik",
                password = "asdf",
                role = Role.Employee
            };
        
            if (matching_user.email != user.email || matching_user.password != user.password)
            {
                return;
            }
        
            ClaimsIdentity identity = new ClaimsIdentity(GetUserClaims(matching_user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        public string GetUserIdByHttpContext(HttpContext httpContext)
        {
            var user_id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            
            return user_id != null ? user_id.Value : "";
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.email));
            claims.AddRange(GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(User user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.role.ToString()));
            return claims;
        }
    }
}