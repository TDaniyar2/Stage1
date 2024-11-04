namespace Stage1.Model
{
    public interface IOrder
    {
        public Guid Id { get; }
        public string Name { get;}
        public string Description { get; }
    }
}
