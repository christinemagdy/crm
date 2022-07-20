using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Bebrand.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Bebrand.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public string GetUserId()
        {
            return _accessor.HttpContext.User.Claims
                       .First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        }
        public string GetParentUserId()
        {
            return _accessor.HttpContext.User.Claims.First(x => x.Type == "ParentUserId").Value;
        }
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
