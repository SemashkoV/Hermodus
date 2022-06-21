using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Data
{
   public class EmailSetting
    {
        public int Id { get; set; }
        [DisplayName("SMTP сервер")]
        public string SMTP_Server { get; set; }
        [DisplayName("Отправитель")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Sender { get; set; }
        [DisplayName("SMTP порт")]
        public int  SMTPServer_Port { get; set; }
        [DisplayName("Имя")]
        public string UserName { get; set; }
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password  { get; set; }
        [DisplayName("Включить SSL")]
        public bool EnableSSL { get; set; }
        [DisplayName("Дата посл. обновления")]
        public int UserId { get; set; }
        [DisplayName("Последнее обновление")]
        public DateTime Last_Update { get; set; }
        public virtual User UserDetails { get; set; }
    }
}
