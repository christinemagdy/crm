using Microsoft.AspNetCore.Identity;
using Bebrand.Domain.Enums;
using System;

namespace Bebrand.Infra.CrossCutting.Identity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Status Status { get; set; }
        public string FullName { get; set; }
        public Guid ParentUserId { get; set; }
    }
}
