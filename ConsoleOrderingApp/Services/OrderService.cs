using ConsoleOrderingApp.Models;
using System;

namespace ConsoleOrderingApp.Services;

public class OrderService
{
    public Order CreateOrder(User user, Product product, int quantity)
    {
        if (quantity > product.Stock)
            throw new InvalidOperationException("Not enough stock.");

        product.Stock -= quantity; // reduce stock

        return new Order
        {
            User = user,
            Product = product,
            Quantity = quantity
        };
    }
}
