using UnityEngine;
using System.Collections;

public class AdvencedFeaturesWindow : LayoutWindow
{
    private enum AdvencedFeaturesWindowType { GlobalStates, ListOfTimers, Macros }

    private static AdvencedFeaturesWindowType openedWindow = AdvencedFeaturesWindowType.ListOfTimers;
  
    private static AdvencedFeaturesWindowGlobalStates advencedFeaturesWindowGlobalStates;
    private static AdvencedFeaturesWindowListOfTimers advencedFeaturesWindowListOfTimers;
    private static AdvencedFeaturesWindowMacros advencedFeaturesWindowMacros;


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

    public AdvencedFeaturesWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        advencedFeaturesWindowGlobalStates = new AdvencedFeaturesWindowGlobalStates(aStartPos, new GUIContent(Language.GetText("GLOBAL_STATES")), "Window");
        advencedFeaturesWindowListOfTimers = new AdvencedFeaturesWindowListOfTimers(aStartPos, new GUIContent(Language.GetText("LIST_OF_TIMERS")), "Window");
        advencedFeaturesWindowMacros = new AdvencedFeaturesWindowMacros(aStartPos, new GUIContent(Language.GetText("MACROS")), "Window");
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
            if (GUILayout.Button(Language.GetText("GLOBAL_STATES")))
            {
                OnWindowTypeChanged(AdvencedFeaturesWindowType.GlobalStates);
            }
            if (GUILayout.Button(Language.GetText("LIST_OF_TIMERS")))
            {
                OnWindowTypeChanged(AdvencedFeaturesWindowType.ListOfTimers);
            }
            if (GUILayout.Button(Language.GetText("MACROS")))
            {
                OnWindowTypeChanged(AdvencedFeaturesWindowType.Macros);
            }
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case AdvencedFeaturesWindowType.GlobalStates:
                    advencedFeaturesWindowGlobalStates.Draw(aID);
                    break;
                case AdvencedFeaturesWindowType.ListOfTimers:
                    advencedFeaturesWindowListOfTimers.Draw(aID);
                    break;
                case AdvencedFeaturesWindowType.Macros:
                    advencedFeaturesWindowMacros.Draw(aID);
                    break;
            }
        }
        else
        {
            GUILayout.Label("AdvencedFeaturesWindow");
        }
    }

    void OnWindowTypeChanged(AdvencedFeaturesWindowType type_)
    {
        openedWindow = type_;
    }
}
