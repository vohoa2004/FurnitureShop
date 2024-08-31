using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace FurnitureShop.DAL.Entities
{
    [Index(nameof(Account.PhoneNo), IsUnique = true)]
    [Index(nameof(Account.Email), IsUnique = true)]
    [Index(nameof(Account.UserName), IsUnique = true)]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? FullName { get; set; }

        [AllowNull]
        public string? PhoneNo { get; set; } // unique

        [Required]
        public required string Email { get; set; } // unique

        public DateTime CreatedAt { get; set; }

        [Required]
        public required string UserName { get; set; } // unique

        [Required]
        public required string Password { get; set; }

        [AllowNull]
        public string? Address { get; set; }

        [AllowNull]
        public DateOnly? BirthDay { get; set; }

        [AllowNull]
        public string? Avatar { get; set; }

        [Required]
        public AccountStatus Status { get; set; }

        public virtual ICollection<Address> ShippingAddress { get; set; } = new HashSet<Address>();

        public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>(); // n-n relationship
    }


    [PrimaryKey(nameof(AccountId), nameof(RoleId))]
    public class AccountRole
    {
        [ForeignKey(nameof(Account.Id))]
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; }
    
        [ForeignKey (nameof(Role.Id))]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }


    public enum AccountStatus
    {
        Active = 1,
        Inactive = 0
    }
}
