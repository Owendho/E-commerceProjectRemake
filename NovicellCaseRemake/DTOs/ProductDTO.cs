using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NovicellCaseRemake.DTOs
{
    public class ProductDTO
    {
        //Data annotation ensures that custom HTTP error messages are thrown when incoming json is invalid. the required keyword next to puplic ensures that the object is setup properly.
        [JsonPropertyName("id")]
        [Required(ErrorMessage = "Id is required")]
        public required string Id { get; set; }

        [JsonPropertyName("category")]
        [Required(ErrorMessage = "Category is required")]
        public required string Category { get; set; }

        [JsonPropertyName("image")]
        [Required(ErrorMessage = "Image link is required")]
        [Url(ErrorMessage = "Image link must be a valud URL")]
        public required string Image { get; set; } //validate that it is a proper http string

        [JsonPropertyName("price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double Price { get; set; } //ensure the number is above zero. can't have -10 as price

        [JsonPropertyName("title")]
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "Description is required")]
        public required string Description  { get; set; }

    }
}
