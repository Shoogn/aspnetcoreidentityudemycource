using System.Collections.Generic;

namespace UdemyCource.ViewModels
{
    public class AllUsers
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public List<RoleModel> Roles { get; set; } = new();

        /// <summary>
        /// This Property indicate ...
        /// if the user is selected to add it to a spesific role
        /// </summary>
        public bool IsSelectedToAddToRole { get; set; }
    }
}
