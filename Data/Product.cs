

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data
{
    public class Product
    {
        public int? ProductId { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public ProductCategory ProductCategory { get; set; }
        public string? Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? SKU { get; set; } = null!;
        public string? Code { get; set; } = null!;
    }
}