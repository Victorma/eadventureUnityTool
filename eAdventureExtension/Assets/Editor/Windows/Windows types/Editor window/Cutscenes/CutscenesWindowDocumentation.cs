using UnityEngine;
using System.Collections;

public class CutscenesWindowDocumentation : LayoutWindow
{
    private string descriptionOfCutscene, nameOfCutscene, descriptionOfCutsceneLast, nameOfCutsceneLast;
    private float windowHeight;

    public CutscenesWindowDocumentation(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        string doc = "";

        if (GameRources.GetInstance().selectedCutsceneIndex >= 0)
            doc = Controller.getInstance().getCharapterList().getSelectedChapterData().getCutscenes()[
                GameRources.GetInstance().selectedCutsceneIndex].getDocumentation();
        string name = "";

        if (GameRources.GetInstance().selectedCutsceneIndex >= 0)
            name = Controller.getInstance().getCharapterList().getSelectedChapterData().getCutscenes()[GameRources.GetInstance().selectedCutsceneIndex].getName();

        doc = (doc == null ? "" : doc);
        name = (name == null ? "" : name);

        descriptionOfCutscene = descriptionOfCutsceneLast = doc;
        nameOfCutscene = nameOfCutsceneLast = name;

        windowHeight = aStartPos.height;
    }


    public override void Draw(int aID)
    {
        //TODO: text from language file
        GUILayout.Label("Full description of the cutscene");
        descriptionOfCutscene = GUILayout.TextArea(descriptionOfCutscene, GUILayout.MinHeight(0.4f * windowHeight));
        if (!descriptionOfCutscene.Equals(descriptionOfCutsceneLast))
            ChangeDocumentation(descriptionOfCutscene);

        GUILayout.Space(30);

        //TODO: text from language file
        GUILayout.Label("Name of the cutscene");
        nameOfCutscene = GUILayout.TextField(nameOfCutscene);
        if (!nameOfCutscene.Equals(nameOfCutsceneLast))
            ChangeName(nameOfCutscene);
    }

    private void ChangeName(string s)
    {
        Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[GameRources.GetInstance().selectedCutsceneIndex].setName(s);
        nameOfCutsceneLast = s;
    }

    private void ChangeDocumentation(string s)
    {
        Controller.getInstance().getCharapterList().getSelectedChapterData().getScenes()[GameRources.GetInstance().selectedCutsceneIndex].setDocumentation(s);
        descriptionOfCutsceneLast = s;
    }
}