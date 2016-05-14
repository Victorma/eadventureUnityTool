using System;
using UnityEngine;
using UnityEditor;

public class TriggerLastSceneEffectEditor : EffectEditor
{
    private bool collapsed = false;
    public bool Collapsed { get { return collapsed; } set { collapsed = value; } }
    private Rect window = new Rect(0, 0, 300, 0);
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

    private TriggerLastSceneEffect effect;

    public TriggerLastSceneEffectEditor()
    {
        this.effect = new TriggerLastSceneEffect();
    }

    public void draw()
    {
        EditorGUILayout.HelpBox("Trigger previous scene.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as TriggerLastSceneEffect; } }
    public string EffectName { get { return "Trigger last scene effect"; } }
    public EffectEditor clone() { return new TriggerLastSceneEffectEditor(); }

    public bool manages(AbstractEffect c)
    {
        return c.GetType() == effect.GetType();
    }
}
