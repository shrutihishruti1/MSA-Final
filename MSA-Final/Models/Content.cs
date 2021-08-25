using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Final.Models
{
    public enum Year
    {
        YEAR_2021
    }
    public class Content
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Link { get; set; } = null!;

        [Required]
        public Year Year { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

}
