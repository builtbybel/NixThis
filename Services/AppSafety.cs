namespace NixThis;

//Says how risky it is to remove an installed app.
//The answer comes from the Action=apps sections of the filter file, which list package names per
//level. An app that is on no list counts as personal, never as safe, because whether someone
//needs it is not for NixThis to decide.
internal static class AppSafety
{
    //a name on no list is never called safe, because that is not NixThis's call. Protected is the
    //last value of the enum, so the strongest of several hits is the one that counts.
    public static Safety Of(string family) => RuleDatabase.Rules
        .Where(rule => rule.Action == "apps" && rule.Packages.Any(known => Listed(family, known)))
        .Select(rule => rule.Safety)
        .DefaultIfEmpty(Safety.Personal)
        .Max();

    //a line is the start of a package name, or *suffix, which matches the end instead
    private static bool Listed(string family, string known) => known.StartsWith("*")
        ? family.EndsWith(known.Substring(1), StringComparison.OrdinalIgnoreCase)
        : family.StartsWith(known, StringComparison.OrdinalIgnoreCase);
}
