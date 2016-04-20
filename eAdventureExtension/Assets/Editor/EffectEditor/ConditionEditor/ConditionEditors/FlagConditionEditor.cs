using UnityEngine;
using UnityEditor;
using System.Collections;

public class FlagConditionEditor : ConditionEditor {
    FlagCondition condition = new FlagCondition("");
    string[] types = { "Active", "Inactive" };
    string name = "Flag";

    public void draw(Condition c){
        condition = c as FlagCondition;

        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Flag ID: ");

        c.setId (EditorGUILayout.TextField (c.getId ()));

        c.setState(EditorGUILayout.Popup (c.getState(), types));

        EditorGUILayout.EndHorizontal ();
    }

    public bool manages(Condition c) {
        return c.GetType() == condition.GetType();
    }

    public string conditionName(){
        return name;
    }

    public Condition InstanceManagedCondition(){
        return new FlagCondition ("");
    }
}
