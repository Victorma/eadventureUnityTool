using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class ConsumeObjectEffectEditor : EffectEditor
{
    private bool collapsed = false;
    public bool Collapsed { get { return collapsed; } set { collapsed = value; } }
    private Rect window = new Rect(0, 0, 300, 0);
    private string[] items;
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

    private ConsumeObjectEffect effect;

    public ConsumeObjectEffectEditor()
    {
        items = Controller.getInstance().getSelectedChapterDataControl().getItemsList().getItemsIDs();
        this.effect = new ConsumeObjectEffect("");
    }

    public void draw()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item: ");

        effect.setTargetId(items[EditorGUILayout.Popup(Array.IndexOf(items, effect.getTargetId()), items)]);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Object will be consumed.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as ConsumeObjectEffect; } }
    public string EffectName { get { return "Consume object effect"; } }
    public EffectEditor clone() { return new ConsumeObjectEffectEditor(); }

    public bool manages(AbstractEffect c)
    {
        return c.GetType() == effect.GetType();
    }
}
