using UnityEngine;
using System.Collections;
using UnityEditor;

public class NewGameInputPopup : BaseInputPopup
{
    void OnGUI()
    {
        EditorGUILayout.LabelField("Type the name of new game", EditorStyles.boldLabel);

        GUILayout.Space(30);

        textContent = GUILayout.TextField(textContent);

        GUILayout.Space(30);

        GUILayout.BeginHorizontal();
        //TODO: validacja
        // Disable button ok if name is not valid
        //if (!Controller.getInstance().isElementIdValid(textContent, false))
        //{
        //    GUI.enabled = false;
        //}
        if (GUILayout.Button("Ok"))
        {
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
