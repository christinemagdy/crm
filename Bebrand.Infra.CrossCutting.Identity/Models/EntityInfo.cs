//using Bebrand.Domain.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bebrand.Infra.CrossCutting.Identity.Models
//{
//    [Authorize]
//    public class EntityInfo
//    {
//        //private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IUser _user;
//        public EntityInfo()
//        {
//            this.CreatedBy = _user.GetUserId();
//        }

//        public DateTime CreatedOn { get; set; }
//        public string CreatedBy { get; set; }
//    }
//}
