using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBlog.Data
{
   public class Watch
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавие обязательно")]
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }

        [Required(ErrorMessage = "Содержание обязательно")]
        public string Content { get; set; }
      
        [Required(ErrorMessage = "Изображение обязательно")]
        [DisplayName("Изображение:")]
        public string Image { get; set; }

        public string Article { get; set; }
        public string Country { get; set; }
        public string Movement { get; set; }
        public string Frame { get; set; }
        public string Face { get; set; }
        public string Bracelet { get; set; }
        public string Protection { get; set; }
        public string Backlight { get; set; }
        public string Glass { get; set; }
        public string Calendar { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }


        public virtual IEnumerable<Company> CompanyDetails { get; set; }



        // Slug generation taken from http://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}", Id, Model);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

    }
}

