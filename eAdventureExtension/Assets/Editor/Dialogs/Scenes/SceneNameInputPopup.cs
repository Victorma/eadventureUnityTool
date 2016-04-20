using UnityEngine;
using System.Collections;
using UnityEditor;

public class SceneNameInputPopup : BaseInputPopup
{

    void OnGUI()
    {
        EditorGUILayout.LabelField("Type the name of scene: ", EditorStyles.wordWrappedLabel);

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
