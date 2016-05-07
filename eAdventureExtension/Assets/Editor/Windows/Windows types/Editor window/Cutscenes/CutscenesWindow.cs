using UnityEngine;
using System.Collections;

public class CutscenesWindow : LayoutWindow
{
    private enum CutscenesWindowType { Appearance, Documentation, EndConfiguration }

    private static CutscenesWindowType openedWindow = CutscenesWindowType.Appearance;
    private static CutscenesWindowAppearance cutscenesWindowAppearance;
    private static CutscenesWindowDocumentation cutscenesWindowDocumentation;
    private static CutscenesWindowEndConfiguration cutscenesWindowEndConfiguration;

    private static float windowWidth, windowHeight;


    // Flag determining visibility of concrete item information
    private bool isConcreteItemVisible = false;

    private static Rect thisRect;

    private static GUISkin selectedButtonSkin;
    private static GUISkin defaultSkin;

    public CutscenesWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {

        cutscenesWindowAppearance = new CutscenesWindowAppearance(aStartPos, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        cutscenesWindowDocumentation = new CutscenesWindowDocumentation(aStartPos, new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
        cutscenesWindowEndConfiguration = new CutscenesWindowEndConfiguration(aStartPos, new GUIContent(Language.GetText("CUTSCENES_AND_CONFIGURATION")), "Window");

        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        thisRect = aStartPos;
        selectedButtonSkin = (GUISkin)Resources.Load("Editor/ButtonSelected", typeof(GUISkin));
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
            if (openedWindow == CutscenesWindowType.Appearance)
                GUI.skin = selectedButtonSkin;
            if (GUILayout.Button(Language.GetText("APPEARANCE")))
            {
                OnWindowTypeChanged(CutscenesWindowType.Appearance);
            }
            if (openedWindow == CutscenesWindowType.Appearance)
                GUI.skin = defaultSkin;

            if (openedWindow == CutscenesWindowType.Documentation)
                GUI.skin = selectedButtonSkin;
            if (GUILayout.Button(Language.GetText("DOCUMENTATION")))
            {
                OnWindowTypeChanged(CutscenesWindowType.Documentation);
            }
            if (openedWindow == CutscenesWindowType.Documentation)
                GUI.skin = defaultSkin;

            if (openedWindow == CutscenesWindowType.EndConfiguration)
                GUI.skin = selectedButtonSkin;
            if (GUILayout.Button(Language.GetText("CUTSCENES_AND_CONFIGURATION")))
            {
                OnWindowTypeChanged(CutscenesWindowType.EndConfiguration);
            }
            if (openedWindow == CutscenesWindowType.EndConfiguration)
                GUI.skin = defaultSkin;
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case CutscenesWindowType.Appearance:
                    cutscenesWindowAppearance.Draw(aID);
                    break;
                case CutscenesWindowType.Documentation:
                    cutscenesWindowDocumentation.Draw(aID);
                    break;
                case CutscenesWindowType.EndConfiguration:
                    cutscenesWindowEndConfiguration.Draw(aID);
                    break;
            }
        }
        else
        {
            GUILayout.Space(30);
            for (int i = 0; i < Controller.getInstance().getCharapterList().getSelectedChapterData().getCutscenes().Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Box(Controller.getInstance().getCharapterList().getSelectedChapterData().getCutscenes()[i].getId(), GUILayout.Width(windowWidth * 0.75f));
                if (GUILayout.Button(Language.GetText("EDIT"), GUILayout.MaxWidth(windowWidth * 0.2f)))
                {
                    ShowItemWindowView(i);
                }

                GUILayout.EndHorizontal();

            }
        }
    }


    void OnWindowTypeChanged(CutscenesWindowType type_)
    {
        openedWindow = type_;
    }


    // Two methods responsible for showing right window content 
    // - concrete item info or base window view
    public void ShowBaseWindowView()
    {
        isConcreteItemVisible = false;
        GameRources.GetInstance().selectedCutsceneIndex = -1;
    }

    public void ShowItemWindowView(int o)
    {
        isConcreteItemVisible = true;
        GameRources.GetInstance().selectedCutsceneIndex = o;

        // Reload windows for newly selected scene
        cutscenesWindowAppearance = new CutscenesWindowAppearance(thisRect, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        cutscenesWindowDocumentation = new CutscenesWindowDocumentation(thisRect, new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
        cutscenesWindowEndConfiguration = new CutscenesWindowEndConfiguration(thisRect, new GUIContent(Language.GetText("CUTSCENES_AND_CONFIGURATION")), "Window");
    }
}
