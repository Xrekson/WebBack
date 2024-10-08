using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBack.Model
{
    [PrimaryKey(nameof(Email), nameof(password))]
    [Index(nameof(Mobile), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
        [Required]
        public string Mobile { get; set; } = string.Empty;
    }
}
