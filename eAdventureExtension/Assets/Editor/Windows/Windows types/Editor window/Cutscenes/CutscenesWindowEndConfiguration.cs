using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor;

// needed for Regex

public class CutscenesWindowEndConfiguration : LayoutWindow
{
    private string[] possibleOptions;
    private int selectedOption, selectedOptionLast;

    private string[] transitionTypes;
    private int selectedTransitionType, selectedTransitionTypeLast;

    private string[] scenesNextNames;
    private string[] cutscenesNextNames;
    private string[] joinedNextScenesList;
    private int selectedSceneNext, selectedSceneNextLast;

    private static float windowWidth, windowHeight;
    private Rect selectorRect, goesToNewSceneRect;

    private string timeString, timeStringLast;
    private int timeInt;


    public CutscenesWindowEndConfiguration(Rect aStartPos, GUIContent aContent, GUIStyle aStyle,
        params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        possibleOptions = new string[] {"Return to the previous scene", "Chapter ends", "Goes to a new scene"};

        transitionTypes = new string[]
        {"No transition", "Top to bottom", "Bottom to top", "Left to right", "Right to left", "Fade in"};

        scenesNextNames = Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenesIDs();
        cutscenesNextNames = Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenesIDs();
        // Both scenes and cutscenes are necessary for next scene popup
        joinedNextScenesList = new string[scenesNextNames.Length + cutscenesNextNames.Length];
        scenesNextNames.CopyTo(joinedNextScenesList, 0);
        cutscenesNextNames.CopyTo(joinedNextScenesList, scenesNextNames.Length);

        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        selectorRect = new Rect(0f, 0.2f*windowHeight, 0.9f*windowWidth, 0.2f*windowHeight);
        goesToNewSceneRect = new Rect(0.2f*windowWidth, 0.35f*windowHeight, 0.6f*windowWidth, 0.35f*windowHeight);

        selectedOption = selectedOptionLast = 0;
        selectedSceneNext = selectedSceneNextLast = 0;
        timeInt = 0;

        if (GameRources.GetInstance().selectedCutsceneIndex >= 0)
        {
            selectedOption =
                selectedOptionLast =
                    Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                        GameRources.GetInstance().selectedCutsceneIndex].getNext();

            timeInt = Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                GameRources.GetInstance().selectedCutsceneIndex].getTransitionTime();

            selectedTransitionType = selectedTransitionTypeLast =
                Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                    GameRources.GetInstance().selectedCutsceneIndex].getTransitionType();

            string nextSceneID =
                Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                    GameRources.GetInstance().selectedCutsceneIndex].getNextSceneId();

            selectedSceneNext =
                selectedSceneNextLast =
                    Controller.getInstance()
                        .getSelectedChapterDataControl()
                        .getScenesList()
                        .getSceneIndexByID(nextSceneID);
            // if next scene is not a scene, but a cutscene...
            if(selectedSceneNext == -1)
            {
                selectedSceneNext =
                 selectedSceneNextLast =
                 Controller.getInstance()
                     .getSelectedChapterDataControl()
                     .getCutscenesList()
                     .getCutsceneIndexByID(nextSceneID) + scenesNextNames.Length;
            }
        }

        timeString = timeStringLast = timeInt.ToString();
    }


    public override void Draw(int aID)
    {
        GUILayout.Label("When the cutscene end is reached...");
        GUILayout.Space(20);
        
        GUILayout.BeginArea(selectorRect);
        selectedOption = GUILayout.SelectionGrid(selectedOption, possibleOptions, 3, GUILayout.Width(0.9f*windowWidth));
        if (selectedOption != selectedOptionLast)
            ChangeSelectedOption(selectedOption);
        GUILayout.EndArea();
        
        GUILayout.BeginArea(goesToNewSceneRect);
        if (selectedOption == 2)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Next scene");

            selectedSceneNext = EditorGUILayout.Popup(selectedSceneNext, joinedNextScenesList);
            if(selectedSceneNext != selectedSceneNextLast)
                ChangeSelectedNextScene(selectedSceneNext);

            if (GUILayout.Button("Edit effects"))
            {
                EffectEditorWindow window =
                        (EffectEditorWindow)ScriptableObject.CreateInstance(typeof(EffectEditorWindow));
                window.Init(Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                        GameRources.GetInstance().selectedCutsceneIndex].getEffects());
            }
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            GUILayout.Label("Transition");

            selectedTransitionType = EditorGUILayout.Popup(selectedTransitionType, transitionTypes);
            if (selectedTransitionType != selectedTransitionTypeLast)
                ChangeSelectedTransitionType(selectedTransitionType);

            GUILayout.Label("Transition time");
            timeString = GUILayout.TextField(timeString, 3);
            timeString = Regex.Replace(timeString, @"[^0-9 ]", "");
            if(!timeString.Equals(timeStringLast))
                ChangeSelectedTransitionTime(timeString);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();
    }

    private void ChangeSelectedOption(int i)
    {
        selectedOptionLast = i;
        Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                  GameRources.GetInstance().selectedCutsceneIndex].setNext(i);
        Debug.Log("ChangeSelectedOption");
    }

    private void ChangeSelectedTransitionType(int i)
    {
        selectedTransitionTypeLast = i;
        Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                  GameRources.GetInstance().selectedCutsceneIndex].setTransitionType(i);
        Debug.Log("ChangeSelectedTransitionType");
    }

    private void ChangeSelectedTransitionTime(string t)
    {
        timeStringLast = t;
        Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                  GameRources.GetInstance().selectedCutsceneIndex].setTransitionTime(int.Parse(t));
        Debug.Log("ChangeSelectedTransitionTime");
    }

    private void ChangeSelectedNextScene(int i)
    {
        selectedSceneNextLast = i;
        // Scene was choosed
        if (i < scenesNextNames.Length)
            Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                GameRources.GetInstance().selectedCutsceneIndex].setNextSceneId(scenesNextNames[i]);
        else
            Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                GameRources.GetInstance().selectedCutsceneIndex].setNextSceneId(cutscenesNextNames[i-scenesNextNames.Length]);
        Debug.Log("ChangeSelectedNextScene");
    }
}