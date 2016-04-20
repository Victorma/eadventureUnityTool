using UnityEngine;
using UnityEditor;

public class AboutMenu : WindowMenuContainer
{
    private AboutEAMenuItem about;
    private AboutEASendMenuItem send;

    public AboutMenu()
    {
        SetMenuItems();
    }

    protected override void Callback(object obj)
    {
        if ((obj as AboutEAMenuItem) != null)
            about.OnCliked();
        else if ((obj as AboutEASendMenuItem) != null)
            send.OnCliked();
    }

    protected override void SetMenuItems()
    {
        menu = new GenericMenu();

        about = new AboutEAMenuItem("ABOUT_EADVENTURE");
        send = new AboutEASendMenuItem("ABOUT_SEND");

        menu.AddItem(new GUIContent(Language.GetText(about.Label)), false, Callback, about);
        menu.AddItem(new GUIContent(Language.GetText(send.Label)), false, Callback, send);
    }
}