using UnityEngine;
using System.Collections;
using System.Xml;

public class EffectSubParser_ : Subparser_
{
    private Effects effects;

    public EffectSubParser_(Effects effects, Chapter chapter):base(chapter)
    {
        this.effects = effects;

    }
    public override void ParseElement(XmlElement element)
    {
    }
}
