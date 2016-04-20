using UnityEngine;
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
    }
}

public class AddChapterMenuItem : IMenuItem
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
        Debug.Log(Language.GetText(Label));
    }
}

public class DeleteChapterMenuItem : IMenuItem
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
    }
}

public class EditFlagsVariablesMenuItem : IMenuItem
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
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
        Debug.Log(Language.GetText(Label));
    }
}