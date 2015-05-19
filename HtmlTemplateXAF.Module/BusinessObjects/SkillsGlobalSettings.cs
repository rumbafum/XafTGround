using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.BusinessObjects
{
    [NonPersistent]
    public class SkillsGlobalSettings
    {
        private SkillsGlobalSettings()
        {

        }
        private static IValueManager<SkillsGlobalSettings> instanceManager;
        public static SkillsGlobalSettings Instance
        {
            get
            {
                if (instanceManager == null)
                {
                    instanceManager = ValueManager.GetValueManager<SkillsGlobalSettings>("SkillsGlobalSettings_SkillsGlobalSettings");
                }
                if (instanceManager.Value == null)
                {
                    instanceManager.Value = new SkillsGlobalSettings();
                }
                return instanceManager.Value;
            }
        }

        TimeZoneInfo tzInfo = null;
        public TimeZoneInfo DefaultTimeZone
        {
            get
            {
                return tzInfo == null ? TimeZoneInfo.Local : tzInfo;
            }
            set
            {
                tzInfo = value;
            }
        }

        public int ClientOffset
        {
            get;
            set;
        }

        public CultureInfo[] Cultures { get; set; }
    }
}
