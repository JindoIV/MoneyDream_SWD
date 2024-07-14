using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Models;

public partial class MoneyDreamContext : DbContext
{
    public MoneyDreamContext()
    {
    }

    public MoneyDreamContext(DbContextOptions<MoneyDreamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountAddress> AccountAddresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ConversationUser> ConversationUsers { get; set; }

    public virtual DbSet<EmailToken> EmailTokens { get; set; }

    public virtual DbSet<ExportInfo> ExportInfos { get; set; }

    public virtual DbSet<GenMainSlider> GenMainSliders { get; set; }

    public virtual DbSet<ImportInfo> ImportInfos { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FMDP3CV\\MSSQLSERVER01;User ID=sa;Password=123456;Database=MoneyDream;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK_Accounts");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.City).HasMaxLength(250);
            entity.Property(e => e.Created)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Picture)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.State)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Roles");
        });

        modelBuilder.Entity<AccountAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId);

            entity.ToTable("AccountAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.DeleteAt)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DeliveryName).HasMaxLength(100);
            entity.Property(e => e.DeliveryPhone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.AccountAddresses)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountAddress_Account");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("Cart");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Account).WithMany(p => p.Carts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Account");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.ToTable("Conversation");

            entity.Property(e => e.ConversationId)
                .ValueGeneratedNever()
                .HasColumnName("ConversationID");
            entity.Property(e => e.CreateAt).HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<ConversationUser>(entity =>
        {
            entity.ToTable("ConversationUser");

            entity.Property(e => e.ConversationUserId)
                .ValueGeneratedNever()
                .HasColumnName("ConversationUserID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

            entity.HasOne(d => d.Account).WithMany(p => p.ConversationUsers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConversationUser_Account");

            entity.HasOne(d => d.Conversation).WithMany(p => p.ConversationUsers)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConversationUser_Conversation");
        });

        modelBuilder.Entity<EmailToken>(entity =>
        {
            entity.ToTable("EmailToken");

            entity.Property(e => e.EmailTokenId).HasColumnName("EmailTokenID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Created)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(220)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.EmailTokens)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailToken_Account");
        });

        modelBuilder.Entity<ExportInfo>(entity =>
        {
            entity.HasKey(e => e.ExportId).HasName("PK__ExportIn__E5C997A41B973917");

            entity.ToTable("ExportInfo");

            entity.Property(e => e.ExportId).HasColumnName("ExportID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.DateExport).HasColumnType("datetime");
            entity.Property(e => e.ImportId).HasColumnName("ImportID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Account).WithMany(p => p.ExportInfos)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExportInfo_Account");

            entity.HasOne(d => d.Import).WithMany(p => p.ExportInfos)
                .HasForeignKey(d => d.ImportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExportInfo_ImportInfo");

            entity.HasOne(d => d.Product).WithMany(p => p.ExportInfos)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExportInfo_Products");
        });

        modelBuilder.Entity<GenMainSlider>(entity =>
        {
            entity.HasKey(e => e.MainSliderId);

            entity.ToTable("genMainSlider");

            entity.Property(e => e.MainSliderId).HasColumnName("MainSliderID");
            entity.Property(e => e.AltText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BtnText)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.OfferTag)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImportInfo>(entity =>
        {
            entity.HasKey(e => e.ImportId).HasName("PK__ImportIn__8697678ACF9A55CF");

            entity.ToTable("ImportInfo");

            entity.Property(e => e.ImportId).HasColumnName("ImportID");
            entity.Property(e => e.DateImport).HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ImportInfos)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImportInfo_Products");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.MessageId)
                .ValueGeneratedNever()
                .HasColumnName("MessageID");
            entity.Property(e => e.AttachedFileUrl).HasMaxLength(100);
            entity.Property(e => e.ConversationId).HasColumnName("ConversationID");
            entity.Property(e => e.CreateAt).HasMaxLength(50);
            entity.Property(e => e.MessageContent).HasMaxLength(500);
            entity.Property(e => e.SenderId).HasColumnName("SenderID");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Conversation");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Oder");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.TotalAmount).HasColumnType("money");
            entity.Property(e => e.UpdateStatusAt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Account");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_AccountAddress");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_OrderStatus");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_Order_Payment");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Orders)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK_Order_Voucher");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId);

            entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");
            entity.Property(e => e.OrderDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("OrderStatus");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.PaymentDateTime)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_PaymentType");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PayTypeId);

            entity.ToTable("PaymentType");

            entity.Property(e => e.PayTypeId).HasColumnName("PayTypeID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.ImageUrlcloud)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ImageURLCloud");
            entity.Property(e => e.ImportId).HasColumnName("ImportID");
            entity.Property(e => e.PublicId)
                .IsUnicode(false)
                .HasColumnName("PublicID");
            entity.Property(e => e.SizeId).HasColumnName("SizeID");
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Import).WithMany(p => p.Products)
                .HasForeignKey(d => d.ImportId)
                .HasConstraintName("FK_Products_ImportInfo");

            entity.HasOne(d => d.Size).WithMany(p => p.Products)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Size");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Suplier");

            entity.HasOne(d => d.Unit).WithMany(p => p.Products)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Unit");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("ProductImage");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PublicId)
                .IsUnicode(false)
                .HasColumnName("PublicID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Products");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.RefreshTokenId).HasColumnName("RefreshTokenID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Created)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_Account");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreateAt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ReviewContent).HasMaxLength(500);

            entity.HasOne(d => d.Account).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Review_Account");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("SizeID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ProductHeight)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ProductWidth)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SampleHeight)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SampleWeight)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666949719BD7E");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Unit__44F5EC954959C305");

            entity.ToTable("Unit");

            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.CreateAt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Discount).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.ExpiredAt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VoucherName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.ToTable("Wishlist");

            entity.Property(e => e.WishlistId).HasColumnName("WishlistID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Account).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wishlist_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
