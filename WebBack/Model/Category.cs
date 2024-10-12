using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebBack.Model
{
    [PrimaryKey(nameof(Id))]
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}