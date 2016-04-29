using UnityEngine;
using System.Collections;
using System.Xml;

public class FrameSubParser_ : Subparser_ {

    private Animation animation;

    private Frame frame;

    public FrameSubParser_(Animation animation):base(null)
    {
        this.animation = animation;
        frame = new Frame(animation.getImageLoaderFactory());
        animation.getFrames().Add(frame);
    }

    public override void ParseElement(XmlElement element)
    {
    }
}
