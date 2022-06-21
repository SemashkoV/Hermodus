
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Service
{
    public interface IEmailSettingRepository
    {
        EmailSetting GetEmailSetting { get; }
        void Save(EmailSetting Emailsetting);
        EmailSetting  Details(int? Id);
    }
}
