using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JewelleryStore;

public partial class StoreDb : DbContext
{
    public StoreDb()
    {
        Database.EnsureCreated();
    }

    public StoreDb(DbContextOptions<StoreDb> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Dictionary> Dictionary { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Metal> Metals { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Stone> Stones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-MFP40AR;Database=jew_store_db;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Basket>(entity =>
        {
            entity.ToTable("BASKET");

            entity.HasIndex(e => e.UserId, "U_UserId").IsUnique();

            entity.HasOne(d => d.User).WithOne(p => p.Basket)
                .HasForeignKey<Basket>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BASKET_UserId");

            entity.HasMany(d => d.Products).WithMany(p => p.Baskets)
                .UsingEntity<Dictionary<string, object>>(
                    "BasketList",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BASKET_LIST_ProductId"),
                    l => l.HasOne<Basket>().WithMany()
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BASKET_LIST_BasketId"),
                    j =>
                    {
                        j.HasKey("BasketId", "ProductId");
                        j.ToTable("BASKET_LIST");
                    });
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.ToTable("BILL");

            entity.Property(e => e.DateOfOrder).HasPrecision(0);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Bills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_UserId");
        });

        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.WordId);

            entity.ToTable("DICTIONARY");

            entity.Property(e => e.WordEn)
                .HasMaxLength(45)
                .IsUnicode(true);
            entity.Property(e => e.WordRus)
                .HasMaxLength(45)
                .IsUnicode(true);
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoritesId);

            entity.ToTable("FAVORITES");

            entity.HasIndex(e => e.UserId, "U_Favorites_UserId").IsUnique();

            entity.HasOne(d => d.User).WithOne(p => p.Favorite)
                .HasForeignKey<Favorite>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FAVORITES_UserId");

            entity.HasMany(d => d.Products).WithMany(p => p.Favorites)
                .UsingEntity<Dictionary<string, object>>(
                    "FavoritesList",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FAVORITES_LIST_ProductId"),
                    l => l.HasOne<Favorite>().WithMany()
                        .HasForeignKey("FavoritesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FAVORITES_LIST_FavoritesId"),
                    j =>
                    {
                        j.HasKey("FavoritesId", "ProductId");
                        j.ToTable("FAVORITES_LIST");
                    });
        });

        modelBuilder.Entity<Metal>(entity =>
        {
            entity.ToTable("METALS");

            entity.Property(e => e.MetalNameEn)
                .HasMaxLength(45)
                .IsUnicode(true);
            entity.Property(e => e.MetalNameRus)
                .HasMaxLength(45)
                .IsUnicode(true);
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => new { e.ProductCode, e.BillId });

            entity.ToTable("ORDER_INFO");

            entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.Bill).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_INFO_BillId");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_INFO_ProductCode");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductCode);

            entity.ToTable("PRODUCT");

            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.PdescriptionEn)
                .HasMaxLength(300)
                .IsUnicode(true)
                .HasColumnName("PDescriptionEn");
            entity.Property(e => e.PdescriptionRus)
                .HasMaxLength(300)
                .IsUnicode(true)
                .HasColumnName("PDescriptionRus");
            entity.Property(e => e.Pimage)
                .HasMaxLength(150)
                .IsUnicode(true)
                .HasColumnName("PImage");
            entity.Property(e => e.Pname).HasColumnName("PName");
            entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Pweight)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("PWeight");

            entity.HasOne(d => d.MetalNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Metal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_Metal");

            entity.HasOne(d => d.PnameNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Pname)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_PName");

            entity.HasOne(d => d.ProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_ProductType");

            entity.HasOne(d => d.StoneInsertNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.StoneInsert)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RODUCT_StoneInsert");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.ToTable("PRODUCT_TYPE");

            entity.Property(e => e.Ptype)
                .HasMaxLength(45)
                .IsUnicode(true)
                .HasColumnName("PType");
        });

        modelBuilder.Entity<Stone>(entity =>
        {
            entity.ToTable("STONES");

            entity.Property(e => e.StoneNameEn)
                .HasMaxLength(45)
                .IsUnicode(true);
            entity.Property(e => e.StoneNameRus)
                .HasMaxLength(45)
                .IsUnicode(true);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USERS");

            entity.Property(e => e.Access)
                .HasMaxLength(15)
                .IsUnicode(true)
                .HasDefaultValueSql("('user')");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(true);
            entity.Property(e => e.GivenName)
                .HasMaxLength(45)
                .IsUnicode(true);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .IsUnicode(true);
            entity.Property(e => e.NickName)
                .HasMaxLength(45)
                .IsUnicode(true);
            entity.Property(e => e.Theme)
                .HasMaxLength(45)
                .IsUnicode(true)
                .HasDefaultValueSql("('Theme1')");
            entity.Property(e => e.UserPasswordHash)
                .HasMaxLength(100)
                .IsUnicode(true);
            entity.Property(e => e.UserPasswordSalt)
                .HasMaxLength(100)
                .IsUnicode(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
