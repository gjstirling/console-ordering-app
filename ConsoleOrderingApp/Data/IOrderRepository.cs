
using ConsoleOrderingApp.Models;

namespace ConsoleOrderingApp.Data
{
    public interface IOrderRepository
    {
        Order? GetById(int id);
        IEnumerable<Order> GetAll();
        void Add(Order order);
    }
}
