namespace NCachedBookStore.Contracts.DTO
{
    public class BookDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public string AuthorName { get; set; }
    }
}
