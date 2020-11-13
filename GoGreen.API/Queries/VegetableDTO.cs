namespace GoGreen.API.Queries
{
    public class VegetableDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
