
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebBack.Model
{
    [PrimaryKey(nameof(Id))]
    public class Country
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public List<City> Cities { get; set; }
    }
}
