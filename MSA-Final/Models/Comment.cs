using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Final.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int ContentId { get; set; }

        public Content Content { get; set; } = null!;

        [Required]
        public int ViewerId { get; set; }

        public Viewer Viewer { get; set; } = null!;

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
