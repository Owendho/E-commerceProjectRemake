namespace NovicellCaseRemake.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Category { get; set; }

        public required string  Image { get; set; } //validate that it is a proper http string

        public double Price { get; set; } //ensure the number is above zero. can't have -10 as price

        public required string Title { get; set; }

        public required string Description  { get; set; }

        
    }
}
