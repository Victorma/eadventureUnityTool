using UnityEngine;
using System.Collections;

public class BooksWindowAppearance : LayoutWindow
{
    public BooksWindowAppearance(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
    }

    public override void Draw(int aID)
    {
        GUILayout.Label("BooksWindowAppearance");
    }
}
