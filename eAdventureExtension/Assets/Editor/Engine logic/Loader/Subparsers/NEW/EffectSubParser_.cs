using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

public class EffectSubParser_ : Subparser_
{
    /**
         * Stores the current id target
         */
    private string currentCharIdTarget;

    /**
     * Stores the effects being parsed
     */
    private Effects effects;

    /**
     * Atributes for show-text effects
     */

    int x = 0;

    int y = 0;

    int frontColor = 0;

    int borderColor = 0;

    /**
     * Constants for reading random-effect
     */
    private bool positiveBlockRead = false;

    private bool readingRandomEffect = false;

    private RandomEffect randomEffect;

    /**
     * Stores the current conditions being read
     */
    private Conditions currentConditions;

    /**
     * CurrentEffect. Stores the last created effect to add it later the
     * conditions
     */
    private AbstractEffect currentEffect;

    /**
     * New effects
     */
    private AbstractEffect newEffect;

    /**
     * Audio path for speak player and character
     */
    private string audioPath;

    public EffectSubParser_(Effects effects, Chapter chapter) : base(chapter)
    {
        this.effects = effects;

    }

    public override void ParseElement(XmlElement element)
    {
        XmlNodeList
            cancelsaction = element.SelectNodes("cancel-action"),
            activates = element.SelectNodes("activate"),
            deactivates = element.SelectNodes("deactivate"),
            setsvalue = element.SelectNodes("set-value"),
            increments = element.SelectNodes("increment"),
            decrements = element.SelectNodes("decrement"),
            macrosref = element.SelectNodes("macro-ref"),
            consumesobject = element.SelectNodes("consume-object"),
            generatesobject = element.SelectNodes("generate-object"),
            speakschar = element.SelectNodes("speak-char"),
            triggersbook = element.SelectNodes("trigger-book"),
            triggerslastscene = element.SelectNodes("trigger-last-scene"),
            playssound = element.SelectNodes("play-sound"),
            triggersconversation = element.SelectNodes("trigger-conversation"),
            triggerscutscene = element.SelectNodes("trigger-cutscene"),
            triggersscenes = element.SelectNodes("trigger-scene"),
            playsanimations = element.SelectNodes("play-animation"),
            movesplayers = element.SelectNodes("move-player"),
            movesnpcs = element.SelectNodes("move-npc"),
            randomseffects = element.SelectNodes("random-effect"),
            waitstimes = element.SelectNodes("wait-time"),
            showstexts = element.SelectNodes("show-text"),
            highlightsitems = element.SelectNodes("highlight-item"),
            movesobjects = element.SelectNodes("move-object"),
            speaksplayers = element.SelectNodes("speak-player"),
            conditions = element.SelectNodes("condition");

        string tmpArgVal;

        foreach (XmlElement el in cancelsaction)
        {
            newEffect = new CancelActionEffect();
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in activates)
        {
            tmpArgVal = el.GetAttribute("flag");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                newEffect = new ActivateEffect(tmpArgVal);
                chapter.addFlag(tmpArgVal);
            }
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in deactivates)
        {
            tmpArgVal = el.GetAttribute("flag");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                newEffect = new DeactivateEffect(tmpArgVal);
                chapter.addFlag(tmpArgVal);
            }
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in setsvalue)
        {
            string var = null;
            int value = 0;

            tmpArgVal = el.GetAttribute("var");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                var = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("value");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                value = int.Parse(tmpArgVal);
            }
            newEffect = new SetValueEffect(var, value);
            chapter.addVar(var);

            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in increments)
        {
            string var = null;
            int value = 0;

            tmpArgVal = el.GetAttribute("var");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                var = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("value");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                value = int.Parse(tmpArgVal);
            }
            newEffect = new IncrementVarEffect(var, value);
            chapter.addVar(var);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in decrements)
        {
            string var = null;
            int value = 0;

            tmpArgVal = el.GetAttribute("var");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                var = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("value");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                value = int.Parse(tmpArgVal);
            }
            newEffect = new DecrementVarEffect(var, value);
            chapter.addVar(var);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in macrosref)
        {
            // Id
            string id = null;
            tmpArgVal = el.GetAttribute("id");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                id = tmpArgVal;
            }
            // Store the inactive flag in the conditions or either conditions
            newEffect = new MacroReferenceEffect(id);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in consumesobject)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                newEffect = new ConsumeObjectEffect(tmpArgVal);
            }
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in generatesobject)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                newEffect = new GenerateObjectEffect(tmpArgVal);
            }
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in speakschar)
        {
            audioPath = "";
            currentCharIdTarget = null;

            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                currentCharIdTarget = tmpArgVal;
            }

            tmpArgVal = el.GetAttribute("uri");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                audioPath = tmpArgVal;
            }

            // Add the effect and clear the current string
            newEffect = new SpeakCharEffect(currentCharIdTarget, el.InnerText.ToString().Trim());
            ((SpeakCharEffect) newEffect).setAudioPath(audioPath);
        }

        foreach (XmlElement el in triggersbook)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                newEffect = new TriggerBookEffect(tmpArgVal);
            }
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in triggerslastscene)
        {
            newEffect = new TriggerLastSceneEffect();
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in playssound)
        {
            // Store the path and background
            string path = "";
            bool background = true;

            tmpArgVal = el.GetAttribute("background");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                background = tmpArgVal.Equals("yes");
            }
            tmpArgVal = el.GetAttribute("uri");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                path = tmpArgVal;
            }

            // Add the new play sound effect
            newEffect = new PlaySoundEffect(background, path);

            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in triggersconversation)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                newEffect = new TriggerConversationEffect(tmpArgVal);
            }
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in triggerscutscene)
        {
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                newEffect = new TriggerCutsceneEffect(tmpArgVal);
            }
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in triggersscenes)
        {
            string scene = "";
            int x = 0;
            int y = 0;
            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                scene = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("x");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                x = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("y");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                y = int.Parse(tmpArgVal);
            }

            newEffect = new TriggerSceneEffect(scene, x, y);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in playsanimations)
        {
            string path = "";
            int x = 0;
            int y = 0;

            tmpArgVal = el.GetAttribute("uri");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                path = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("x");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                x = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("y");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                y = int.Parse(tmpArgVal);
            }

            // Add the new play sound effect
            newEffect = new PlayAnimationEffect(path, x, y);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in movesplayers)
        {
            int x = 0;
            int y = 0;

            tmpArgVal = el.GetAttribute("x");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                x = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("y");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                y = int.Parse(tmpArgVal);
            }

            // Add the new move player effect
            newEffect = new MovePlayerEffect(x, y);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in movesnpcs)
        {
            string npcTarget = "";
            int x = 0;
            int y = 0;

            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                npcTarget = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("x");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                x = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("y");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                y = int.Parse(tmpArgVal);
            }

            // Add the new move NPC effect
            newEffect = new MoveNPCEffect(npcTarget, x, y);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in randomseffects)
        {
            int probability = 0;

            tmpArgVal = el.GetAttribute("probability");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                probability = int.Parse(tmpArgVal);
            }

            // Add the new random effect
            randomEffect = new RandomEffect(probability);
            newEffect = randomEffect;
            readingRandomEffect = true;
            positiveBlockRead = false;
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;

            // When we have just created the effect, add it
            if (newEffect != null && newEffect == randomEffect)
            {
                effects.add(newEffect);
            }
            // Otherwise, determine if it is positive or negative effect 
            else if (newEffect != null && !positiveBlockRead)
            {
                randomEffect.setPositiveEffect(newEffect);
                positiveBlockRead = true;
            }
            // Negative effect 
            else if (newEffect != null && positiveBlockRead)
            {
                randomEffect.setNegativeEffect(newEffect);
                positiveBlockRead = false;
                readingRandomEffect = false;
                newEffect = randomEffect;
                randomEffect = null;

            }
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in waitstimes)
        {
            int time = 0;

            tmpArgVal = el.GetAttribute("time");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                time = int.Parse(tmpArgVal);
            }

            // Add the new move NPC effect
            newEffect = new WaitTimeEffect(time);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in showstexts)
        {
            x = 0;
            y = 0;
            frontColor = 0;
            borderColor = 0;
            audioPath = "";


            tmpArgVal = el.GetAttribute("x");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                x = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("y");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                y = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("frontColor");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                frontColor = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("borderColor");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                borderColor = int.Parse(tmpArgVal);
            }
            tmpArgVal = el.GetAttribute("uri");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                audioPath = tmpArgVal;
            }

            // Add the new ShowTextEffect
            newEffect = new ShowTextEffect(el.InnerText.ToString().Trim(), x, y, frontColor, borderColor);
            ((ShowTextEffect) newEffect).setAudioPath(audioPath);
        }

        foreach (XmlElement el in highlightsitems)
        {
            int type = 0;
            bool animated = false;
            string id = "";

            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                id = tmpArgVal;
            }

            tmpArgVal = el.GetAttribute("animated");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                animated = (tmpArgVal.Equals("yes") ? true : false);
            }

            tmpArgVal = el.GetAttribute("type");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                if (tmpArgVal.Equals("none"))
                    type = HighlightItemEffect.NO_HIGHLIGHT;
                if (tmpArgVal.Equals("green"))
                    type = HighlightItemEffect.HIGHLIGHT_GREEN;
                if (tmpArgVal.Equals("red"))
                    type = HighlightItemEffect.HIGHLIGHT_RED;
                if (tmpArgVal.Equals("blue"))
                    type = HighlightItemEffect.HIGHLIGHT_BLUE;
                if (tmpArgVal.Equals("border"))
                    type = HighlightItemEffect.HIGHLIGHT_BORDER;
            }
            newEffect = new HighlightItemEffect(id, type, animated);

            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in movesobjects)
        {
            bool animated = false;
            string id = "";
            int x = 0;
            int y = 0;
            float scale = 1.0f;
            int translateSpeed = 20;
            int scaleSpeed = 20;


            tmpArgVal = el.GetAttribute("idTarget");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                id = tmpArgVal;
            }
            tmpArgVal = el.GetAttribute("animated");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                animated = (tmpArgVal.Equals("yes") ? true : false);
            }

            tmpArgVal = el.GetAttribute("x");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                x = int.Parse(tmpArgVal);
            }

            tmpArgVal = el.GetAttribute("y");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                y = int.Parse(tmpArgVal);
            }

            tmpArgVal = el.GetAttribute("scale");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                scale = float.Parse(tmpArgVal, CultureInfo.InvariantCulture);
            }

            tmpArgVal = el.GetAttribute("translateSpeed");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                translateSpeed = int.Parse(tmpArgVal);
            }

            tmpArgVal = el.GetAttribute("scaleSpeed");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                scaleSpeed = int.Parse(tmpArgVal);
            }
            newEffect = new MoveObjectEffect(id, x, y, scale, animated, translateSpeed, scaleSpeed);
            effects.add(newEffect);
            // Store current effect
            currentEffect = newEffect;
        }

        foreach (XmlElement el in speaksplayers)
        {
            audioPath = "";

            tmpArgVal = el.GetAttribute("uri");
            if (!string.IsNullOrEmpty(tmpArgVal))
            {
                audioPath = tmpArgVal;
            }

            // Add the effect and clear the current string
            newEffect = new SpeakPlayerEffect(el.InnerText.ToString().Trim());
            ((SpeakPlayerEffect) newEffect).setAudioPath(audioPath);
        }

        foreach (XmlElement el in conditions)
        {
            currentConditions = new Conditions();
            new ConditionSubParser_(currentConditions, chapter).ParseElement(el);
            currentEffect.setConditions(currentConditions);
        }
        newEffect = null;
        audioPath = string.Empty;
    }

}