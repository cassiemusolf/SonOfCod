using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SonOfCodSeafood.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
