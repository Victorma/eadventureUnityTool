using UnityEngine;
using System.Collections;
using System.Xml;

public class ActionsSubParser_ : Subparser_
{
    /**
     * Stores the current conditions being read
     */
    private Conditions currentConditions;

    /**
     * Stores the current effects being read
     */
    private Effects currentEffects;

    /**
     * Stores the current not-effects being read
     */
    private Effects currentNotEffects;

    /**
     * Stores the current click-effects being read
     * 
     */ /**
     * Stores the current IdTarget being read
     */
    private string currentIdTarget;

    /**
     * Stores the current Name being read
     */
    private string currentName;

    /**
     * Stores the current needsGoTo being read
     */
    private bool currentNeedsGoTo;

    /**
     * Stores the current keepDinstance being read
     */
    private int currentKeepDistance;

    /**
     * Stores the current customAction being read
     */
    private CustomAction currentCustomAction;

    /**
     * Stores the current Resources being read
     */
    private ResourcesUni currentResources;

    /**
     * Activate not effects
     */
    bool activateNotEffects;

    /**
     * Activate click effects
     */
    bool activateClickEffects;

    private Effects currentClickEffects;
    private Element element;

    public ActionsSubParser_(Chapter chapter, Element element) : base(chapter)
    {
        this.element = element;
    }

    public override void ParseElement(XmlElement element)
    {
        XmlNodeList
            examines = element.SelectNodes("examines"),
            grabs = element.SelectNodes("grabs"),
            uses = element.SelectNodes("use"),
            talksto = element.SelectNodes("talk-to"),
            useswith = element.SelectNodes("use-with"),
            givesto = element.SelectNodes("give-to"),
            dragsto = element.SelectNodes("drag-to"),
            customs = element.SelectNodes("custom"),
            resourcess = element.SelectNodes("resources"),
            assets = element.SelectNodes("asset"),
            conditions = element.SelectNodes("condition"),
            effects = element.SelectNodes("effect"),
            notseffect = element.SelectNodes("not-effect"),
            clickseffect = element.SelectNodes("click-effect"),
            customsinteract = element.SelectNodes("custom-interact");

        string tmpArgVal;

        if (element.SelectSingleNode("documentation") != null)
            this.element.setDocumentation(element.SelectSingleNode("documentation").InnerText);

        foreach (XmlElement el in examines)
        {
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();

            Action examineAction = new Action(Action.EXAMINE, currentConditions, currentEffects, currentNotEffects);
            examineAction.setKeepDistance(currentKeepDistance);
            examineAction.setNeedsGoTo(currentNeedsGoTo);
            examineAction.setActivatedNotEffects(activateNotEffects);
            examineAction.setActivatedClickEffects(activateClickEffects);
            this.element.addAction(examineAction);
        }
        foreach (XmlElement el in grabs)
        {
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();

            Action grabAction = new Action(Action.GRAB, currentConditions, currentEffects, currentNotEffects);
            grabAction.setKeepDistance(currentKeepDistance);
            grabAction.setNeedsGoTo(currentNeedsGoTo);
            grabAction.setActivatedNotEffects(activateNotEffects);
            grabAction.setActivatedClickEffects(activateClickEffects);
            this.element.addAction(grabAction);
        }
        foreach (XmlElement el in uses)
        {
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();

            Action useAction = new Action(Action.USE, currentConditions, currentEffects, currentNotEffects);
            useAction.setNeedsGoTo(currentNeedsGoTo);
            useAction.setKeepDistance(currentKeepDistance);
            useAction.setActivatedNotEffects(activateNotEffects);
            useAction.setActivatedClickEffects(activateClickEffects);
            this.element.addAction(useAction);
        }
        foreach (XmlElement el in talksto)
        {
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();

            Action talkToAction = new Action(Action.TALK_TO, currentConditions, currentEffects, currentNotEffects);
            talkToAction.setNeedsGoTo(currentNeedsGoTo);
            talkToAction.setKeepDistance(currentKeepDistance);
            talkToAction.setActivatedNotEffects(activateNotEffects);
            talkToAction.setActivatedClickEffects(activateClickEffects);
            this.element.addAction(talkToAction);
        }


        foreach (XmlElement el in useswith)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentIdTarget = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("click-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateClickEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();

            Action useWithAction = new Action(Action.USE_WITH, currentIdTarget, currentConditions, currentEffects,
                currentNotEffects, currentClickEffects);
            useWithAction.setKeepDistance(currentKeepDistance);
            useWithAction.setNeedsGoTo(currentNeedsGoTo);
            useWithAction.setActivatedNotEffects(activateNotEffects);
            useWithAction.setActivatedClickEffects(activateClickEffects);
            this.element.addAction(useWithAction);
        }


        foreach (XmlElement el in dragsto)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentIdTarget = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("click-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateClickEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();

            Action useWithAction = new Action(Action.DRAG_TO, currentIdTarget, currentConditions, currentEffects,
                currentNotEffects, currentClickEffects);
            useWithAction.setKeepDistance(currentKeepDistance);
            useWithAction.setNeedsGoTo(currentNeedsGoTo);
            useWithAction.setActivatedNotEffects(activateNotEffects);
            useWithAction.setActivatedClickEffects(activateClickEffects);
            this.element.addAction(useWithAction);
        }


        foreach (XmlElement el in givesto)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentIdTarget = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("click-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateClickEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();

            Action giveToAction = new Action(Action.GIVE_TO, currentIdTarget, currentConditions, currentEffects,
                currentNotEffects, currentClickEffects);
            giveToAction.setKeepDistance(currentKeepDistance);
            giveToAction.setNeedsGoTo(currentNeedsGoTo);
            giveToAction.setActivatedNotEffects(activateNotEffects);
            giveToAction.setActivatedClickEffects(activateClickEffects);
            this.element.addAction(giveToAction);
        }


        foreach (XmlElement el in customs)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentIdTarget = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("name");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentName = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("click-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateClickEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();
            currentCustomAction = new CustomAction(Action.CUSTOM);

            currentCustomAction.setName(currentName);
            currentCustomAction.setConditions(currentConditions);
            currentCustomAction.setEffects(currentEffects);
            currentCustomAction.setNotEffects(currentNotEffects);
            currentCustomAction.setKeepDistance(currentKeepDistance);
            currentCustomAction.setNeedsGoTo(currentNeedsGoTo);
            currentCustomAction.setActivatedNotEffects(activateNotEffects);
            currentCustomAction.setClickEffects(currentClickEffects);
            currentCustomAction.setActivatedClickEffects(activateClickEffects);
            //				customAction.addResources(currentResources);
            this.element.addAction(currentCustomAction);
            currentCustomAction = null;
        }

        foreach (XmlElement el in customsinteract)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentIdTarget = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("name");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentName = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("needsGoTo");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentNeedsGoTo = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("keepDistance");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentKeepDistance = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("not-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateNotEffects = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("click-effects");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                activateClickEffects = tmpArgVal.Equals("yes");
            }
            currentConditions = new Conditions();
            currentEffects = new Effects();
            currentNotEffects = new Effects();
            currentClickEffects = new Effects();
            currentCustomAction = new CustomAction(Action.CUSTOM_INTERACT);

            currentCustomAction.setConditions(currentConditions);
            currentCustomAction.setEffects(currentEffects);
            currentCustomAction.setNotEffects(currentNotEffects);
            currentCustomAction.setName(currentName);
            currentCustomAction.setTargetId(currentIdTarget);
            currentCustomAction.setKeepDistance(currentKeepDistance);
            currentCustomAction.setNeedsGoTo(currentNeedsGoTo);
            currentCustomAction.setActivatedNotEffects(activateNotEffects);
            currentCustomAction.setClickEffects(currentClickEffects);
            currentCustomAction.setActivatedClickEffects(activateClickEffects);
            //				customAction.addResources(currentResources);
            this.element.addAction(currentCustomAction);
            currentCustomAction = null;
        }


