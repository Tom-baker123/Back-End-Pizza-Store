using Microsoft.EntityFrameworkCore;
using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Khai báo các DbSet cho từng bảng
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PizzaSize> Sizes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<ProductTopping> ProductToppings { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderTopping> OrderToppings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // **Cấu hinh bảng ProductSizes (Nhiều-Nhiều giữa Product và Size)**
            modelBuilder.Entity<ProductSize>()
                .HasKey(ps => new { ps.ProductID, ps.SizeID });

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(ps => ps.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Size)
                .WithMany(s => s.ProductSizes)
                .HasForeignKey(ps => ps.SizeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PizzaSize>()
                .HasKey(p => p.SizeID); // Chỉ định SizeID là primary key

            // **Cấu hình bảng ProductToppings (Nhiều-Nhiều giữa Product và Topping)**
            modelBuilder.Entity<ProductTopping>()
                .HasKey(pt => new { pt.ProductID, pt.ToppingID });

            modelBuilder.Entity<ProductTopping>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductToppings)
                .HasForeignKey(pt => pt.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductTopping>()
                .HasOne(pt => pt.Topping)
                .WithMany(t => t.ProductToppings)
                .HasForeignKey(pt => pt.ToppingID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình bảng OrderToppings (Nhiều-Nhiều giữa OrderDetail và Topping)**
            modelBuilder.Entity<OrderTopping>()
                .HasKey(ot => new { ot.OrderDetailID, ot.ToppingID });

            modelBuilder.Entity<OrderTopping>()
                .HasOne(ot => ot.OrderDetail)
                .WithMany(od => od.OrderToppings)
                .HasForeignKey(ot => ot.OrderDetailID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderTopping>()
                .HasOne(ot => ot.Topping)
                .WithMany(t => t.OrderToppings)
                .HasForeignKey(ot => ot.ToppingID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình bảng ProductPromotions (Nhiều-Nhiều giữa Product và Promotion)**
            modelBuilder.Entity<ProductPromotion>()
                .HasKey(pp => new { pp.ProductID, pp.PromotionID });

            modelBuilder.Entity<ProductPromotion>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.ProductPromotions)
                .HasForeignKey(pp => pp.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductPromotion>()
                .HasOne(pp => pp.Promotion)
                .WithMany(pr => pr.ProductPromotions)
                .HasForeignKey(pp => pp.PromotionID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa Product và Category**
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // **Cấu hình quan hệ giữa Order và User**
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa Order và Voucher**
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Voucher)
                .WithMany(v => v.Orders)
                .HasForeignKey(o => o.VoucherID)
                .OnDelete(DeleteBehavior.SetNull); // Nếu voucher bị xóa, đơn hàng vẫn tồn tại

            // **Cấu hình quan hệ giữa OrderDetail và Order**
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa OrderDetail và Product**
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa Payment và Order**
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa Review và User**
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa Review và Product**
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa Cart và User**
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // **Cấu hình quan hệ giữa Cart và Product**
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
