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
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Категория обязательна")]
        [DisplayName("Категория")]
        public string CategoryName { get; set; }
        [DisplayName("Дата создания:")]
        [Column(TypeName = "DateTime2")]
        public  DateTime Create_time { get; set; }

        public int Frequence { get; set; }

    }
}
