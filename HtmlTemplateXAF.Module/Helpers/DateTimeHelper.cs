using HtmlTemplateXAF.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTimeOffset Now
        {
            get
            {
                DateTimeOffset serverTime = DateTimeOffset.Now;
                return ConvertToLocalTime(serverTime);
            }
        }

        public static DateTimeOffset ConvertToLocalTime(DateTimeOffset serverTime)
        {
            DateTime dt = TimeZoneInfo.ConvertTimeFromUtc(serverTime.UtcDateTime, SkillsGlobalSettings.Instance.DefaultTimeZone);
            DateTimeOffset localDateOffset = new DateTimeOffset(dt, SkillsGlobalSettings.Instance.DefaultTimeZone.GetUtcOffset(dt));
            return localDateOffset;
        }
    }
}
