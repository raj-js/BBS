using System.ComponentModel.DataAnnotations;

namespace EDoc2.FAQ.Api.Models.Accounts
{
    public class VmRegister
    {
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Nickname { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }
    }
}
