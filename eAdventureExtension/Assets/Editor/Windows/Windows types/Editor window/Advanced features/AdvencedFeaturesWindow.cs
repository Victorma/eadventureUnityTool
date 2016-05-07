using UnityEngine;
using System.Collections;

public class AdvencedFeaturesWindow : LayoutWindow
{
    private enum AdvencedFeaturesWindowType
    {
        GlobalStates,
        ListOfTimers,
        Macros
    }

    private static GUISkin selectedButtonSkin;
    private static GUISkin defaultSkin;

    private static AdvencedFeaturesWindowType openedWindow = AdvencedFeaturesWindowType.ListOfTimers;

    private static AdvencedFeaturesWindowGlobalStates advencedFeaturesWindowGlobalStates;
    private static AdvencedFeaturesWindowListOfTimers advencedFeaturesWindowListOfTimers;
    private static AdvencedFeaturesWindowMacros advencedFeaturesWindowMacros;

    public AdvencedFeaturesWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle,
        params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        advencedFeaturesWindowGlobalStates = new AdvencedFeaturesWindowGlobalStates(aStartPos,
            new GUIContent(Language.GetText("GLOBAL_STATES")), "Window");
        advencedFeaturesWindowListOfTimers = new AdvencedFeaturesWindowListOfTimers(aStartPos,
            new GUIContent(Language.GetText("LIST_OF_TIMERS")), "Window");
        advencedFeaturesWindowMacros = new AdvencedFeaturesWindowMacros(aStartPos,
            new GUIContent(Language.GetText("MACROS")), "Window");
        selectedButtonSkin = (GUISkin)Resources.Load("Editor/ButtonSelected", typeof(GUISkin));
    }


    public override void Draw(int aID)
    {

        /**
            UPPER MENU
            */
        GUILayout.BeginHorizontal();

        if (openedWindow == AdvencedFeaturesWindowType.ListOfTimers)
            GUI.skin = selectedButtonSkin;
        if (GUILayout.Button(Language.GetText("LIST_OF_TIMERS")))
        {
            OnWindowTypeChanged(AdvencedFeaturesWindowType.ListOfTimers);
        }
        if (openedWindow == AdvencedFeaturesWindowType.ListOfTimers)
            GUI.skin = defaultSkin;

        if (openedWindow == AdvencedFeaturesWindowType.GlobalStates)
            GUI.skin = selectedButtonSkin;
        if (GUILayout.Button(Language.GetText("GLOBAL_STATES")))
        {
            OnWindowTypeChanged(AdvencedFeaturesWindowType.GlobalStates);
        }
        if (openedWindow == AdvencedFeaturesWindowType.GlobalStates)
            GUI.skin = defaultSkin;

        if (openedWindow == AdvencedFeaturesWindowType.Macros)
            GUI.skin = selectedButtonSkin;
        if (GUILayout.Button(Language.GetText("MACROS")))
        {
            OnWindowTypeChanged(AdvencedFeaturesWindowType.Macros);
        }
        if (openedWindow == AdvencedFeaturesWindowType.Macros)
            GUI.skin = defaultSkin;
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


    void OnWindowTypeChanged(AdvencedFeaturesWindowType type_)
    {
        openedWindow = type_;
    }
}