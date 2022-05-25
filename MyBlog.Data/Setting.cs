using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data
{
    public class Setting
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Home Text is required")]
        [DisplayName("Титульный текст:")]
     
        public string HomeImageText { get; set; }
        [Required(ErrorMessage = "Home Image is required")]
        [DisplayName("Изображение слайдера 1:")]
        public int HomeImage1 { get; set; }
        [Required(ErrorMessage = "Home Image is required")]
        [DisplayName("Изображение слайдера 2:")]
        public int HomeImage2 { get; set; }
        [Required(ErrorMessage = "Home Image is required")]
        [DisplayName("Изображение слайдера 3:")]
        public int HomeImage3 { get; set; }
        [Required(ErrorMessage = "Num. of Last Post is required")]
        [DisplayName("Кол-во последних постов:")]
        public int NumberOfLastPost { get; set; }
        [Required(ErrorMessage = "Num. of Category is required")]
        [DisplayName("Кол-во категорий:")]
        public int NumberOfCategory { get; set; }
        [Required(ErrorMessage = "Post Num. in Page is required")]
        [DisplayName("Кол-во часов на странице:")]
        public int PostNumberInPage { get; set; }
        [Required(ErrorMessage = "Num. of Top Post is required")]
        [DisplayName("Num. of Top Post:")]
        public int NumberOfTopPost { get; set; }

        [DisplayName("Last Update:")]
        public DateTime Update_Time { get; set; }
        [DisplayName("Updated By:")]
        public int UserId { get; set; }
        [DisplayName("Last Category")]
     

        public virtual User UserDetails { get; set; }

    }
}
