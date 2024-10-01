﻿using Castle.DynamicProxy;
using Core.Extensions;
using Core.Helpers.Interceptors;
using Core.Helpers.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>()!;
           
        }
      
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor?.HttpContext?.User.ClaimRoles();
            foreach (var roleClaim in _roles)
            {
                if (roleClaims!.Contains(roleClaim))
                {
                    return;
                }

            }
            Exception exception = new("Duxunuz catmir");
            throw exception;
        }
    }
}
