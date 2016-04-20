using UnityEditor;
using UnityEngine;

public class AdventureMenu : WindowMenuContainer
{
    private CheckAdventureConsistencyMenuItem consistency;
    private EditAdventureDataMenuItem editAdventureData;
    private VisualisationMenuItem visualisation;
    private ConvertToMenuItem convertTo;
    private DeleteUnusedDataMenuItem deleteUnused;

    public AdventureMenu()
    {
        SetMenuItems();
    }

    protected override void Callback(object obj)
    {
        if ((obj as CheckAdventureConsistencyMenuItem) != null)
            consistency.OnCliked();
        else if ((obj as EditAdventureDataMenuItem) != null)
            editAdventureData.OnCliked();
        else if ((obj as VisualisationMenuItem) != null)
            visualisation.OnCliked();
        else if ((obj as ConvertToMenuItem) != null)
            convertTo.OnCliked();
        else if ((obj as DeleteUnusedDataMenuItem) != null)
            deleteUnused.OnCliked();
    }

    protected override void SetMenuItems()
    {
        menu = new GenericMenu();

        consistency = new CheckAdventureConsistencyMenuItem("CHECK_ADVENTURE_CONSISTENCY");
        editAdventureData = new EditAdventureDataMenuItem("EDIT_ADVENTURE_DATA");
        visualisation = new VisualisationMenuItem("VISUALIZATION");
        convertTo = new ConvertToMenuItem("CONVERT_TO");
        deleteUnused = new DeleteUnusedDataMenuItem("DELETE_UNUSED_DATA");

        menu.AddItem(new GUIContent(Language.GetText(consistency.Label)), false, Callback, consistency);
        menu.AddItem(new GUIContent(Language.GetText(editAdventureData.Label)), false, Callback, editAdventureData);
        menu.AddItem(new GUIContent(Language.GetText(visualisation.Label)), false, Callback, visualisation);
        menu.AddItem(new GUIContent(Language.GetText(convertTo.Label)), false, Callback, convertTo);
        menu.AddItem(new GUIContent(Language.GetText(deleteUnused.Label)), false, Callback, deleteUnused);
    }
}
