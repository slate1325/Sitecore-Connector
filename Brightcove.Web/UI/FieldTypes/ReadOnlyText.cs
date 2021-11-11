using System;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web.UI.HtmlControls;

namespace Brightcove.Web.UI.FieldTypes
{
  public class ReadOnlyText : Edit, IContentField
  {
    public ReadOnlyText()
    {
      base.Class = "scContentControl";
      base.Activation = true;
    }

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
      this.Background = "Transparent";
      base.OnPreRender(e);
    }
  }
}