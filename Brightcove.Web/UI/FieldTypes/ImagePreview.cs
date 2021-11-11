using Sitecore;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentEditor;

namespace Brightcove.Web.UI.FieldTypes
{
  public class ImagePreview : Sitecore.Web.UI.HtmlControls.Control, IContentField
  {  
    public string Source { get; set; }

    public string GetValue()
    {
      return this.Value;
    }

    public void SetValue(string value)
    {
      this.Value = value;
    }

    protected override void DoRender(System.Web.UI.HtmlTextWriter output)
    {
      string value = this.Value;
      if (!string.IsNullOrEmpty(value))
      {
        var values = StringUtil.GetNameValues(this.Source, '=', '&');

        output.Write("<img width='" + values["Width"] + "' src='" + value + "'>");
      }
      else
      {
        //output.Write("<div>" + Translate.Text(Translations.AnAssetDoesNotHaveAnyPreviewImage) + "</div>");
        output.Write("<div>No preview image available</div>");
      }
    }
  }
}