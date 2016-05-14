using System;
using UnityEngine;
using UnityEditor;

public class TriggerCutsceneEffectEditor : EffectEditor
{
    private bool collapsed = false;
    public bool Collapsed { get { return collapsed; } set { collapsed = value; } }
    private Rect window = new Rect(0, 0, 300, 0);
    private string[] cutscenes;
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

    private TriggerConversationEffect effect;

    public TriggerCutsceneEffectEditor()
    {
        cutscenes = Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenesIDs();
        this.effect = new TriggerConversationEffect(cutscenes[0]);
    }

    public void draw()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cutscene: ");

        effect.setTargetId(cutscenes[EditorGUILayout.Popup(Array.IndexOf(cutscenes, effect.getTargetId()), cutscenes)]);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Cutscene will be displayed.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as TriggerConversationEffect; } }
    public string EffectName { get { return "Trigger cutscene effect"; } }
    public EffectEditor clone() { return new TriggerCutsceneEffectEditor(); }

    public bool manages(AbstractEffect c)
    {
        return c.GetType() == effect.GetType();
    }
}
