using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Required(ErrorMessage = "Role name is required.")]
        [MaxLength(20, ErrorMessage = "Role name 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Role Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        public ICollection<User>? Users { get; set; } = new List<User>();
    }

}
