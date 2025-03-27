using Microsoft.EntityFrameworkCore;
using WebPizza_API_BackEnd.Entities;
using System;
using System.Linq;

namespace WebPizza_API_BackEnd.Context
{
    public static class DbInitializer
    {
        public static void SeedData(AppDbContext context)
        {
            if (context.Users.Any()) return; // Tránh thêm dữ liệu trùng lặp

            // Thêm Users trước và lấy ID
            var user = new User { FullName = "John Doe", Email = "johndoe@example.com", PasswordHash = "password", Role = "Customer" };
            context.Users.Add(user);
            context.SaveChanges(); // Lưu để có UserID

            // Thêm Categories
            context.Categories.AddRange(
                new Category { CategoryName = "Pizza" },
                new Category { CategoryName = "Drinks" }
            );
            context.SaveChanges();

            // Thêm Products
            context.Products.AddRange(
             new Product { Name = "Pepperoni Pizza", Price = 10.99M, CategoryID = 1, Description = "Delicious pepperoni pizza" },
             new Product { Name = "Coca Cola", Price = 1.99M, CategoryID = 2, Description = "Refreshing soft drink" }
            );
            context.SaveChanges();

            // Thêm Sizes
            context.Sizes.AddRange(
                new Size { Name = "Small" },
                new Size { Name = "Medium" },
                new Size { Name = "Large" }
            );
            context.SaveChanges();

            // Thêm ProductSizes
            context.ProductSizes.AddRange(
                new ProductSize { ProductID = 1, SizeID = 1 },
                new ProductSize { ProductID = 1, SizeID = 2 }
            );
            context.SaveChanges();

            // Thêm Toppings
            context.Toppings.AddRange(
                new Topping { Name = "Extra Cheese" },
                new Topping { Name = "Mushrooms" }
            );
            context.SaveChanges();

            // Thêm ProductToppings
            context.ProductToppings.AddRange(
                new ProductTopping { ProductID = 1, ToppingID = 1 },
                new ProductTopping { ProductID = 1, ToppingID = 2 }
            );
            context.SaveChanges();

            // Thêm Vouchers
            context.Vouchers.AddRange(
                new Voucher { Code = "DISCOUNT10", Discount = 10 }
            );
            context.SaveChanges();

            // Thêm Orders với UserID từ user đã lưu
            var order = new Order { UserID = user.UserID, VoucherID = 1, TotalAmount = 20.99M, OrderDate = DateTime.Now };
            context.Orders.Add(order);
            context.SaveChanges(); // Lưu trước để có OrderID

            // Thêm OrderDetails với OrderID từ Order đã lưu
            context.OrderDetails.AddRange(
                new OrderDetail { OrderID = order.OrderID, ProductID = 1, Quantity = 1, BasePrice = 10.99M, Subtotal = 10.99M }
            );
            context.SaveChanges();

            // Thêm Payments với OrderID từ Order đã lưu
            context.Payments.AddRange(
                new Payment { OrderID = order.OrderID, PaymentMethod = "Credit Card", PaymentStatus = "Pending", TransactionID = null, PaymentDate = DateTime.Now }
            );
            context.SaveChanges();
        }
    }
}
