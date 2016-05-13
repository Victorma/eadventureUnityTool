﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class EffectHolderNode{
    AbstractEffect effect;

	private bool runs_once = true;
	private int times_runed = 0;

    private bool validated = false;
    private bool is_valid = false;

    Dictionary<string,object> aditional_info = new Dictionary<string, object> ();

    public EffectHolderNode(AbstractEffect effect){
        this.effect = effect;
	}
	
	public bool execute(){
		bool forcewait = false;
		if(!(runs_once && times_runed >0)){
            if (effect == null || effect.getConditions () == null) {
                Debug.Log ("Asd");
            } else {
                if (!validated) {
                    is_valid = ConditionChecker.check (effect.getConditions ());
                    validated = true;
                }

                if (is_valid) {
                    switch (effect.getType ()) {
                    case EffectType.ACTIVATE:
                        Game.Instance.setFlag (((ActivateEffect)effect).getTargetId (), FlagCondition.FLAG_ACTIVE);
                        break;
                    case EffectType.DEACTIVATE:
                        Game.Instance.setFlag (((DeactivateEffect)effect).getTargetId (), FlagCondition.FLAG_INACTIVE);
                        break;
                    case EffectType.SPEAK_PLAYER:
                        Game.Instance.talk (((SpeakPlayerEffect)effect).getLine (), null);
                        forcewait = true;
                        break;
                    case EffectType.SPEAK_CHAR:
                        Game.Instance.talk (((SpeakCharEffect)effect).getLine (), ((SpeakCharEffect)effect).getTargetId ());
                        forcewait = true;
                        break;
                    case EffectType.TRIGGER_SCENE:
                        runs_once = false;
                        Game.Instance.renderScene (((TriggerSceneEffect)effect).getTargetId ());
                        break;
                    case EffectType.TRIGGER_CUTSCENE: 
                        runs_once = false;
                        TriggerCutsceneEffect tce = (TriggerCutsceneEffect)effect;
                        if (times_runed > 0) {
                            forcewait = ((SceneMB)aditional_info ["scene"]).Interacted () == InteractuableResult.REQUIRES_MORE_INTERACTION;
                        } else {
                            aditional_info = new Dictionary<string, object> ();
                            aditional_info.Add ("lastscene", Game.Instance.getCurrentScene ());
                            aditional_info.Add ("scene", Game.Instance.renderScene (tce.getTargetId ()).GetComponent<SceneMB> ());
                            forcewait = true; 
                        }

                        if (!forcewait && ((Slidescene)((SceneMB)aditional_info ["scene"]).sceneData).getNext () == Slidescene.GOBACK) {
                            string last = (string)aditional_info ["lastscene"];
                            Game.Instance.renderScene (last);
                        }

                        break;
                    case EffectType.TRIGGER_LAST_SCENE:
                        runs_once = false;
                        Game.Instance.renderLastScene ();
                        break;
                    case EffectType.TRIGGER_CONVERSATION: 
                        runs_once = false; 
                        runs_once = false;
                        if (times_runed == 0) {
                            TriggerConversationEffect tcoe = (TriggerConversationEffect)effect;
                            this.aditional_info.Add ("conversation", new GraphConversationHolder (Game.Instance.getConversation (tcoe.getTargetId ())));
                        }
                        forcewait = ((GraphConversationHolder)this.aditional_info ["conversation"]).execute ();
                        break;
                    case EffectType.RANDOM_EFFECT: 
                        runs_once = false;
                        RandomEffect re = (RandomEffect)effect;

                        if (!aditional_info.ContainsKey ("first")) {
                            aditional_info.Add ("first", new EffectHolderNode (re.getPositiveEffect ()));
                            aditional_info.Add ("second", new EffectHolderNode (re.getNegativeEffect ()));
                        }

                        if (times_runed == 0) {
                            int pro = re.getProbability (), now = Random.Range (0, 100);
                            if (aditional_info.ContainsKey ("current"))
                                aditional_info.Remove ("current");

                            if (pro <= now)
                                aditional_info.Add ("current", "first");
                            else
                                aditional_info.Add ("current", "second");

                            forcewait = ((EffectHolderNode)aditional_info [((string)aditional_info ["current"])]).execute ();
                        } else
                            forcewait = ((EffectHolderNode)aditional_info [((string)aditional_info ["current"])]).execute ();

                        break;
                    case EffectType.SET_VALUE: 
                        SetValueEffect sve = (SetValueEffect)effect;
                        Game.Instance.setVariable (sve.getTargetId (), sve.getValue ()); 
                        break;
                    case EffectType.INCREMENT_VAR:
                        IncrementVarEffect ive = (IncrementVarEffect)effect;
                        Game.Instance.setVariable (ive.getTargetId (), Game.Instance.getVariable (ive.getTargetId ()) + ive.getIncrement ()); 
                        break;
                    case EffectType.DECREMENT_VAR:
                        DecrementVarEffect dve = (DecrementVarEffect)effect;
                        Game.Instance.setVariable (dve.getTargetId (), Game.Instance.getVariable (dve.getTargetId ()) - dve.getDecrement ()); 
                        break;
                    case EffectType.MACRO_REF: 
                        runs_once = false;
                        if (times_runed == 0) {
                            MacroReferenceEffect mre = (MacroReferenceEffect)effect;
                            this.aditional_info.Add ("macro", new EffectHolder (Game.Instance.getMacro (mre.getTargetId ())));
                        }
                        forcewait = ((EffectHolder)this.aditional_info ["macro"]).execute ();
                        break;
                    case EffectType.MOVE_OBJECT:
                        MoveObjectEffect moe = (MoveObjectEffect)effect;
                        Game.Instance.Move (moe.getTargetId (), new Vector2 (moe.getX (), 600 - moe.getY ()) / 10f);
                        break;
                    }
                }
            }
		}

		if (!forcewait)
			times_runed = 0;
		else
			times_runed++;
		
		return forcewait;
	}

	public bool check(){
        return ConditionChecker.check(effect.getConditions());
	}
}

public class EffectHolder : Secuence, Interactuable {
    public List<EffectHolderNode> effects;

	private string documentation;

    public EffectHolder(Effects effects){
        this.effects = new List<EffectHolderNode> ();

        if (effects != null && effects.getEffects().Count>0) {
            
            EffectHolderNode e = null;
			List<Condition> conditions = new List<Condition>();
            foreach (AbstractEffect effect in effects.getEffects()) {
                if (effect == null) {

                }
				this.effects.Add(new EffectHolderNode(effect));
			}
		}
	}

	private int lastexecuted = 0;
	public bool execute(){
		bool forcewait = false;
		for (int i = lastexecuted; i<effects.Count; i++) {
			if(effects[i].execute()){
				lastexecuted = i;
				forcewait = true;
				break;
			}
		}

		if (!forcewait)
			lastexecuted = 0;

		return forcewait;
	}

    public InteractuableResult Interacted (RaycastHit hit = new RaycastHit()){
        if (this.execute ()) {
            return InteractuableResult.REQUIRES_MORE_INTERACTION;
        } else
            return InteractuableResult.DOES_SOMETHING;
    }
}