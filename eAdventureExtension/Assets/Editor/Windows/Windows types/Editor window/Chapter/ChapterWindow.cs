using UnityEngine;
using System.Collections;
using UnityEditor;

public class ChapterWindow : LayoutWindow
{
    private string chapterName, descriptionOfGame, chapterNameLast, descriptionOfGameLast;
    private Texture2D clearImg = null;
    private Vector2 scrollPosition = Vector2.zero;
    private int selAdaptation, selAssesment, selInitialScene, selAdaptationLast, selAssesmentLast, selInitialSceneLast;
    private string[] selStringsAdapatation, selStringsAssesment, selStringsInitialScene;
    private float windowHeight;

    public ChapterWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        Chapter chapter = Controller.getInstance().getCharapterList().getSelectedChapterData();
        chapterName = chapterNameLast = chapter.getTitle();
        descriptionOfGame = descriptionOfGameLast = chapter.getDescription();
        selAdaptation = selAdaptationLast = 0;
        selAssesment = selAssesmentLast = 0;
        selInitialScene = selInitialSceneLast = 0;

        selStringsAdapatation = new string[] { "none", "test1 Adaptation", "test2 Adaptation", "test3 Adaptation", "test4 Adaptation", "test5 Adaptation" };
        selStringsAssesment = new string[] { "none", "test1 Assesment", "test2 Assesment" };

        int amountOfScenes = Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes().Count;
        selStringsInitialScene = new string[amountOfScenes];
        for (int i = 0; i < amountOfScenes; i++)
        {
            selStringsInitialScene[i] = Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[i].getId();
            // Set index for selction grid
            if (selStringsInitialScene[i] == Controller.getInstance().getCharapterList().getSelectedChapterDataControl().getInitialScene())
                selInitialScene = i;
        }

        clearImg = (Texture2D)Resources.Load("EAdventureData/img/icons/deleteContent", typeof(Texture2D));

        windowHeight = aStartPos.height;
    }


    public override void Draw(int aID)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label(Language.GetText("TITLE_OF_CHAPTER"));
        chapterName = GUILayout.TextField(chapterName);
        if(!chapterName.Equals(chapterNameLast))
            ChangeTitle(chapterName);

        GUILayout.Space(20);

        GUILayout.Label(Language.GetText("DESCRIPTION_OF_CHAPTER"));
        descriptionOfGame = GUILayout.TextArea(descriptionOfGame, GUILayout.MinHeight(0.2f * windowHeight));
        if (!descriptionOfGame.Equals(descriptionOfGameLast))
            ChangeDescription(descriptionOfGame);

        GUILayout.Space(20);

        GUILayout.Label(Language.GetText("ASSESMENT_FILE_CHAPTER"));
        GUILayout.BeginHorizontal();
        // Change to none
        if (GUILayout.Button(clearImg))
        {
            selAssesment = 0;
        }
        selAssesment = EditorGUILayout.Popup(selAssesment, selStringsAssesment);
        if(selAssesment != selAssesmentLast)
            ChangeSelectedAssessment(selAssesment);
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.Label(Language.GetText("ADAPTATION_FILE_CHAPTER"));
        GUILayout.BeginHorizontal();
        // Change to none
        if (GUILayout.Button(clearImg))
        {
            selAdaptation = 0;
        }
        selAdaptation = EditorGUILayout.Popup(selAdaptation, selStringsAdapatation);
        if (selAdaptation != selAdaptationLast)
            ChangeSelectedAdaptation(selAdaptation);
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.Label(Language.GetText("INITIAL_SCENE_CHAPTER"));
        selInitialScene = EditorGUILayout.Popup(selInitialScene, selStringsInitialScene);
        if (selInitialScene != selInitialSceneLast)
            ChangeSelectedInitialScene(selInitialScene);
        GUILayout.EndScrollView();
    }

    private void ChangeTitle(string s)
    {
        Controller.getInstance().getCharapterList().getSelectedChapterDataControl().setTitle(s);
        chapterNameLast = s;
        Debug.Log("ChangeTitle ");
    }

    private void ChangeDescription(string s)
    {
        Controller.getInstance().getCharapterList().getSelectedChapterDataControl().setDescription(s);
        descriptionOfGameLast = s;
        Debug.Log("ChangeDescription ");
    }

    private void ChangeSelectedAdaptation(int i)
    {
        //TODO:
        selAdaptationLast = i;
        Debug.Log("ChangeSelectedAdaptation ");
    }

    private void ChangeSelectedAssessment(int i)
    {
        //TODO:
        selAssesmentLast = i;
        Debug.Log("ChangeSelectedAssessment ");
    }

    private void ChangeSelectedInitialScene(int i)
    {
        selInitialSceneLast = i;
        Controller.getInstance()
            .getCharapterList()
            .getSelectedChapterDataControl()
            .setInitialScene(
                Controller.getInstance().getCharapterList().getSelectedChapterData().getCutscenes()[i].getId());
    }
}