using UnityEngine;
using System.Collections;
using System.Xml;

public class TrajectorySubParser_ : Subparser_ {

    private Trajectory trajectory;

    private Scene scene;

    public TrajectorySubParser_(Chapter chapter, Scene scene):base(chapter)
    {
        this.trajectory = new Trajectory();
        //scene.setTrajectory(trajectory);
        this.scene = scene;
    }

    public override void ParseElement(XmlElement element)
    {
    }
}