        foreach (XmlElement el in resourcess)
        {
            currentResources = new ResourcesUni();
            tmpArgVal = el.GetAttribute("name");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentResources.setName(el.GetAttribute(tmpArgVal));
            }

            assets = el.SelectNodes("asset");
            foreach (XmlElement ell in assets)
            {
                string type = "";
                string path = "";

                tmpArgVal = ell.GetAttribute("type");
                if (!string.IsNullOrEmpty(tmpArgVal))
                {
                    type = tmpArgVal;
                }
                tmpArgVal = ell.GetAttribute("uri");
                if (!string.IsNullOrEmpty(tmpArgVal))
                {
                    path = tmpArgVal;
                }
                currentResources.addAsset(type, path);
            }

            conditions = el.SelectNodes("condition");
            foreach (XmlElement ell in conditions)
            {
                currentConditions = new Conditions();
                new ConditionSubParser_(currentConditions, chapter).ParseElement(ell);
                currentResources.setConditions(currentConditions);
            }

            this.element.addResources(currentResources);
        }

        foreach (XmlElement el in effects)
        {
            currentEffects = new Effects();
            new EffectSubParser_(currentEffects, chapter).ParseElement(el);
        }
        foreach (XmlElement el in notseffect)
        {
            currentNotEffects = new Effects();
            new EffectSubParser_(currentNotEffects, chapter).ParseElement(el);
        }
        foreach (XmlElement el in effects)
        {
            currentClickEffects = new Effects();
            new EffectSubParser_(currentClickEffects, chapter).ParseElement(el);
        }

    }

}