using UnityEngine;
using System.Collections;

public class ItemsWindow : LayoutWindow
{
    private enum ItemsWindowType { Appearance, DescriptionConfig, Actions}

    private static ItemsWindowType openedWindow = ItemsWindowType.Appearance;
    private static ItemsWindowActions itemsWindowActions;
    private static ItemsWindowAppearance itemsWindowAppearance;
    private static ItemsWindowDescription itemsWindowDescription;

    // Two methods responsible for showing right window content 
    // - concrete item info or base window view
    public void ShowBaseWindowView()
    {
        isConcreteItemVisible = false;
        GameRources.GetInstance().selectedItemIndex = -1;
    }

    public void ShowItemWindowView(int o)
    {
        isConcreteItemVisible = true;
        GameRources.GetInstance().selectedItemIndex = o;
    }

    // Flag determining visibility of concrete item information
    private bool isConcreteItemVisible = false;

    public ItemsWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        itemsWindowActions = new ItemsWindowActions(aStartPos, new GUIContent(Language.GetText("ACTIONS")), "Window");
        itemsWindowAppearance = new ItemsWindowAppearance(aStartPos, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        itemsWindowDescription = new ItemsWindowDescription(aStartPos, new GUIContent(Language.GetText("DESCRIPTION_AND_CONFIG")), "Window");
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
                OnWindowTypeChanged(ItemsWindowType.Appearance);
            }
            if (GUILayout.Button(Language.GetText("ACTIONS")))
            {
                OnWindowTypeChanged(ItemsWindowType.Actions);
            }
            if (GUILayout.Button(Language.GetText("DESCRIPTION_AND_CONFIG")))
            {
                OnWindowTypeChanged(ItemsWindowType.DescriptionConfig);
            }
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case ItemsWindowType.Actions:
                    itemsWindowActions.Draw(aID);
                    break;
                case ItemsWindowType.Appearance:
                    itemsWindowAppearance.Draw(aID);
                    break;
                case ItemsWindowType.DescriptionConfig:
                    itemsWindowDescription.Draw(aID);
                    break;
            }
        }
        else
        {
            GUILayout.Label("ItemsWindow");
        }
    }

    void OnWindowTypeChanged(ItemsWindowType type_)
    {
        openedWindow = type_;
    }
}
