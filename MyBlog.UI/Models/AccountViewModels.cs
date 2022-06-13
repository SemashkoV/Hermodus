
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlog.UI.Models
{

    [Table("Users")]
    public class LoginViewModel
    {
       
        public int UserId { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
      

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        public string GoogleSitekey { get; set; }



    }
    [Table("Users")]
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        [DataType(DataType.Text)]
        public string FName { get; set; }


        [Required]
        [Display(Name = "Фамилия")]
        [DataType(DataType.Text)]
        public string LName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать как минимум {2} символа(-ов)", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароль не совпадает")]
        public string ConfirmPassword { get; set; }
    }
    [Table("Users")]
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать как минимум {2} символа(-ов)", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль не совпадает")]
        public string ConfirmPassword { get; set; }

       
    }


}