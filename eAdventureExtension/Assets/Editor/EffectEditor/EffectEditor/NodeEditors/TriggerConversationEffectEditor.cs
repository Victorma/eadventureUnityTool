using System;
using UnityEngine;
using UnityEditor;

public class TriggerConversationEffectEditor : EffectEditor
{
    private bool collapsed = false;
    public bool Collapsed { get { return collapsed; } set { collapsed = value; } }
    private Rect window = new Rect(0, 0, 300, 0);
    private string[] conversations;
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

    public TriggerConversationEffectEditor()
    {
        conversations = Controller.getInstance().getSelectedChapterDataControl().getConversationsList().getConversationsIDs();
        this.effect = new TriggerConversationEffect(conversations[0]);
    }

    public void draw()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Conversation: ");

        effect.setTargetId(conversations[EditorGUILayout.Popup(Array.IndexOf(conversations, effect.getTargetId()), conversations)]);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Conversation will be displayed.", MessageType.Info);
    }

    public AbstractEffect Effect { get { return effect; } set { effect = value as TriggerConversationEffect; } }
    public string EffectName { get { return "Trigger conversation effect"; } }
    public EffectEditor clone() { return new TriggerConversationEffectEditor(); }

    public bool manages(AbstractEffect c)
    {
        return c.GetType() == effect.GetType();
    }
}
