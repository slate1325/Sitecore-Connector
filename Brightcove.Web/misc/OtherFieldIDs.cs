namespace Sitecore.MediaFramework
{
  using Sitecore.Data;

  public static class FieldIDs
  {
    public static class AccountSettings
    {
      public static readonly ID PlaybackEventsRules = new ID("{9E3AC3E4-E9AB-4B9C-94F0-60B90B284D18}");
    }

    public static class MediaElement
    {
      public static readonly ID Events = new ID("{C923961C-5664-43D2-85D2-D194143197DF}");
    }

    public static class PlaybackEvent
    {
      public static readonly ID PageEvent = new ID("{7CCB928C-7EB0-465A-A755-D4BD7256BD58}");
      public static readonly ID Parameter = new ID("{E1E68B6B-5C30-41D3-B6C9-7089B5CE1B34}");
    }
  }
}