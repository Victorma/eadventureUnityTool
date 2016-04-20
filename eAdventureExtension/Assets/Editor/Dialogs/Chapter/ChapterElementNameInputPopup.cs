using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text.RegularExpressions;

public class ChapterElementNameInputPopup : BaseInputPopup
{
    public EditorWindowBase.EditorWindowType connectedAsssetType;

    public void Init(DialogReceiverInterface e, string startTextContent, EditorWindowBase.EditorWindowType type)
    {
        connectedAsssetType = type;
        base.Init(e, startTextContent);
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Type the name of " + Enum.GetName(typeof(EditorWindowBase.EditorWindowType), connectedAsssetType) +": ", EditorStyles.wordWrappedLabel);

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
