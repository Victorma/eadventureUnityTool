using UnityEngine;
using System.Collections;
using System.Xml;

public class TransitionSubParser_ : Subparser_ {
    private Animation animation;

    private Transition transition;

    public TransitionSubParser_(Animation animation):base(null)
    {
        this.animation = animation;
        transition = new Transition();
    }

    public override void ParseElement(XmlElement element)
    {
    }
}
