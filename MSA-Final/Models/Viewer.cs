using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Final.Models
{
    public class Viewer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string GitHub { get; set; }

        public string ImageURI { get; set; }

        public ICollection<Content> Contents { get; set; } = new List<Content>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
