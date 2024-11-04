using Stage1;
using Microsoft.EntityFrameworkCore;

namespace Stage1.Model
{
    public class Order : IOrder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
