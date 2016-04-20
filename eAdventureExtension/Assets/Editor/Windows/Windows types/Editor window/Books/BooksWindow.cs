using UnityEngine;
using System.Collections;

public class BooksWindow : LayoutWindow
{
    private enum BookWindowType { Appearance, Content, Documentation}

    private static BookWindowType openedWindow = BookWindowType.Appearance;
    private static BooksWindowAppearance booksWindowAppearance;
    private static BooksWindowContents booksWindowContents;
    private static BooksWindowDocumentation booksWindowDocumentation;

    // Two methods responsible for showing right window content 
    // - concrete item info or base window view
    public void ShowBaseWindowView()
    {
        isConcreteItemVisible = false;
        GameRources.GetInstance().selectedBookIndex = -1;
    }

    public void ShowItemWindowView(int o)
    {
        isConcreteItemVisible = true;
        GameRources.GetInstance().selectedBookIndex = o;
    }

    // Flag determining visibility of concrete item information
    private bool isConcreteItemVisible = false;

    public BooksWindow(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        booksWindowAppearance = new BooksWindowAppearance(aStartPos, new GUIContent(Language.GetText("APPEARANCE")), "Window");
        booksWindowContents = new BooksWindowContents(aStartPos, new GUIContent(Language.GetText("CONTENTS")), "Window");
        booksWindowDocumentation = new BooksWindowDocumentation(aStartPos, new GUIContent(Language.GetText("DOCUMENTATION")), "Window");
    }


    public override void Draw(int aID)
    {
        // Show information of concrete item
        if (isConcreteItemVisible)
        {
            /**
            UPPER MENU
            */
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(Language.GetText("APPEARANCE")))
            {
                OnWindowTypeChanged(BookWindowType.Appearance);
            }
            if (GUILayout.Button(Language.GetText("DOCUMENTATION")))
            {
                OnWindowTypeChanged(BookWindowType.Documentation);
            }
            if (GUILayout.Button(Language.GetText("CONTENTS")))
            {
                OnWindowTypeChanged(BookWindowType.Content);
            }
            GUILayout.EndHorizontal();

            switch (openedWindow)
            {
                case BookWindowType.Appearance:
                    booksWindowAppearance.Draw(aID);
                    break;
                case BookWindowType.Documentation:
                    booksWindowDocumentation.Draw(aID);
                    break;
                case BookWindowType.Content:
                    booksWindowContents.Draw(aID);
                    break;
            }
        }
        else
        {
            GUILayout.Label("BooksWindow");
        }
    }

    void OnWindowTypeChanged(BookWindowType type_)
    {
        openedWindow = type_;
    }
}