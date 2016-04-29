using UnityEngine;
using System.Collections;
using System.Xml;

public class DescriptionsSubParser_ : Subparser_
{
    private Description description;
    public DescriptionsSubParser_(Description description, Chapter chapter):base(chapter)
    {
        this.description = description;
    }

    public override void ParseElement(XmlElement element)
    {
    }
}
