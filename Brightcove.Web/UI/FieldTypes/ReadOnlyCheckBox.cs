using System;
using Sitecore.Shell.Applications.ContentEditor;

namespace Brightcove.Web.UI.FieldTypes
{
  public class ReadOnlyCheckBox : Sitecore.Web.UI.HtmlControls.Checkbox, IContentField
  {
    public string GetValue()
    {
      return this.Value;
    }

    public void SetValue(string value)
    {
      this.Value = value;
    }

    protected override void OnPreRender(EventArgs e)
    {
      this.ReadOnly = true;
      this.Disabled = true;
      //this.Background = "Transparent";
      base.OnPreRender(e);
    }
  }
}