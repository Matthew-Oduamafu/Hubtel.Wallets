#nullable disable

namespace Hubtel.Wallets.Domain.Models
{
    public partial class VwUserRolesAndClaim
    {
        public string TblUsersUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string TblRolesId { get; set; }
        public string RoleName { get; set; }
        public string RoleNormalizedName { get; set; }
    }
}