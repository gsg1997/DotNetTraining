using System;
using System.Collections.Generic;

// Product class
public class Product
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }

    public Product(string name, decimal price, string category)
    {
        Name = name;
        Price = price;
        Category = category;
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }
}

// User class
public class User
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }

    public User(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }

    public void UpdateEmail(string newEmail)
    {
        Email = newEmail;
    }
}

// Order class
public class Order
{
    private List<Product> products = new List<Product>();
    public decimal TotalAmount { get; private set; }  // Make this property public

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");

        products.Add(product);
        CalculateTotal();
    }

    public virtual void CalculateTotal()
    {
        TotalAmount = 0;
        foreach (var product in products)
        {
            TotalAmount += product.Price;
        }
    }

    public void PrintOrderDetails()
    {
        Console.WriteLine("Order Details:");
        foreach (var product in products)
        {
            Console.WriteLine($"- {product.Name}: {product.Price:C}");
        }
        Console.WriteLine($"Total Amount: {TotalAmount:C}");
    }
}

// Person class
public class Person
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }

    public Person(string name, string email)
    {
        Name = name;
        Email = email;
    }
}

// Customer class
public class Customer : Person
{
    public string LoyaltyCardNumber { get; set; }

    public Customer(string name, string email, string loyaltyCardNumber)
        : base(name, email)
    {
        LoyaltyCardNumber = loyaltyCardNumber;
    }
}

// Admin class
public class Admin : Person
{
    public string AdminCode { get; set; }

    public Admin(string name, string email, string adminCode)
        : base(name, email)
    {
        AdminCode = adminCode;
    }
}

// DiscountedOrder class for polymorphism
public class DiscountedOrder : Order
{
    public decimal Discount { get; set; }

    public void ApplyDiscount(decimal discount)
    {
        Discount = discount;
        CalculateTotal();
    }

    
}

// IOrderProcessor interface
public interface IOrderProcessor
{
    void ProcessOrder(Order order);
    void CancelOrder(Order order);
}

// PaymentProcessor class
public class PaymentProcessor : IOrderProcessor
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Processing payment for order total: {order.TotalAmount:C}");
    }

    public void CancelOrder(Order order)
    {
        Console.WriteLine("Payment for the order has been cancelled.");
    }
}

// ShippingProcessor class
public class ShippingProcessor : IOrderProcessor
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Shipping order with total: {order.TotalAmount:C}");
    }

    public void CancelOrder(Order order)
    {
        Console.WriteLine("Shipping for the order has been cancelled.");
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Creating products
        var product1 = new Product("Laptop", 999.99m, "Electronics");
        var product2 = new Product("Phone", 499.99m, "Electronics");

        // Creating a user
        var user = new User("john_doe", "password123", "john@example.com");

        // Creating an order
        var order = new Order();
        order.AddProduct(product1);
        order.AddProduct(product2);

        // Print order details
        order.PrintOrderDetails();

        // Inheritance demonstration
        var customer = new Customer("Jane Doe", "jane@example.com", "L123");
        var admin = new Admin("Admin User", "admin@example.com", "A123");

        // Polymorphism demonstration
        var discountedOrder = new DiscountedOrder();
        discountedOrder.AddProduct(product1);
        discountedOrder.AddProduct(product2);
        discountedOrder.ApplyDiscount(100); // Apply a discount
        Console.WriteLine($"Total Amount after discount: {discountedOrder.TotalAmount:C}");

        // Interface demonstration
        IOrderProcessor paymentProcessor = new PaymentProcessor();
        paymentProcessor.ProcessOrder(order);

        IOrderProcessor shippingProcessor = new ShippingProcessor();
        shippingProcessor.ProcessOrder(order);
    }
}