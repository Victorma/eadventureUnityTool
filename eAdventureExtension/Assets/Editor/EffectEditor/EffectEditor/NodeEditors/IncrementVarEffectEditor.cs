using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class IncrementVarEffectEditor : EffectEditor
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

    private IncrementVarEffect effect;

    public IncrementVarEffectEditor()
    {
        vars = Controller.getInstance().getVarFlagSummary().getVars();
        this.effect = new IncrementVarEffect(vars[0], 1);
    }

    public void draw()
    {

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Variable to increment: ");

        effect.setTargetId(vars[EditorGUILayout.Popup(Array.IndexOf(vars, effect.getTargetId()), vars)]);
        effect.setIncrement(EditorGUILayout.IntField(effect.getIncrement()));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Variable will be increment.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as IncrementVarEffect; } }
    public string EffectName { get { return "Decrement var effect"; } }
    public EffectEditor clone() { return new IncrementVarEffectEditor(); }

    public bool manages(AbstractEffect c)
    {

        return c.GetType() == effect.GetType();
    }
}
