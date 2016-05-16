using UnityEngine;
using UnityEditor;
using System;

public class ExitNewLinkTo : BaseChooseObjectPopup
{

    public override void Init(DialogReceiverInterface e)
    {
        elements = Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenesIDs();
        selectedElementID = elements[0];

        base.Init(e);
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Select exit scene target: ", EditorStyles.boldLabel);

        GUILayout.Space(20);

        selectedElementID = elements[EditorGUILayout.Popup(Array.IndexOf(elements, selectedElementID), elements)];

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("OK"))
        {
            reference.OnDialogOk(selectedElementID, this);
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
