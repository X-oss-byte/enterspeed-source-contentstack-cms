namespace Enterspeed.Source.Contentstack.CMS.Constants;

internal class WebHookConstants
{
    public class Events
    {
        public static string Publish => "publish";
        public static string UnPublish => "unpublish";
        public static string Delete => "delete";
    }

    public class Types
    {
        public static string Entry => "entry";
        public static string Asset => "asset";
    }
}