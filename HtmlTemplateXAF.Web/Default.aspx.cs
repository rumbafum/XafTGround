using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using HtmlTemplateXAF.Module.Helpers;
using HtmlTemplateXAF.Module.Web.Controllers.Skills;
using DevExpress.Web;
using HtmlTemplateXAF.Module.BusinessObjects;
using System.Globalization;

public partial class Default : BaseXafPage, ICallbackFrameTemplate
{
    protected override ContextActionsMenu CreateContextActionsMenu()
    {
        return new ContextActionsMenu(this, "Edit", "RecordEdit", "ObjectsCreation", "ListView", "Reports");
    }
    public override Control InnerContentPlaceHolder
    {
        get
        {
            return Content;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        CurrentPageHelper.Request = Request;
        if (!string.IsNullOrEmpty(Page.Request["__CALLBACKPARAM"]) && Page.Request["__CALLBACKPARAM"].ToString().Contains("NTAC"))
            CurrentPageHelper.CurrentPageUrl = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CallbackManager.PreRender += new EventHandler<EventArgs>(CallbackManager_PreRender);

        if (Request.Cookies.AllKeys.Contains("skillsTimezoneOffset"))
        {
            SkillsGlobalSettings.Instance.ClientOffset = Convert.ToInt16(Request.Cookies["skillsTimezoneOffset"].Value);
        }
        if (Request.Cookies.AllKeys.Contains("skillsTimezone"))
        {
            try
            {
                SkillsGlobalSettings.Instance.DefaultTimeZone =
                    SkillsGlobalSettings.Instance.TimeZoneDB.MapTZID(Request.Cookies["skillsTimezone"].Value);
            }
            catch (Exception ex)
            {
                TimeZoneInfo tzInfo = null;
                TimeSpan ts = new TimeSpan(0, -SkillsGlobalSettings.Instance.ClientOffset, 0);
                List<TimeZoneInfo> withSameTime = new List<TimeZoneInfo>();
                string clientZoneName = Request.Cookies["skillsTimezone"].Value;
                foreach(TimeZoneInfo tzi in TimeZoneInfo.GetSystemTimeZones())
                {
                    if (tzi.BaseUtcOffset.TotalMinutes == ts.TotalMinutes)
                        withSameTime.Add(tzi);
                }
                foreach (TimeZoneInfo tzi in withSameTime)
                {
                    string[] splited = tzi.DisplayName.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in splited)
                    {
                        if (clientZoneName.Contains(s))
                            tzInfo = tzi;
                    }
                }
                SkillsGlobalSettings.Instance.DefaultTimeZone = tzInfo != null ? tzInfo : (withSameTime.Count > 0 ? withSameTime[0] : TimeZoneInfo.Local);
            }
        }

        CultureInfo[] cultures = new CultureInfo[Request.UserLanguages.Length + 1];
        for (int ctr = Request.UserLanguages.GetLowerBound(0); ctr <= Request.UserLanguages.GetUpperBound(0);
             ctr++)
        {
            string locale = Request.UserLanguages[ctr];
            if (!string.IsNullOrEmpty(locale))
            {
                if (locale.Contains(";"))
                    locale = locale.Substring(0, locale.IndexOf(';'));
                try
                {
                    cultures[ctr] = new CultureInfo(locale, false);
                }
                catch (Exception) { }
            }
            else
            {
                cultures[ctr] = CultureInfo.CurrentCulture;
            }
        }
        cultures[Request.UserLanguages.Length] = CultureInfo.InvariantCulture;
        SkillsGlobalSettings.Instance.Cultures = cultures;
    }

    private void CallbackManager_PreRender(object sender, EventArgs e)
    {
        try
        {
            string url = GetCurrentViewUrl();
            CurrentPageHelper.CurrentPageUrl = url.Split(new string[] { "ShortcutViewID=" }, StringSplitOptions.RemoveEmptyEntries)[1].
                Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries)[0];
        }
        catch
        {
            CurrentPageHelper.CurrentPageUrl = "";
        }
    }
    protected override void OnUnload(EventArgs e)
    {
        CallbackManager.PreRender -= new EventHandler<EventArgs>(CallbackManager_PreRender);
        base.OnUnload(e);
    }
    private string GetCurrentViewUrl()
    {
        DevExpress.ExpressApp.View view = WebWindow.CurrentRequestWindow != null ? WebWindow.CurrentRequestWindow.View : null;
        return view != null && WebApplication.Instance != null ? WebApplication.Instance.RequestManager.GetQueryString(view.CreateShortcut()) : String.Empty;
    }


    #region ICallbackFrameTemplate Members

    ICollection<ASPxCallbackPanel> ICallbackFrameTemplate.GetASPxCallbackPanels()
    {
        return new[] 
		    { 
			    this.ASPxCallbackPanel1,
			    this.ASPxCallbackPanel4,
		    };
    }

    ASPxCallbackPanel ICallbackFrameTemplate.ProcessActionCallbackPanel
    {
        get { return this.ASPxCallbackPanel4; }
    }

    #endregion



    public ICollection<IActionContainer> GetContainers()
    {
        return base.GetContainers();
    }

    public void SetView(DevExpress.ExpressApp.View view)
    {
        base.SetView(view);
    }

}
