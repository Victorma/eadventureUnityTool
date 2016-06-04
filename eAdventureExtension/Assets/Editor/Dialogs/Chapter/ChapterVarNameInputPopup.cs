﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class ChapterVarNameInputPopup : BaseInputPopup
{
    void OnGUI()
    {
        EditorGUILayout.LabelField(TC.get("Vars.AddVarMessage"), EditorStyles.wordWrappedLabel);

        GUILayout.Space(30);

        textContent = GUILayout.TextField(textContent);

        GUILayout.Space(30);

        GUILayout.BeginHorizontal();

        if (!Controller.getInstance().isElementIdValid(textContent, false))
        {
            GUI.enabled = false;
        }

        // Disable button ok if name is not valid
        if (GUILayout.Button("OK"))
        {
            reference.OnDialogOk(textContent, this);
            this.Close();
        }
        GUI.enabled = true;
        if (GUILayout.Button(TC.get("GeneralText.Cancel")))
        {
            reference.OnDialogCanceled();
            this.Close();
        }
        GUILayout.EndHorizontal();
    }
}
