﻿using UnityEngine;
using System.Collections;
using System;

public class NewProjectMenuItem : IMenuItem
{
    public NewProjectMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class LoadProjectMenuItem : IMenuItem
{
    public LoadProjectMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class SaveProjectMenuItem : IMenuItem
{
    public SaveProjectMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class SaveProjectAsMenuItem : IMenuItem
{
    public SaveProjectAsMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class LOMMetadataEditorMenuItem : IMenuItem
{
    public LOMMetadataEditorMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class LearningObjectPropertiesMenuItem : IMenuItem
{
    public LearningObjectPropertiesMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class LearningObjectPropertiesSCORMMenuItem : IMenuItem
{
    public LearningObjectPropertiesSCORMMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class ExportProjectMenuItem : IMenuItem
{
    public ExportProjectMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class ExportProjectEadMenuItem : IMenuItem
{
    public ExportProjectEadMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        Controller.getInstance().exportGame();
    }
}

public class UndoMenuItem : IMenuItem
{
    public UndoMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class RedoMenuItem : IMenuItem
{
    public RedoMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class SearchMenuItem : IMenuItem
{
    public SearchMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class CheckAdventureConsistencyMenuItem : IMenuItem
{
    public CheckAdventureConsistencyMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class EditAdventureDataMenuItem : IMenuItem
{
    public EditAdventureDataMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}


public class VisualisationMenuItem : IMenuItem
{
    public VisualisationMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class ConvertToMenuItem : IMenuItem
{
    public ConvertToMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class DeleteUnusedDataMenuItem : IMenuItem
{
    public DeleteUnusedDataMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class AddChapterMenuItem : IMenuItem, DialogReceiverInterface
{
    public AddChapterMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        ChapterNewNameInputPopup window = (ChapterNewNameInputPopup)ScriptableObject.CreateInstance(typeof(ChapterNewNameInputPopup));
        window.Init(this, "chapter");
    }

    public void OnDialogOk(string message, object workingObject = null, object workingObjectSecond = null)
    {
        if (workingObject is ChapterNewNameInputPopup)
        {
            Controller.getInstance().addChapter(message);
        }
    }

    public void OnDialogCanceled(object workingObject = null)
    {
    }
}

public class DeleteChapterMenuItem : IMenuItem, DialogReceiverInterface
{
    public DeleteChapterMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        ConfirmationDialog window = (ConfirmationDialog)ScriptableObject.CreateInstance(typeof(ConfirmationDialog));
        window.Init(this, "Delete chapter: " + Controller.getInstance().getCharapterList().getChapterTitles()[Controller.getInstance().getCharapterList().getSelectedChapter()]);
    }

    public void OnDialogOk(string message, object workingObject = null, object workingObjectSecond = null)
    {
        if (workingObject is ConfirmationDialog)
        {
            Controller.getInstance().deleteChapter();
            Controller.getInstance().RefreshView();
            ChaptersMenu.getInstance().RefreshMenuItems();
        }
    }

    public void OnDialogCanceled(object workingObject = null)
    {
    }
}

public class MoveUpChapterMenuItem : IMenuItem
{
    public MoveUpChapterMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        Controller.getInstance()
            .getCharapterList()
            .moveChapterUp(Controller.getInstance().getCharapterList().getSelectedChapter());
        ChaptersMenu.getInstance().RefreshMenuItems();
    }
}

public class MoveDownChapterMenuItem : IMenuItem
{
    public MoveDownChapterMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        Controller.getInstance()
            .getCharapterList()
            .moveChapterDown(Controller.getInstance().getCharapterList().getSelectedChapter());
        ChaptersMenu.getInstance().RefreshMenuItems();
    }
}

public class ImportChapterMenuItem : IMenuItem
{
    public ImportChapterMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class EditFlagsVariablesMenuItem : IMenuItem, DialogReceiverInterface
{
    public EditFlagsVariablesMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        ChapterVarAndFlagsEditor falgsVarEditor =
        (ChapterVarAndFlagsEditor)ScriptableObject.CreateInstance(typeof(ChapterVarAndFlagsEditor));
        falgsVarEditor.Init(this);
    }

    public void OnDialogOk(string message, object workingObject = null, object workingObjectSecond = null)
    {

    }

    public void OnDialogCanceled(object workingObject = null)
    {
        
    }
}


public class RunMenuItem : IMenuItem
{
    public RunMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class RunNormalMenuItem : IMenuItem
{
    public RunNormalMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class RunDebugMenuItem : IMenuItem
{
    public RunDebugMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class SetLanguageEnglishMenuItem : IMenuItem
{
    public SetLanguageEnglishMenuItem()
    {
        this.Label = "English";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_ENGLISH));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguageDeutschMenuItem : IMenuItem
{
    public SetLanguageDeutschMenuItem()
    {
        this.Label = "Deutsch";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_DEUTSCH));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguageSpanishMenuItem : IMenuItem
{
    public SetLanguageSpanishMenuItem()
    {
        this.Label = "Español";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_SPANISH));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguageGalegoMenuItem : IMenuItem
{
    public SetLanguageGalegoMenuItem()
    {
        this.Label = "Galego";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_GALEGO));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguageItalianoMenuItem : IMenuItem
{
    public SetLanguageItalianoMenuItem()
    {
        this.Label = "Italiano";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_ITALIANO));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguagePortugeseMenuItem : IMenuItem
{
    public SetLanguagePortugeseMenuItem()
    {
        this.Label = "Português";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_PORTUGESE));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguagePortugeseBrazilMenuItem : IMenuItem
{
    public SetLanguagePortugeseBrazilMenuItem()
    {
        this.Label = "Português-Brasil";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_PORTUGESE_BRAZIL));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguageRomaniaMenuItem : IMenuItem
{
    public SetLanguageRomaniaMenuItem()
    {
        this.Label = "Language.Name";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_ROMANIA));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguageRussiaMenuItem : IMenuItem
{
    public SetLanguageRussiaMenuItem()
    {
        this.Label = "русский язык";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_RUSSIAN));
        EditorWindowBase.InitGUI();
    }
}

public class SetLanguageChinaMenuItem : IMenuItem
{
    public SetLanguageChinaMenuItem()
    {
        this.Label = "中文";
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {
        TC.loadstrings(ReleaseFolders.getLanguageFilePath4Editor(true, ReleaseFolders.LANGUAGE_CHINA));
        EditorWindowBase.InitGUI();
    }
}

public class AboutEAMenuItem : IMenuItem
{
    public AboutEAMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}

public class AboutEASendMenuItem : IMenuItem
{
    public AboutEASendMenuItem(string name_)
    {
        this.Label = name_;
    }

    public string Label
    {
        get; set;
    }

    public void OnCliked()
    {

    }
}