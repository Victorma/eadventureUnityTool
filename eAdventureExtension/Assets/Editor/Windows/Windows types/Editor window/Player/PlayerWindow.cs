using UnityEngine;
using System.Collections;

public class PlayerWindow : LayoutWindow
{
    private enum PlayerWindowType { DialogConfiguration, Documentation }

    private static PlayerWindowType openedWindow = PlayerWindowType.DialogConfiguration;
    private static PlayerWindowDialogConfiguration playerWindowDialogConfiguration;
    private static PlayerWindowDocumentation playerWindowDocumentation;

    private Player playerRef = null;
    
    public PlayerWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        playerWindowDialogConfiguration = new PlayerWindowDialogConfiguration(aStartPos, new GUIContent(Language.GetText("DIALOG_CONFIGURATION")), "Window");
        playerWindowDocumentation = new PlayerWindowDocumentation(aStartPos, new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
    }


    public override void Draw(int aID)
    {
      
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(Language.GetText("DIALOG_CONFIGURATION")))
            {
                OnWindowTypeChanged(PlayerWindowType.DialogConfiguration);
            }
            if (GUILayout.Button(Language.GetText("DOCUMENTATION")))
            {
                OnWindowTypeChanged(PlayerWindowType.Documentation);
            }
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case PlayerWindowType.DialogConfiguration:
                    playerWindowDialogConfiguration.Draw(aID);
                    break;
                case PlayerWindowType.Documentation:
                    playerWindowDocumentation.Draw(aID);
                    break;
            }
    }

    void OnWindowTypeChanged(PlayerWindowType type_)
    {
        openedWindow = type_;
    }
}