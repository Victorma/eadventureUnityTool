using UnityEditor;
using UnityEngine;

public class ChaptersMenu : WindowMenuContainer
{
    private AddChapterMenuItem add;
    private DeleteChapterMenuItem delete;
    private ImportChapterMenuItem import;
    private EditFlagsVariablesMenuItem variablesFlags;

    public ChaptersMenu()
    {
        SetMenuItems();
    }

    protected override void Callback(object obj)
    {
        if ((obj as AddChapterMenuItem) != null)
            add.OnCliked();
        else if ((obj as DeleteChapterMenuItem) != null)
            delete.OnCliked();
        else if ((obj as ImportChapterMenuItem) != null)
            import.OnCliked();
        else if ((obj as EditFlagsVariablesMenuItem) != null)
            variablesFlags.OnCliked();
    }

    protected override void SetMenuItems()
    {
        menu = new GenericMenu();

        add = new AddChapterMenuItem("ADD_CHAPTER");
        delete = new DeleteChapterMenuItem("DELETE_CHAPTER");
        import = new ImportChapterMenuItem("IMPORT_CHAPTER");
        variablesFlags = new EditFlagsVariablesMenuItem("EDIT_FLAGS_VARIABLES");

        menu.AddItem(new GUIContent(Language.GetText(add.Label)), false, Callback, add);
        menu.AddItem(new GUIContent(Language.GetText(delete.Label)), false, Callback, delete);
        menu.AddItem(new GUIContent(Language.GetText(import.Label)), false, Callback, import);
        menu.AddSeparator("");
        menu.AddItem(new GUIContent(Language.GetText(variablesFlags.Label)), false, Callback, variablesFlags);
    }
}
