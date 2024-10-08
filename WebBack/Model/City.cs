

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebBack.Model
{
    [PrimaryKey(nameof(Id))]
    public class City
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
