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
        [Required(ErrorMessage = "Титульный текст обязателен")]
        [DisplayName("Титульный текст:")]
     
        public string HomeImageText { get; set; }
        [Required(ErrorMessage = "Изображение слайдера обязательно")]
        [DisplayName("Изображение слайдера 1:")]
        public int HomeImage1 { get; set; }
        [Required(ErrorMessage = "Изображение слайдера обязательно")]
        [DisplayName("Изображение слайдера 2:")]
        public int HomeImage2 { get; set; }
        [Required(ErrorMessage = "Изображение слайдера обязательно")]
        [DisplayName("Изображение слайдера 3:")]
        public int HomeImage3 { get; set; }
        [Required(ErrorMessage = "Кол-во последних постов обязательно")]
        [DisplayName("Кол-во последних постов:")]
        public int NumberOfLastPost { get; set; }
        [Required(ErrorMessage = "Кол-во категорий обязательно")]
        [DisplayName("Кол-во категорий:")]
        public int NumberOfCategory { get; set; }
        [Required(ErrorMessage = "Кол-во часов на странице обязательно")]
        [DisplayName("Кол-во часов на странице:")]
        public int PostNumberInPage { get; set; }
        [Required(ErrorMessage = "Число топ постов обязательно")]
        [DisplayName("Число топ постов:")]
        public int NumberOfTopPost { get; set; }

        [DisplayName("Дата создания:")]
        public DateTime Update_Time { get; set; }
        [DisplayName("Дата создания:")]
        public int UserId { get; set; }
        [DisplayName("Последняя категория")]
     

        public virtual User UserDetails { get; set; }

    }
}
