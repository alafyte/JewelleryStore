using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryStore;

public partial class Dictionary
{
    [Key]
    public int WordId { get; set; }

    [Required(ErrorMessage = "ErrorProductName")]
    [StringLength(45, ErrorMessage = "ErrorProductName", MinimumLength = 2)]
    public string WordRus { get; set; } = null!;

    [Required(ErrorMessage = "ErrorProductName")]
    [StringLength(45, ErrorMessage = "ErrorProductName", MinimumLength = 2)]
    public string WordEn { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
