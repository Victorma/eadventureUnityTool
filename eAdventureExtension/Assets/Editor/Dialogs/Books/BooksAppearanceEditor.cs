using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class BooksAppearanceEditor : BaseAreaEditablePopup
{
    private BookDataControl bookRef;

    private Texture2D backgroundPreviewTex = null;
    private Texture2D leftArrowTex = null;
    private Texture2D rightArrowTex = null;

    private Rect imageBackgroundRect, leftArrowRect, rightArrowRect;
    private Vector2 scrollPosition;

    private string xLeft, yLeft, xRight, yRight;
    private string xLeftLast, yLeftLast, xRightLast, yRightLast;

    private Vector2 defaultPreviousPageArrowPosition, defaultNextPageArrowPosition;

    private const float MARGIN = 20.0f;

    public void Init(DialogReceiverInterface e, BookDataControl book)
    {
        bookRef = book;

        string backgroundPath = book.getPreviewImage();

        backgroundPreviewTex = (Texture2D)Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof(Texture2D));

        string leftNormalArrowPath = book.getArrowImagePath_WithDefault(BookDataControl.ARROW_LEFT,
                   BookDataControl.ARROW_NORMAL);

        leftArrowTex = (Texture2D)Resources.Load(leftNormalArrowPath.Substring(0, leftNormalArrowPath.LastIndexOf(".")), typeof(Texture2D));

        string rightNormalArrowPath = book.getArrowImagePath_WithDefault(BookDataControl.ARROW_RIGHT,
                BookDataControl.ARROW_NORMAL);

        rightArrowTex = (Texture2D)Resources.Load(rightNormalArrowPath.Substring(0, rightNormalArrowPath.LastIndexOf(".")), typeof(Texture2D));

        imageBackgroundRect = new Rect(0f, 0f, backgroundPreviewTex.width, backgroundPreviewTex.height);

        defaultPreviousPageArrowPosition= new Vector2(MARGIN, backgroundPreviewTex.height - leftArrowTex.height - MARGIN);
        defaultNextPageArrowPosition = new Vector2(backgroundPreviewTex.width - rightArrowTex.width - MARGIN, backgroundPreviewTex.height - rightArrowTex.height - MARGIN);

        if(bookRef.getPreviousPagePosition() == Vector2.zero && bookRef.getNextPagePosition() == Vector2.zero)
            SetDefaultArrowsPosition();

        xLeft = xLeftLast = ((int)bookRef.getPreviousPagePosition().x).ToString();
        yLeft = yLeftLast = ((int)bookRef.getPreviousPagePosition().y).ToString();
        xRight = xRightLast = ((int)bookRef.getNextPagePosition().x).ToString();
        yRight = yRightLast = ((int)bookRef.getNextPagePosition().y).ToString();

        CalculateArrowsPosition();

        base.Init(e, backgroundPreviewTex.width, backgroundPreviewTex.height);
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUI.DrawTexture(imageBackgroundRect, backgroundPreviewTex);

        GUI.DrawTexture(leftArrowRect, leftArrowTex);
        GUI.DrawTexture(rightArrowRect, rightArrowTex);

        GUILayout.EndScrollView();

        // Default arrow positions
        if (GUILayout.Button("Default"))
        {
            OnDefaultClicked();
        }



        GUILayout.BeginHorizontal();
        GUILayout.Box("Previous page", GUILayout.Width(0.5f * backgroundPreviewTex.width));
        GUILayout.Box("Next page", GUILayout.Width(0.5f * backgroundPreviewTex.width));
        GUILayout.EndHorizontal();



        GUILayout.BeginHorizontal();

        GUILayout.Box("X", GUILayout.Width(0.15f * backgroundPreviewTex.width));
        xLeft = GUILayout.TextField(xLeft, GUILayout.Width(0.35f * backgroundPreviewTex.width));
        xLeft = (Regex.Match(xLeft, "^[0-9]{1,4}$").Success ? xLeft : xLeftLast);
        if (!xLeft.Equals(xLeftLast))
            OnChangeLeftX(xLeft);

        GUILayout.Box("X", GUILayout.Width(0.15f * backgroundPreviewTex.width));
        xRight = GUILayout.TextField(xRight, GUILayout.Width(0.35f * backgroundPreviewTex.width));
        xRight = (Regex.Match(xRight, "^[0-9]{1,4}$").Success ? xRight : xRightLast);
        if (!xRight.Equals(xRightLast))
            OnChangeRightX(xRight);

        GUILayout.EndHorizontal();



        GUILayout.BeginHorizontal();

        GUILayout.Box("Y", GUILayout.Width(0.15f * backgroundPreviewTex.width));
        yLeft = GUILayout.TextField(yLeft, GUILayout.Width(0.35f * backgroundPreviewTex.width));
        yLeft = (Regex.Match(yLeft, "^[0-9]{1,4}$").Success ? yLeft : yLeftLast);
        if (!yLeft.Equals(yLeftLast))
            OnChangeLeftY(yLeft);

        GUILayout.Box("Y", GUILayout.Width(0.15f * backgroundPreviewTex.width));
        yRight = GUILayout.TextField(yRight, GUILayout.Width(0.35f * backgroundPreviewTex.width));
        yRight = (Regex.Match(yRight, "^[0-9]{1,4}$").Success ? yRight : yRightLast);
        if (!yRight.Equals(yRightLast))
            OnChangeRightY(yRight);

        GUILayout.EndHorizontal();



        GUILayout.BeginHorizontal();
        if (GUILayout.Button("End"))
        {
            reference.OnDialogOk("Applied");
            this.Close();
        }
        GUILayout.EndHorizontal();
    }

    private void CalculateArrowsPosition()
    {
        leftArrowRect = new Rect(float.Parse(xLeft), float.Parse(yLeft), leftArrowTex.width, leftArrowTex.height);
        rightArrowRect = new Rect(float.Parse(xRight), float.Parse(yRight), rightArrowTex.width, rightArrowTex.height);
    }

    private void SetDefaultArrowsPosition()
    {
        bookRef.setPreviousPagePosition(defaultPreviousPageArrowPosition);
        bookRef.setNextPagePosition(defaultNextPageArrowPosition);

        xLeft = xLeftLast = ((int)bookRef.getPreviousPagePosition().x).ToString();
        yLeft = yLeftLast = ((int)bookRef.getPreviousPagePosition().y).ToString();
        xRight = xRightLast = ((int)bookRef.getNextPagePosition().x).ToString();
        yRight = yRightLast = ((int)bookRef.getNextPagePosition().y).ToString();

        CalculateArrowsPosition();
    }

    void OnChangeLeftX(string val)
    {
        xLeftLast = val;
        bookRef.setPreviousPagePosition(new Vector2(float.Parse(xLeft), float.Parse(yLeft)));
        CalculateArrowsPosition();
    }

    void OnChangeLeftY(string val)
    {
        yLeftLast = val;
        bookRef.setPreviousPagePosition(new Vector2(float.Parse(xLeft), float.Parse(yLeft)));
        CalculateArrowsPosition();
    }

    void OnChangeRightX(string val)
    {
        xRightLast = val;
        bookRef.setNextPagePosition(new Vector2(float.Parse(xRight), float.Parse(yRight)));
        CalculateArrowsPosition();
    }

    void OnChangeRightY(string val)
    {
        yRightLast = val;
        bookRef.setNextPagePosition(new Vector2(float.Parse(xRight), float.Parse(yRight)));
        CalculateArrowsPosition();
    }

    void OnDefaultClicked()
    {
        SetDefaultArrowsPosition();
    }
}
