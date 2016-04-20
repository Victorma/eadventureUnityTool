using UnityEngine;
using System.Collections;

public class ConversationWindow : LayoutWindow
{
    public ConversationWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {

    }


    public override void Draw(int aID)
    {
        GUILayout.Label("ConversationWindow");
    }

}