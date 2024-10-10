using Stage1;
using Microsoft.EntityFrameworkCore;

namespace Stage1
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}