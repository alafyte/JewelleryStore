using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelleryStore;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "ErrorName")]
    [StringLength(45, MinimumLength = 2, ErrorMessage = "ErrorName")]
    public string GivenName { get; set; } = null!;

    [Required(ErrorMessage = "ErrorLastName")]
    [StringLength(45, MinimumLength = 2, ErrorMessage = "ErrorLastName")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "ErrorLogin")]
    [StringLength(45, MinimumLength = 3, ErrorMessage = "ErrorLogin")]
    public string NickName { get; set; } = null!;

    [Required]
    public string UserPasswordSalt { get; set; } = null!;

    [Required]
    public string UserPasswordHash { get; set; } = null!;

    [Required(ErrorMessage = "ErrorEmail")]
    [EmailAddress(ErrorMessage = "ErrorEmail")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "date")]
    [DateOfBirth(ErrorMessage = "ErrorDateBirth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(15)]
    public string Access { get; set; } = null!;

    public bool IsActive { get; set; }

    [Required]
    [StringLength(45)]
    public string Theme { get; set; } = null!;

    public virtual Basket? Basket { get; set; }

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();

    public virtual Favorite? Favorite { get; set; }
}
