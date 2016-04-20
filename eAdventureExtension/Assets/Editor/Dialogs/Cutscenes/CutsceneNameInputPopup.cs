using UnityEngine;
using System.Collections;
using UnityEditor;

public class CutsceneNameInputPopup : BaseInputPopup
{

    void OnGUI()
    {
        EditorGUILayout.LabelField("Please write filename for the animation (without extension) ", EditorStyles.wordWrappedLabel);

        GUILayout.Space(30);

        textContent = GUILayout.TextField(textContent);

        GUILayout.Space(30);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("OK"))
        {
            reference.OnDialogOk(textContent, this);
            this.Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            reference.OnDialogCanceled();
            this.Close();
        }
        GUILayout.EndHorizontal();
    }
}
