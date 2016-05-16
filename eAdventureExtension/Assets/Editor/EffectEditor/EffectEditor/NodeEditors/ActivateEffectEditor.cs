using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class ActivateEffectEditor : EffectEditor {
    private bool collapsed = false;
    public bool Collapsed { get { return collapsed; } set { collapsed = value; } }
    private Rect window = new Rect(0, 0, 300, 0);
    private string[] flags;
    public Rect Window
    {
        get {
            if (collapsed) return new Rect(window.x, window.y, 50, 30);
            else           return window; 
        }
        set {
            if (collapsed) window = new Rect(value.x, value.y, window.width, window.height);
            else           window = value; 
        }
    }

    private ActivateEffect effect;

    public ActivateEffectEditor()
    {
        flags = Controller.getInstance().getVarFlagSummary().getFlags();
        this.effect = new ActivateEffect (flags[0]);
    }

    public void draw(){
       
        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Flag ID: ");

        effect.setTargetId(flags[EditorGUILayout.Popup(Array.IndexOf(flags, effect.getTargetId()), flags)]);

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.HelpBox("Flag will be activated.",MessageType.Info);
    }

    public AbstractEffect Effect { get{ return effect; } set { effect = value as ActivateEffect; } }
    public string EffectName{ get { return "Activate effect"; } }
    public EffectEditor clone(){ return new ActivateEffectEditor(); }

    public bool manages(AbstractEffect c) { 
      
        return c.GetType() == effect.GetType();
    }
}
