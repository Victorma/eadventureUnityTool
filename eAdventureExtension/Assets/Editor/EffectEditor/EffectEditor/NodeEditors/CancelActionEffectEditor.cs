using UnityEngine;
using UnityEditor;
using System.Collections;

public class CancelActionEffectEditor : EffectEditor
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

    private CancelActionEffect effect;

    public CancelActionEffectEditor()
    {
        this.effect = new CancelActionEffect();
    }

    public void draw()
    {
        EditorGUILayout.HelpBox("Cancels action.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as CancelActionEffect; } }
    public string EffectName { get { return "Cancel action effect"; } }
    public EffectEditor clone() { return new CancelActionEffectEditor(); }

    public bool manages(AbstractEffect c)
    {

        return c.GetType() == effect.GetType();
    }
}
