using UnityEngine;
using System.Collections;

public class ItemsWindow : LayoutWindow
{
    private enum ItemsWindowType { Appearance, DescriptionConfig, Actions}

    private static ItemsWindowType openedWindow = ItemsWindowType.Appearance;
    private static ItemsWindowActions itemsWindowActions;
    private static ItemsWindowAppearance itemsWindowAppearance;
    private static ItemsWindowDescription itemsWindowDescription;

    private static float windowWidth, windowHeight;

    private static Rect thisRect;

    // Flag determining visibility of concrete item information
    private bool isConcreteItemVisible = false;

    public ItemsWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        itemsWindowActions = new ItemsWindowActions(aStartPos, new GUIContent(Language.GetText("ACTIONS")), "Window");
        itemsWindowAppearance = new ItemsWindowAppearance(aStartPos, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        itemsWindowDescription = new ItemsWindowDescription(aStartPos, new GUIContent(Language.GetText("DESCRIPTION_AND_CONFIG")), "Window");

        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        thisRect = aStartPos;
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
            GUILayout.Space(30);
            for (int i = 0; i < Controller.getInstance().getCharapterList().getSelectedChapterData().getItems().Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Box(Controller.getInstance().getCharapterList().getSelectedChapterData().getItems()[i].getId(), GUILayout.Width(windowWidth * 0.75f));
                if (GUILayout.Button(Language.GetText("EDIT"), GUILayout.MaxWidth(windowWidth * 0.2f)))
                {
                    ShowItemWindowView(i);
                }

                GUILayout.EndHorizontal();

            }
        }
    }

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

        itemsWindowActions = new ItemsWindowActions(thisRect, new GUIContent(Language.GetText("ACTIONS")), "Window");
        itemsWindowAppearance = new ItemsWindowAppearance(thisRect, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        itemsWindowDescription = new ItemsWindowDescription(thisRect, new GUIContent(Language.GetText("DESCRIPTION_AND_CONFIG")), "Window");
    }

    void OnWindowTypeChanged(ItemsWindowType type_)
    {
        openedWindow = type_;
    }
}
