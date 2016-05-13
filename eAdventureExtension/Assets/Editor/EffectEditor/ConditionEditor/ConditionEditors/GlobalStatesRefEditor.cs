using UnityEngine;
using UnityEditor;
using System.Collections;

public class GlobalStatesRefEditor : ConditionEditor
{
    GlobalStateCondition condition = new GlobalStateCondition("");
    string[] types = { "is satisfied", "is not satisfied" };
    string name = "Global state";

    public void draw(Condition c)
    {
        condition = c as GlobalStateCondition;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Global state ID: ");

        c.setId(EditorGUILayout.TextField(c.getId()));
        c.setState(EditorGUILayout.Popup(c.getState() - GlobalStateCondition.GS_SATISFIED, types) + GlobalStateCondition.GS_SATISFIED);

        EditorGUILayout.EndHorizontal();
    }

    public bool manages(Condition c)
    {
        return c.GetType() == condition.GetType();
    }

    public string conditionName()
    {
        return name;
    }

    public Condition InstanceManagedCondition()
    {
        return new GlobalStateCondition("");
    }

    public bool Collapsed { get; set; }
    public Rect Window { get; set; }
}
