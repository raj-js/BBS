using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EDoc2.Identity.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string NickName { get; set; }

        [MaxLength(16)]
        public string City { get; set; }

        [MaxLength(256)]
        public string Signature { get; set; }
    }
}
