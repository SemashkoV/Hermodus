using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Data
{

    public class User
    {
        public int UserId { get; set; }
        [DisplayName("Имя")]
        public string FName { get; set; }
        [DisplayName("Фамилия")]
        public string LName { get; set; }
     //   [Index(IsUnique = true)]
        [DisplayName("Почта")]
        [DataType(DataType.EmailAddress)]
       
        public string Email { get; set; }


        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [DisplayName("Дата создания")]
        public DateTime Create_time { get; set; }
        [DisplayName("Дата изменения")]
        public DateTime Update_Time{get;set;}
        [DisplayName("Последний вход")]
        public DateTime Last_Login { get; set; }
        [DisplayName("Статус")]
        public int RoleId { get; set; }

        virtual public Role RoleDetails { get; set; }
        virtual public IEnumerable<Role> IENUMRoleDetails { get; set; }

    }
}
