using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class PlayerSubParser_ : Subparser_ {

    private List<Description> descriptions;

    public PlayerSubParser_(Chapter chapter):base(chapter)
    {
        descriptions = new List<Description>();
    }

    public override void ParseElement(XmlElement element)
    {
    }
}
