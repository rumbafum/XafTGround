﻿using DevExpress.Xpo.Metadata;
using HtmlTemplateXAF.Module.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.BusinessObjects.Converters
{
    public class DateTimeOffsetConverter : ValueConverter
    {
        public override object ConvertFromStorageType(object value)
        {
            try
            {
                if (value is string)
                {
                    bool result = false;
                    DateTimeStyles styles = DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite |
                                           DateTimeStyles.AllowTrailingWhite;
                    DateTimeOffset fromDB = DateTimeOffset.Now;
                    foreach (CultureInfo culture in SkillsGlobalSettings.Instance.Cultures)
                    {
                        result = DateTimeOffset.TryParse((value as string), culture, styles, out fromDB);
                        if (result) break;
                    }
                    if (result)
                    {
                        DateTimeOffset localDateOffset = DateTimeHelper.ConvertToLocalTime(fromDB);
                        return localDateOffset;
                    }


                    //Working solution
                    //bool result = false;
                    //DateTimeStyles styles = DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite |
                    //                       DateTimeStyles.AllowTrailingWhite;

                    //DateTime localDate = DateTimeOffset.UtcNow.UtcDateTime;
                    //DateTimeOffset localDateOffset;
                    //foreach (CultureInfo culture in SkillsGlobalSettings.Instance.Cultures)
                    //{
                    //    result = DateTime.TryParse((value as string), culture, styles, out localDate);
                    //    if (result) break;
                    //}
                    //if (result)
                    //{
                    //    try
                    //    {
                    //        localDate = DateTime.SpecifyKind(localDate, DateTimeKind.Unspecified);
                    //        localDateOffset = new DateTimeOffset(localDate, new TimeSpan(0, -SkillsGlobalSettings.Instance.ClientOffset, 0));
                    //        return localDateOffset;
                    //    }
                    //    catch (Exception)
                    //    {
                    //        result = false;
                    //    }
                    //}
                }
                    
                return value;
            }
            catch
            {
                return DateTimeOffset.MinValue;
            }
            
        }

        public override object ConvertToStorageType(object value)
        {
            if (value is DateTimeOffset)
            {
                DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
                DateTimeOffset dto = DateTimeHelper.ConvertToLocalTime(dateTimeOffset);
                return dto.ToString("O");
            }
            return value;
        }

        public override Type StorageType
        {
            get { return typeof(string); }
        }
    }
}
