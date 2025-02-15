﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hermodus.Data
{
   
   public class Post
    {
       [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Заполните заглавие")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Заполните пост")]
        public string Post_Content { get; set; }

        [DisplayName("Дата создания:") ]
        
        public DateTime Create_time { get; set; }

        [DisplayName("Дата создания:")]
        
        public DateTime Update_time { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Тег обязателен")]
        [DisplayName("Теги:")]
        public string Tages { get; set; }
        [Required(ErrorMessage = "Категория обязательна")]
        [DisplayName("Категория:")]
        public int CategoryId { get; set; }
        public int Frequence { get; set; }
        [Required(ErrorMessage = "Изображение обязательно")]
        [DisplayName("Ссылка на изображение:")]
        public string FeaturedImage { get; set; }

        public virtual User UserDetails { get; set; }
        public virtual Category CategoryDetail { get; set; }
        public virtual IEnumerable<Category> CategoryDetails { get; set; }


   
        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}", PostId, Title);

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
