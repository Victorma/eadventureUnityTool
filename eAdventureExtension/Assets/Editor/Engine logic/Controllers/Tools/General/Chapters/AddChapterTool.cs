using UnityEngine;
using System.Collections;

public class AddChapterTool : Tool
{
    private Controller controller;

    private ChapterListDataControl chaptersController;

    private Chapter newChapter;

    private int index;

    private string chapterTitle;

    public AddChapterTool(ChapterListDataControl chaptersController)
    {

        this.chaptersController = chaptersController;
        this.controller = Controller.getInstance();
    }

    public override bool canRedo()
    {

        return true;
    }
    
    public override bool canUndo()
    {

        return true;
    }
    
    public override bool combine(Tool other)
    {

        return false;
    }
    
    public override bool doTool()
    {

        // Show a dialog asking for the chapter title
        chapterTitle = controller.showInputDialog(TC.get("Operation.AddChapterTitle"), TC.get("Operation.AddChapterMessage"), TC.get("Operation.AddChapterDefaultValue"));

        // If some value was typed
        if (chapterTitle != null)
        {
            if (!chaptersController.exitsChapter(chapterTitle))
            {
                // Create the new chapter, and the controller
                newChapter = new Chapter(chapterTitle, TC.get("DefaultValue.SceneId"));
                chaptersController.addChapterDataControl(newChapter);
                index = chaptersController.getSelectedChapter();

                //controller.reloadData();
                return true;
            }
            else {
                controller.showErrorDialog(TC.get("Operation.CreateAdaptationFile.FileName.ExistValue.Title"), TC.get("Operation.NewChapter.ExistingName"));
                return false;
            }
        }
        return false;

    }
    
    public override bool redoTool()
    {

        // Create the new chapter, and the controller
        newChapter = new Chapter(chapterTitle, TC.get("DefaultValue.SceneId"));
        chaptersController.addChapterDataControl(newChapter);
        index = chaptersController.getSelectedChapter();

        //controller.reloadData();
        return true;
    }
    
    public override bool undoTool()
    {

        bool done = (chaptersController.removeChapterDataControl(index)) != null;
        //controller.reloadData();
        return done;
    }
}
