using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace WebBack.Model{
    [PrimaryKey(nameof(Id))]
    public class Product{
        [Required]
        public int Id { get; set;}
        [Required]
        public required string Title { get; set;}
        public string? Description { get; set;} = null;
        [Required]
        public required Category Category { get; set;}
        [Required]
        public BigInteger Price { get; set;}
        public List<string>? Images { get; set;}
        public int? Thumbnail { get; set;}
    }
}