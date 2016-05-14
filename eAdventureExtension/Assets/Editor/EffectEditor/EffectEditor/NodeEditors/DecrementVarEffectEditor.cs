using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class DecrementVarEffectEditor : EffectEditor
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
    private string[] vars;

    private DecrementVarEffect effect;

    public DecrementVarEffectEditor()
    {
        vars = Controller.getInstance().getVarFlagSummary().getVars();
        this.effect = new DecrementVarEffect(vars[0], 1);
    }

    public void draw()
    {

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Variable to decrement: ");

        effect.setTargetId(vars[EditorGUILayout.Popup(Array.IndexOf(vars, effect.getTargetId()), vars)]);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Variable will be decrement.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as DecrementVarEffect; } }
    public string EffectName { get { return "Decrement var effect"; } }
    public EffectEditor clone() { return new DecrementVarEffectEditor(); }

    public bool manages(AbstractEffect c)
    {

        return c.GetType() == effect.GetType();
    }
}
