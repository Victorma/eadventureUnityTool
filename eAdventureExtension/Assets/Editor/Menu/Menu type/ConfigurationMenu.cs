using UnityEditor;

public class ConfigurationMenu : WindowMenuContainer
{
    public ConfigurationMenu()
    {
        SetMenuItems();
    }

    protected override void Callback(object obj)
    {
    }

    protected override void SetMenuItems()
    {
        menu = new GenericMenu();
    }
}
