namespace NixThis;

//what every subpage has in common: the name the list shows, and the chance to keep what was typed
internal class SettingsPage : UserControl
{
    public virtual string Title => "";
    public virtual void Save() { }
}
