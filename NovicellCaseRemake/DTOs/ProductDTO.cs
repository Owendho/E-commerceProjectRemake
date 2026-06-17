using System.Text.Json.Serialization;

namespace NovicellCaseRemake.DTOs
{
    public class ProductDTO
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("category")]
        public required string Category { get; set; }

        [JsonPropertyName("image")]
        public required string  Image { get; set; } //validate that it is a proper http string

        [JsonPropertyName("price")]
        public double Price { get; set; } //ensure the number is above zero. can't have -10 as price

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("description")]
        public required string Description  { get; set; }

        
    }
}
