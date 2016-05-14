using UnityEngine;
using UnityEditor;
using System;

public class SetValueEffectEditor : EffectEditor
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

    private SetValueEffect effect;

    public SetValueEffectEditor()
    {
        vars = Controller.getInstance().getVarFlagSummary().getVars();
        this.effect = new SetValueEffect(vars[0], 1);
    }

    public void draw()
    {

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Variable to change value: ");
        effect.setTargetId(vars[EditorGUILayout.Popup(Array.IndexOf(vars, effect.getTargetId()), vars)]);
        effect.setValue(EditorGUILayout.IntField(effect.getValue()));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Variable value will be changed.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as SetValueEffect; } }
    public string EffectName { get { return "Set var value effect"; } }
    public EffectEditor clone() { return new SetValueEffectEditor(); }

    public bool manages(AbstractEffect c)
    {
        return c.GetType() == effect.GetType();
    }
}
