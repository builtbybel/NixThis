namespace NixThis;

//What the app says about itself
internal static class AppInfo
{
    // --- Identity ------------------------------------------------------------

    //the name is a name, not a word: no locale file translates it
    public static string Name => Application.ProductName;

    //what the about page and the version line at the bottom of the panel show
    public static string Version => Application.ProductVersion;

    //a signature is not a sentence to translate, so this one line stays English everywhere
    public const string Credit = "A Belim app creation (2026)";

    // --- Links -----------------------------------------------------------------

    public const string Repo = "https://github.com/builtbybel/NixThis";

    public const string Issues = Repo + "/issues";
    public const string Donate = "https://www.paypal.com/donate?hosted_button_id=MY7HX4QLYR4KG";
}
