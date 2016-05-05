using UnityEngine;
using System.Collections;

public class ScenesWindowPlayerMovement : LayoutWindow
{

    public ScenesWindowPlayerMovement(Rect aStartPos, GUIContent aContent, GUIStyle aStyle,
        params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
    }

    public override void Draw(int aID)
    {
    }
}
