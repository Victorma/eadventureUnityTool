using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class HighlightItemEffectEditor : EffectEditor
{
    private bool collapsed = false;
    public bool Collapsed { get { return collapsed; } set { collapsed = value; } }
    private Rect window = new Rect(0, 0, 300, 0);
    private string[] items;
    private string[] higlightTypes = {"No highlight", "Blue highlight", "Red highlight", "Green highlight", "Highlight borders"};

    public Rect Window
    {
        get
        {
            if (collapsed) return new Rect(window.x, window.y, 50, 30);
            else return window;
        }
        set
        {
            if (collapsed) window = new Rect(value.x, value.y, window.width, window.height);
            else window = value;
        }
    }

    private HighlightItemEffect effect;

    public HighlightItemEffectEditor()
    {
        items = Controller.getInstance().getSelectedChapterDataControl().getItemsList().getItemsIDs();
        this.effect = new HighlightItemEffect(items[0], 0, false);
    }

    public void draw()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item: ");
        effect.setTargetId(items[EditorGUILayout.Popup(Array.IndexOf(items, effect.getTargetId()), items)]);
        effect.setHighlightType(EditorGUILayout.Popup(Array.IndexOf(higlightTypes, effect.getHighlightType()), higlightTypes));
        effect.setHighlightAnimated(GUILayout.Toggle(effect.isHighlightAnimated(), "Animated"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Object will be higlighted.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as HighlightItemEffect; } }
    public string EffectName { get { return "Highlight object effect"; } }
    public EffectEditor clone() { return new HighlightItemEffectEditor(); }

    public bool manages(AbstractEffect c)
    {
        return c.GetType() == effect.GetType();
    }
}
