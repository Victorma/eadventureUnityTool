using UnityEngine;
using UnityEditor;
using System.Collections;

public class DeactivateEffectEditor : EffectEditor {
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

    private DeactivateEffect effect;

    public DeactivateEffectEditor(){
        this.effect = new DeactivateEffect ("");
    }

    public void draw(){

        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Flag ID: ");

        string tid = effect.getTargetId ();
        EditorGUILayout.TextField (tid);
        effect.setTargetId (tid);

        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.HelpBox("Flag will be deactivated.",MessageType.Info);
    }

    public AbstractEffect Effect { get{ return effect; } set { effect = value as DeactivateEffect; } }
    public string EffectName{ get { return "Deactivate effect"; } }
    public EffectEditor clone(){ return new DeactivateEffectEditor(); }

    public bool manages(AbstractEffect c) { 

        return c.GetType() == effect.GetType();
    }
}
