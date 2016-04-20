using UnityEngine;
using UnityEditor;
using System.Collections;

public class ActivateEffectEditor : EffectEditor {
    private bool collapsed = false;
    public bool Collapsed { get { return collapsed; } set { collapsed = value; } }
    private Rect window = new Rect(0, 0, 300, 0);
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

    public ActivateEffectEditor(){
        this.effect = new ActivateEffect ("");
    }

    public void draw(){
       
        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Flag ID: ");

        effect.setTargetId (EditorGUILayout.TextField (effect.getTargetId ()));

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
