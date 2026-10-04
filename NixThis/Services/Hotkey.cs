using System.Runtime.InteropServices;

namespace NixThis;

//Registers Ctrl+Shift+Space so picking can be started while another window has the focus.
//That matters for things that close the moment something else is clicked, like the Start menu.
//The key press arrives at a window that is never shown, so nothing on screen moves. Windows
//gives a combination to whoever asked for it first, and Ready says whether that was this app.
internal sealed class Hotkey : NativeWindow, IDisposable
{
    private const int Pressed = 0x0312, Control = 0x0002, Shift = 0x0004;

    private readonly Action _action;
    private readonly int _id;

    //Windows hands a combination to whoever asked first, so a second asker is simply refused.
    //A shortcut the app cannot deliver must not be promised on screen.
    public bool Ready { get; }

    public Hotkey(Keys key, Action action)
    {
        _action = action;
        //the key doubles as the id, which keeps every registration in this process distinct
        _id = (int)key;
        CreateHandle(new CreateParams());
        Ready = RegisterHotKey(Handle, _id, Control | Shift, _id);
    }

    protected override void WndProc(ref Message message)
    {
        if (message.Msg == Pressed) _action();
        base.WndProc(ref message);
    }

    public void Dispose()
    {
        UnregisterHotKey(Handle, _id);
        DestroyHandle();
    }

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr window, int id, int modifiers, int key);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr window, int id);
}
