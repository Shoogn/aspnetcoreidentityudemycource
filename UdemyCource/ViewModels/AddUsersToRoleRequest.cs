using Microsoft.Build.Framework;
using System.Collections.Generic;

namespace UdemyCource.ViewModels
{
    public class AddUsersToRoleRequest
    {
        /// <summary>
        /// Role Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// List Of Users
        /// </summary>
        public List<AllUsers> Users { get; set; }
    }
}
