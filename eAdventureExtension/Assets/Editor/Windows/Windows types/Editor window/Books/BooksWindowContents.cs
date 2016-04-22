using UnityEngine;
using System.Collections;

public class BooksWindowContents : LayoutWindow
{
    private static float windowWidth, windowHeight;

    private Texture2D addTex = null;
    private Texture2D moveUpTex, moveDownTex = null;
    private Texture2D clearTex = null;

    private Texture2D titleParagraphTex = null;
    private Texture2D textParagraphTex = null;
    private Texture2D bulletParagraphTex = null;
    private Texture2D imageParagraphTex = null;

    // Variables for storing paragraph type information
    private Texture2D tmpTexture = null;
    private string tmpParagraphTypeName = "";

    private static Vector2 scrollPosition;

    private static GUISkin selectedElementSkin;
    private static GUISkin defaultSkin;
    private static GUISkin noBackgroundSkin;

    private static Rect tableRect;
    private static Rect previewRect;
    private Rect rightPanelRect;

    private int selectedElement;

    private string editableFieldContent = "";

    public BooksWindowContents(Rect aStartPos, GUIContent aContent, GUIStyle aStyle, params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        clearTex = (Texture2D) Resources.Load("EAdventureData/img/icons/deleteContent", typeof (Texture2D));
        addTex = (Texture2D) Resources.Load("EAdventureData/img/icons/addNode", typeof (Texture2D));
        moveUpTex = (Texture2D) Resources.Load("EAdventureData/img/icons/moveNodeUp", typeof (Texture2D));
        moveDownTex = (Texture2D) Resources.Load("EAdventureData/img/icons/moveNodeDown", typeof (Texture2D));

        titleParagraphTex =
            (Texture2D) Resources.Load("EAdventureData/img/icons/titleBookParagraph", typeof (Texture2D));
        textParagraphTex =
            (Texture2D) Resources.Load("EAdventureData/img/icons/bulletBookParagraph", typeof (Texture2D));
        bulletParagraphTex =
            (Texture2D) Resources.Load("EAdventureData/img/icons/bulletBookParagraph", typeof (Texture2D));
        imageParagraphTex =
            (Texture2D) Resources.Load("EAdventureData/img/icons/imageBookParagraph", typeof (Texture2D));

        selectedElementSkin = (GUISkin) Resources.Load("Editor/EditorLeftMenuItemSkinConcreteOptions", typeof (GUISkin));
        noBackgroundSkin = (GUISkin) Resources.Load("Editor/EditorNoBackgroundSkin", typeof (GUISkin));

        tableRect = new Rect(0f, 0.1f*windowHeight, 0.9f*windowWidth, windowHeight*0.33f);
        rightPanelRect = new Rect(0.9f*windowWidth, 0.1f*windowHeight, 0.08f*windowWidth, 0.33f*windowHeight);
        previewRect = new Rect(0f, 0.5f*windowHeight, windowWidth, windowHeight*0.45f);

        selectedElement = -1;
    }

    public override void Draw(int aID)
    {
        GUILayout.BeginArea(tableRect);
        GUILayout.BeginHorizontal();
        GUILayout.Box("Paragraph type", GUILayout.Width(windowWidth*0.19f));
        GUILayout.Box("Content", GUILayout.Width(windowWidth*0.69f));
        GUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0;
            i <
            Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs().Count;
            i++)
        {
            GUI.skin = noBackgroundSkin;
            if (i == selectedElement)
                GUI.skin = selectedElementSkin;

            GUILayout.BeginHorizontal();

            switch (Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs()[
                    i].getType())
            {
                case Controller.BOOK_TITLE_PARAGRAPH:
                    tmpTexture = titleParagraphTex;
                    tmpParagraphTypeName = "Title paragraph";
                    break;
                case Controller.BOOK_BULLET_PARAGRAPH:
                    tmpTexture = bulletParagraphTex;
                    tmpParagraphTypeName = "Bullet paragraph";
                    break;
                case Controller.BOOK_TEXT_PARAGRAPH:
                    tmpTexture = textParagraphTex;
                    tmpParagraphTypeName = "Text paragraph";
                    break;
                case Controller.BOOK_IMAGE_PARAGRAPH:
                    tmpTexture = imageParagraphTex;
                    tmpParagraphTypeName = "Image paragraph";
                    break;
            }

            if (GUILayout.Button(new GUIContent(tmpParagraphTypeName, tmpTexture),
                GUILayout.Width(windowWidth*0.19f), GUILayout.MaxHeight(0.05f*windowHeight)))
            {
                selectedElement = i;
            }

            if (selectedElement != i)
            {
                if (GUILayout.Button(Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                    GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs()[
                        i].getParagraphContent(),
                    GUILayout.Width(windowWidth*0.69f)))
                {
                    selectedElement = i;
                }
            }
            else
            {
                editableFieldContent =
                    Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                        GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs()[
                            i].getParagraphContent();
                editableFieldContent = GUILayout.TextField(editableFieldContent, GUILayout.Width(0.69f*windowWidth));
                Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                    GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs()[
                        i].setParagraphTextContent(editableFieldContent);
            }

            GUILayout.EndHorizontal();
            GUI.skin = defaultSkin;
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        /*
        * Right panel
        */
        GUILayout.BeginArea(rightPanelRect);
        GUI.skin = noBackgroundSkin;
        if (GUILayout.Button(addTex, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("ADD");
        }
        if (GUILayout.Button(moveUpTex, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Up");
            Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().moveElementUp(
                    Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                        GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs()[
                            selectedElement]);
        }
        if (GUILayout.Button(moveDownTex, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Down");
            Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().moveElementDown(
                    Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                        GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs()[
                            selectedElement]);
        }
        if (GUILayout.Button(clearTex, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Clear");
            Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().deleteElement(
                    Controller.getInstance().getSelectedChapterDataControl().getBooksList().getBooks()[
                        GameRources.GetInstance().selectedBookIndex].getBookParagraphsList().getBookParagraphs()[
                            selectedElement], false);
        }
        GUI.skin = defaultSkin;
        GUILayout.EndArea();
    }
}