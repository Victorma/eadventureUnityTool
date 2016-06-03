using UnityEngine;
using System.Collections;

public class ScenesWindowDocumentation : LayoutWindow
{
    private string descriptionOfScene, nameOfScene, descriptionOfSceneLast, nameOfSceneLast;
    private float windowHeight;

    public ScenesWindowDocumentation(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        string doc = "";

        if (GameRources.GetInstance().selectedSceneIndex >= 0)
            doc = Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getDocumentation();
        string name = "";

        if (GameRources.GetInstance().selectedSceneIndex >= 0)
            name = Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[GameRources.GetInstance().selectedSceneIndex].getName();

        doc = (doc == null ? "" : doc);
        name = (name == null ? "" : name);

        descriptionOfScene = descriptionOfSceneLast = doc;
        nameOfScene = nameOfSceneLast = name;

        windowHeight = aStartPos.height;
    }

    public override void Draw(int aID)
    {
        GUILayout.Label(TC.get("Scene.Documentation"));
        descriptionOfScene = GUILayout.TextArea(descriptionOfScene, GUILayout.MinHeight(0.4f * windowHeight));
        if (!descriptionOfScene.Equals(descriptionOfSceneLast))
            ChangeDocumentation(descriptionOfScene);

        GUILayout.Space(30);

        GUILayout.Label(TC.get("Scene.Name"));
        nameOfScene = GUILayout.TextField(nameOfScene);
        if (!nameOfScene.Equals(nameOfSceneLast))
            ChangeName(nameOfScene);
    }

    private void ChangeName(string s)
    {
        Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[GameRources.GetInstance().selectedSceneIndex].setName(s);
        nameOfSceneLast = s;
    }

    private void ChangeDocumentation(string s)
    {
        Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[GameRources.GetInstance().selectedSceneIndex].setDocumentation(s);
        descriptionOfSceneLast = s;
    }
}