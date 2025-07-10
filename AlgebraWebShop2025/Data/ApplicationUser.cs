using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AlgebraWebShop2025.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? FirstName { get; set; }
        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(20)]
        public string? ZIP { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        //TODO: dodati FK-e (Coolection) na tablicu narudžba kada bude napravljena!
    }
}
