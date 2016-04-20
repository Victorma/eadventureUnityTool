using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class ChapterElementNewCutsceneInputPopup : ChapterElementNameInputPopup
{
    public int cutsceneType { get; set; }

    private bool selectedSlide = true;
    private bool selectedVideo = true;

    public void Init(DialogReceiverInterface e, string startTextContent, EditorWindowBase.EditorWindowType type)
    {
        base.Init(e, startTextContent, type);
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Select type of cutscene ");

        selectedSlide = GUILayout.Toggle(!selectedVideo, new GUIContent("Slidescene"));
        selectedVideo = GUILayout.Toggle(!selectedSlide, new GUIContent("Videoscene"));

        GUILayout.Space(50);

        EditorGUILayout.LabelField("Type the name of Cutscene: ", EditorStyles.wordWrappedLabel);

        GUILayout.Space(30);

        textContent = GUILayout.TextField(textContent);

        GUILayout.Space(30);

        GUILayout.BeginHorizontal();
        // Disable button ok if name is not valid
        if (!Controller.getInstance().isElementIdValid(textContent, false))
        {
            GUI.enabled = false;
        }
        if (GUILayout.Button("Ok"))
        {
            if (selectedSlide)
                cutsceneType = Controller.CUTSCENE_SLIDES;
            else
                cutsceneType = Controller.CUTSCENE_VIDEO;

            reference.OnDialogOk(textContent, this);
            this.Close();
        }
        GUI.enabled = true;
        if (GUILayout.Button("Cancel"))
        {
            reference.OnDialogCanceled(this);
            this.Close();
        }
        GUILayout.EndHorizontal();
    }
}
