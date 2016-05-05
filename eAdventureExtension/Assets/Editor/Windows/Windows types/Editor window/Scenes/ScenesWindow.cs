using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ScenesWindow : LayoutWindow
{
    private enum ScenesWindowType
    {
        ActiveAreas,
        Appearance,
        Documentation,
        ElementRefrence,
        Exits, 
        Barriers,
        PlayerMovement
    }

    private static ScenesWindowType openedWindow = ScenesWindowType.Appearance;
    private static ScenesWindowActiveAreas scenesWindowActiveAreas;
    private static ScenesWindowAppearance scenesWindowAppearance;
    private static ScenesWindowDocumentation scenesWindowDocumentation;
    private static ScenesWindowElementReference scenesWindowElementReference;
    private static ScenesWindowExits scenesWindowExits;
    private static ScenesWindowBarriers scenesWindowBarriers;
    private static ScenesWindowPlayerMovement scenesWindowPlayerMovement;

    private static float windowWidth, windowHeight;
    private static List<bool> toggleList;

    private static Rect thisRect;

    // Flag determining visibility of concrete item information
    private bool isConcreteItemVisible = false;

    public ScenesWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        thisRect = aStartPos;
        scenesWindowActiveAreas = new ScenesWindowActiveAreas(aStartPos,
            new GUIContent(Language.GetText("ACTIVE_AREAS")), "Window");
        scenesWindowAppearance = new ScenesWindowAppearance(aStartPos, new GUIContent(Language.GetText("APPEARANCE")),
            "Window");
        scenesWindowDocumentation = new ScenesWindowDocumentation(aStartPos,
            new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
        scenesWindowElementReference = new ScenesWindowElementReference(aStartPos,
            new GUIContent(Language.GetText("ELEMENT_REFERENCES")), "Window");
        scenesWindowExits = new ScenesWindowExits(aStartPos, new GUIContent(Language.GetText("EXITS")), "Window");

        scenesWindowBarriers = new ScenesWindowBarriers(aStartPos, new GUIContent("Barriers"), "Window");
        scenesWindowPlayerMovement = new ScenesWindowPlayerMovement(aStartPos, new GUIContent("Player movement"), "Window");


        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        GenerateToggleList();
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
                OnWindowTypeChanged(ScenesWindowType.Appearance);
            }
            if (GUILayout.Button(Language.GetText("DOCUMENTATION")))
            {
                OnWindowTypeChanged(ScenesWindowType.Documentation);
            }
            if (GUILayout.Button(Language.GetText("ELEMENT_REFERENCES")))
            {
                OnWindowTypeChanged(ScenesWindowType.ElementRefrence);
            }
            if (GUILayout.Button(Language.GetText("ACTIVE_AREAS")))
            {
                OnWindowTypeChanged(ScenesWindowType.ActiveAreas);
            }
            if (GUILayout.Button(Language.GetText("EXITS")))
            {
                OnWindowTypeChanged(ScenesWindowType.Exits);
            }
            // Only visible for 3rd person
            if (Controller.getInstance().playerMode() == DescriptorData.MODE_PLAYER_3RDPERSON)
            {
                if (GUILayout.Button("Barriers"))
                {
                    OnWindowTypeChanged(ScenesWindowType.Barriers);
                }
                if (GUILayout.Button("Player movement"))
                {
                    OnWindowTypeChanged(ScenesWindowType.PlayerMovement);
                }
            }
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case ScenesWindowType.ActiveAreas:
                    scenesWindowActiveAreas.Draw(aID);
                    break;
                case ScenesWindowType.Appearance:
                    scenesWindowAppearance.Draw(aID);
                    break;
                case ScenesWindowType.Documentation:
                    scenesWindowDocumentation.Draw(aID);
                    break;
                case ScenesWindowType.ElementRefrence:
                    scenesWindowElementReference.Draw(aID);
                    break;
                case ScenesWindowType.Exits:
                    scenesWindowExits.Draw(aID);
                    break;
                case ScenesWindowType.Barriers:
                    scenesWindowBarriers.Draw(aID);
                    break;
                case ScenesWindowType.PlayerMovement:
                    scenesWindowPlayerMovement.Draw(aID);
                    break;
            }
        }
        // Show information of whole scenes (global-scene view)
        else
        {
            GUILayout.Label(Language.GetText("SCENES"));

            GUILayout.BeginHorizontal();
            GUILayout.Box(Language.GetText("SHOW_?"), GUILayout.MaxWidth(windowWidth*0.2f));
            GUILayout.Box(Language.GetText("SCENE_ID"), GUILayout.Width(windowWidth*0.55f));
            GUILayout.Box(Language.GetText("EDIT"), GUILayout.MaxWidth(windowWidth*0.2f));
            GUILayout.EndHorizontal();

            for (int i = 0;
                i < Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes().Count;
                i++)
            {
                GUILayout.BeginHorizontal();
                toggleList[i] = GUILayout.Toggle(toggleList[i], "", GUILayout.MaxWidth(windowWidth*0.2f));
                GUILayout.Label(
                    Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[i].getId(),
                    GUILayout.Width(windowWidth*0.55f));
                if (GUILayout.Button(Language.GetText("EDIT"), GUILayout.MaxWidth(windowWidth*0.2f)))
                {
                    ShowItemWindowView(i);
                }

                GUILayout.EndHorizontal();

            }

        }
    }

    void OnWindowTypeChanged(ScenesWindowType type_)
    {
        openedWindow = type_;
    }


    // Two methods responsible for showing right window content 
    // - concrete item info or base window view
    public void ShowBaseWindowView()
    {
        isConcreteItemVisible = false;
        GameRources.GetInstance().selectedSceneIndex = -1;
        GenerateToggleList();
    }

    public void ShowItemWindowView(int s)
    {
        GameRources.GetInstance().selectedSceneIndex = s;
        isConcreteItemVisible = true;
        // Generate new toogle list - maybe user already created new scenes?
        GenerateToggleList();

        // Reload windows for newly selected scene
        scenesWindowActiveAreas = new ScenesWindowActiveAreas(thisRect, new GUIContent(Language.GetText("ACTIVE_AREAS")),
            "Window");
        scenesWindowAppearance = new ScenesWindowAppearance(thisRect, new GUIContent(Language.GetText("APPEARANCE")),
            "Window");
        scenesWindowDocumentation = new ScenesWindowDocumentation(thisRect,
            new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
        scenesWindowElementReference = new ScenesWindowElementReference(thisRect,
            new GUIContent(Language.GetText("ELEMENT_REFERENCES")), "Window");
        scenesWindowExits = new ScenesWindowExits(thisRect, new GUIContent(Language.GetText("EXITS")), "Window");

        // Only visible for 3rd person
        if (Controller.getInstance().playerMode() == DescriptorData.MODE_PLAYER_3RDPERSON)
        {
            scenesWindowBarriers = new ScenesWindowBarriers(thisRect, new GUIContent("Barriers"), "Window");
            scenesWindowPlayerMovement = new ScenesWindowPlayerMovement(thisRect, new GUIContent("Player movement"), "Window");
        }
    }

    void GenerateToggleList()
    {
        toggleList =
            new List<bool>(Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes().Count);
        for (int i = 0; i < Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes().Count; i++)
            toggleList.Add(true);
    }
}