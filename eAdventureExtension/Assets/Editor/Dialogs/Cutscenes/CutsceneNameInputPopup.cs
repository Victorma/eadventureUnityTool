using UnityEngine;
using System.Collections;
using UnityEditor;

public class CutsceneNameInputPopup : BaseInputPopup
{
    private bool isCharacterCutscene = false;
    private CharactersWindowAppearance.CharacterAnimationType type;

    public void Init(DialogReceiverInterface e, string startTextContent, System.Object characterAnimType = null)
    {
        if (characterAnimType is CharactersWindowAppearance.CharacterAnimationType)
        {
            isCharacterCutscene = true;
            type = (CharactersWindowAppearance.CharacterAnimationType) type;
        }

        base.Init(e, startTextContent);
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Please write filename for the animation (without extension) ",
            EditorStyles.wordWrappedLabel);

        GUILayout.Space(30);

        textContent = GUILayout.TextField(textContent);

        GUILayout.Space(30);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("OK"))
        {
            if (isCharacterCutscene)
                reference.OnDialogOk(textContent, this, type);
            else
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