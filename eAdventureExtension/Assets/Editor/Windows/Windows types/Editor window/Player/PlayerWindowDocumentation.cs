using UnityEngine;
using System.Collections;

public class PlayerWindowDocumentation : LayoutWindow
{
    public PlayerWindowDocumentation(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {

    }


    public override void Draw(int aID)
    {
        GUILayout.Label("PlayerWindowDocumentation");
    }

}