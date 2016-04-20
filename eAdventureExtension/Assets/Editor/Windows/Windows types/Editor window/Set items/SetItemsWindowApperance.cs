using UnityEngine;
using System.Collections;

public class SetItemsWindowApperance : LayoutWindow
{
    public SetItemsWindowApperance(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {

    }


    public override void Draw(int aID)
    {
        GUILayout.Label("SetItemsWindowApperance");
    }

}