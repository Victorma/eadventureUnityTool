﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class ChapterHandler_{
    /**
     * Chapter data
     */
    private Chapter chapter;

    /**
     * Current global state being subparsed
     */
    private GlobalState currentGlobalState;

    /**
     * Current macro being subparsed
     */
    private Macro currentMacro;

    /**
     * Buffer for globalstate docs
     */
    private string currentString;

    /* Methods */

    /**
     * Default constructor.
     * 
     * @param chapter
     *            Chapter in which the data will be stored
     */
    public ChapterHandler_(Chapter chapter)
    {
        this.chapter = chapter;
        currentString = string.Empty;
    }

    public void Parse(string path)
    {
        XmlDocument xmld = new XmlDocument();
        xmld.Load(path);

        XmlElement element = xmld.DocumentElement;
        XmlNodeList
            eAdventure = element.SelectNodes("/eAdventure"),
            scenes = element.SelectNodes("/eAdventure/scene"),
            slidescenes = element.SelectNodes("/eAdventure/slidescene"),
            videoscenes = element.SelectNodes("/eAdventure/videoscene"),
            books = element.SelectNodes("/eAdventure/book"),
            objects = element.SelectNodes("/eAdventure/object"),
            players = element.SelectNodes("/eAdventure/player"),
            characters = element.SelectNodes("/eAdventure/character"),
            treeconversations = element.SelectNodes("/eAdventure/tree-conversation"),
            graphconversations = element.SelectNodes("/eAdventure/graph-conversation"),
            globalstates = element.SelectNodes("/eAdventure/global-state"),
            macros = element.SelectNodes("/eAdventure/macro"),
            timers = element.SelectNodes("/eAdventure/timer"),
            atrezzoobjects = element.SelectNodes("/eAdventure/atrezzoobject"),
            assessment = element.SelectNodes("/eAdventure/assessment"),
            adaptation = element.SelectNodes("/eAdventure/adaptation");

        foreach (XmlElement el in eAdventure)
        {
            if (!string.IsNullOrEmpty(el.GetAttribute("adaptProfile")))
            {
                chapter.setAdaptationName(el.GetAttribute("adaptProfile"));
            }
            if (!string.IsNullOrEmpty(el.GetAttribute("assessProfile")))
            {
                chapter.setAssessmentName(el.GetAttribute("assessProfile"));
            }
        }


        foreach (XmlElement el in scenes)
        {
            new SceneSubParser_(chapter).ParseElement(el);
        }

        foreach (XmlElement el in slidescenes)
        {
            //TODO: subparser
            new CutsceneSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in videoscenes)
        {
            //TODO: subparser
            new CutsceneSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in books)
        {
            //TODO: subparser
            new BookSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in objects)
        {
            //TODO: subparser
            new ItemSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in players)
        {
            //TODO: subparser
            new PlayerSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in characters)
        {
            //TODO: subparser
            new CharacterSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in treeconversations)
        {
            //TODO: subparser
            new TreeConversationSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in graphconversations)
        {
            //TODO: subparser
            new GraphConversationSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in globalstates)
        {
            string id = el.GetAttribute("id");
            currentGlobalState = new GlobalState(id);
            currentString = string.Empty;
            chapter.addGlobalState(currentGlobalState);
            //TODO: subparser
            new ConditionSubParser_(currentGlobalState, chapter).ParseElement(el);
            currentGlobalState.setDocumentation(el.InnerText);
        }
        foreach (XmlElement el in macros)
        {
            string id = el.GetAttribute("id");
            currentMacro = new Macro(id);
            currentString = string.Empty;
            chapter.addMacro(currentMacro);
            //TODO: subparser
            new EffectSubParser_(currentMacro, chapter).ParseElement(el);
            currentMacro.setDocumentation(el.InnerText);
        }
        foreach (XmlElement el in timers)
        {
            //TODO: subparser
            new TimerSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in atrezzoobjects)
        {
            //TODO: subparser
            new AtrezzoSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in assessment)
        {
            //TODO: subparser
            new AssessmentSubParser_(chapter).ParseElement(el);
        }
        foreach (XmlElement el in adaptation)
        {
            //TODO: 
            new AdaptationSubParser_(chapter).ParseElement(el);
        } 
        // In the end of the document, if the chapter has no initial scene
        if (chapter.getTargetId() == null)
        {
            // Set it to the first scene
            if (chapter.getScenes().Count > 0)
                chapter.setTargetId(chapter.getScenes()[0].getId());

            // Or to the first cutscene
            else if (chapter.getCutscenes().Count > 0)
                chapter.setTargetId(chapter.getCutscenes()[0].getId());
        }
    }
  
}
