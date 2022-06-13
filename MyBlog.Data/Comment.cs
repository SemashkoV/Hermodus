
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data
{
  public  class Comment
    {
        public int CommentId { get; set; }
        [Required]
        [DisplayName("Комментарий:")]
        public string Comment_Content { get; set; }
        [DisplayName("Дата создания:")]
        [Column(TypeName = "DateTime2")]
        public DateTime Create_time { get; set; }

        [DisplayName("Дата создания:")]
        [Column(TypeName = "DateTime2")]
        public Nullable<DateTime> Update_time { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        [DisplayName("Публикация")]
        public bool Publish { get; set; }
        [DisplayName("Одобрено")]
        
        virtual public User UserDetails { get; set; }

    }
}
