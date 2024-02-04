using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelleryStore;

public partial class Product : ICloneable
{
    [Key]
    public int ProductCode { get; set; }

    [Required(ErrorMessage = "ErrorProductName")]
    [Range(0, 100000000, ErrorMessage = "ErrorProductName")]
    public int Pname { get; set; }

    [Required(ErrorMessage = "ErrorMetal")]
    [Range(1, 100, ErrorMessage = "ErrorMetal")]
    public int Metal { get; set; }

    [Required(ErrorMessage = "ErrorMetalSample")]
    [Range(325, 999, ErrorMessage = "ErrorMetalSample")]
    public int MetalSample { get; set; }

    [Required(ErrorMessage = "ErrorProductPrice")]
    [Range(1, 9999.99, ErrorMessage = "ErrorProductPrice")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "ErrorStoneInsert")]
    [Range(1, 10, ErrorMessage = "ErrorStoneInsert")]
    public int StoneInsert { get; set; }

    [Required(ErrorMessage = "ErrorProductWeight")]
    [Range(0.01, 9.99, ErrorMessage = "ErrorProductWeight")]
    public decimal Pweight { get; set; }

    [Required(ErrorMessage = "ErrorProductQuantity")]
    [Range(0, 100000, ErrorMessage = "ErrorProductQuantity")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "ErrorProductImage")]
    [StringLength(150, ErrorMessage = "ErrorProductImage")]
    public string Pimage { get; set; }

    [StringLength(400, ErrorMessage = "ErrorProductDescription")]
    public string? PdescriptionEn { get; set; }

    [StringLength(400, ErrorMessage = "ErrorProductDescription")]
    public string? PdescriptionRus { get; set; }

    public bool IsActive { get; set; }

    [Required(ErrorMessage = "ErrorProductType")]
    [Range(1, 4, ErrorMessage = "ErrorProductType")]
    public int ProductType { get; set; }

    public virtual Metal MetalNavigation { get; set; } = null!;

    public virtual ICollection<OrderInfo> OrderInfos { get; } = new List<OrderInfo>();

    public virtual Dictionary PnameNavigation { get; set; } = null!;

    public virtual ProductType ProductTypeNavigation { get; set; } = null!;

    public virtual Stone StoneInsertNavigation { get; set; } = null!;

    public virtual ICollection<Basket> Baskets { get; } = new List<Basket>();

    public virtual ICollection<Favorite> Favorites { get; } = new List<Favorite>();

    public Product() { }
    public Product(int productCode, int pname, int metal, int metalSample, decimal price, int stoneInsert, decimal pweight, int quantity, string? pimage, string? pdescriptionEn, string? pdescriptionRus, bool isActive, int productType)
    {
        ProductCode = productCode;
        Pname = pname;
        Metal = metal;
        MetalSample = metalSample;
        Price = price;
        StoneInsert = stoneInsert;
        Pweight = pweight;
        Quantity = quantity;
        Pimage = pimage;
        PdescriptionEn = pdescriptionEn;
        PdescriptionRus = pdescriptionRus;
        IsActive = isActive;
        ProductType = productType;
    }

    public object Clone()
    {
        return new Product(ProductCode, Pname, Metal, MetalSample, Price, StoneInsert, Pweight, Quantity, Pimage, PdescriptionEn, PdescriptionRus, IsActive, ProductType);
    }
}
