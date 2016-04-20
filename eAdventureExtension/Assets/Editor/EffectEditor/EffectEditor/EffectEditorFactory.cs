﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class EffectEditorFactory {
    
	private static EffectEditorFactory instance;
	public static EffectEditorFactory Intance {
		get{ 
			if(instance == null)
				instance = new EffectEditorFactoryImp();
			return instance; 
		}
	}
	
	public abstract string[] CurrentEffectEditors { get; }
	public abstract EffectEditor createEffectEditorFor (string effectName);
    public abstract EffectEditor createEffectEditorFor (AbstractEffect effect); 
	public abstract int EffectEditorIndex(AbstractEffect effect);
}

public class EffectEditorFactoryImp : EffectEditorFactory {

    private List<System.Type> types;
	private List<EffectEditor> effectEditors;
	private EffectEditor defaultEffectEditor;
	
	public EffectEditorFactoryImp(){
		this.effectEditors = new List<EffectEditor> ();

        if (types == null) {
            types = System.AppDomain.CurrentDomain.GetAssemblies ().SelectMany (s => s.GetTypes ()).Where (p => typeof(EffectEditor).IsAssignableFrom (p)).ToList();
            types.Remove(typeof(EffectEditor));
        }

        foreach (System.Type t in types)
            this.effectEditors.Add((EffectEditor) System.Activator.CreateInstance(t));
	}
	
	public override string[] CurrentEffectEditors {
		get {
			string[] descriptors = new string[effectEditors.Count+1];
			for(int i = 0; i< effectEditors.Count; i++)
				descriptors[i] = effectEditors[i].EffectName;
			return descriptors;
		}
	}
	
	
	public override EffectEditor createEffectEditorFor (string effectName)
	{
		foreach (EffectEditor effectEditor in effectEditors) {
			if(effectEditor.EffectName.ToLower() == effectName.ToLower()){
				return effectEditor.clone();
			}
		}
		return null;
	}

    public override EffectEditor createEffectEditorFor (AbstractEffect effect)
    {
        foreach (EffectEditor effectEditor in effectEditors) 
            if(effectEditor.manages(effect))    
                return effectEditor.clone();

        return null;
    }

	public override int EffectEditorIndex(AbstractEffect effect){
		
		int i = 0;
		foreach (EffectEditor effectEditor in effectEditors) 
			if(effectEditor.manages(effect))	
                return i;
		    else 							
                i++;
		
		return 0;
	}
}
