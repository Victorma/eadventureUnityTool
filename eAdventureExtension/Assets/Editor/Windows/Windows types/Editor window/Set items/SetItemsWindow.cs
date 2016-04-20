using UnityEngine;

public class SetItemsWindow : LayoutWindow
{
    private enum SetItemsWindowType { Appearance, Documentation}

    private static SetItemsWindowType openedWindow = SetItemsWindowType.Appearance;
    private static SetItemsWindowApperance setItemsWindowApperance;
    private static SetItemsWindowDocumentation setItemsWindowDocumentation;

    // Two methods responsible for showing right window content 
    // - concrete item info or base window view
    public void ShowBaseWindowView()
    {
        isConcreteItemVisible = false;
    }

    // TODO - change Object to adequate class
    public void ShowItemWindowView(Object o)
    {
        isConcreteItemVisible = true;
        // TODO - set desire object reference
    }

    // Flag determining visibility of concrete item information
    private bool isConcreteItemVisible = false;

    public SetItemsWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        setItemsWindowApperance = new SetItemsWindowApperance(aStartPos, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        setItemsWindowDocumentation = new SetItemsWindowDocumentation(aStartPos, new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
    }


    public override void Draw(int aID)
    {
        // Show information of concrete item
        if (isConcreteItemVisible)
        {
            /**
             UPPER MENU
            */
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(Language.GetText("APPEARANCE")))
            {
                OnWindowTypeChanged(SetItemsWindowType.Appearance);
            }
            if (GUILayout.Button(Language.GetText("DOCUMENTATION")))
            {
                OnWindowTypeChanged(SetItemsWindowType.Documentation);
            }
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case SetItemsWindowType.Appearance:
                    setItemsWindowApperance.Draw(aID);
                    break;
                case SetItemsWindowType.Documentation:
                    setItemsWindowDocumentation.Draw(aID);
                    break;
            }
        }
        else
        {
            GUILayout.Label("SetItemsWindow");
        }
    }

    void OnWindowTypeChanged(SetItemsWindowType type_)
    {
        openedWindow = type_;
    }
}