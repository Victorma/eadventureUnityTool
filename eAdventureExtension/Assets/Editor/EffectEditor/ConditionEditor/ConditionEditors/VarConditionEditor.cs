using UnityEngine;
using UnityEditor;
using System.Collections;

public class VarConditionEditor : ConditionEditor {
    VarCondition condition = new VarCondition("",4,0);
    string[] types = { " > ", " >= ", " == ", " <= ", " != "};
    string name = "Variable";

    public void draw(Condition c){
        condition = c as VarCondition;

        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Flag ID: ");

        condition.setId (EditorGUILayout.TextField (c.getId ()));

        condition.setState(EditorGUILayout.Popup (c.getState() - 4 , types) + 4);

        condition.setValue (int.Parse(EditorGUILayout.TextField (condition.getValue ().ToString ())));

        EditorGUILayout.EndHorizontal ();
    }

    public bool manages(Condition c) {
        return c.GetType() == condition.GetType();
    }

    public string conditionName(){
        return name;
    }

    public Condition InstanceManagedCondition(){
        return new VarCondition ("",4,0);
    }

    public bool Collapsed { get; set; }
    public Rect Window { get; set; }
}
