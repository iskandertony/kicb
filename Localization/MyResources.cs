using Toolbelt.Blazor.I18nText.Interfaces;

namespace MyBlazorApp.Localization
{
    public class MyResources : I18nTextFallbackLanguage
    {
        public string Hello { get; set; } = "Hello";
        public string ChangeLanguage { get; set; } = "Change language";
        public string TestPhrase { get; set; } = "This is a test phrase"; 

        public string FallBackLanguage => "en";
    }

}


