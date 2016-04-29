using UnityEngine;
using System.Collections;
using System.Xml;

public class ConditionSubParser_ : Subparser_ {

    private Conditions conditions;

    public ConditionSubParser_(Conditions conditions, Chapter chapter):base(chapter)
    {
        this.conditions = conditions;
    }

    public override void ParseElement(XmlElement element)
    {
    }
}
