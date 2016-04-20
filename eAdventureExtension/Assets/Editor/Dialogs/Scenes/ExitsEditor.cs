using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ExitsEditor : BaseAreaEditablePopup
{
    private SceneDataControl sceneRef;

    private Texture2D backgroundPreviewTex = null;

    private Texture2D activeAreaTex = null;
    private Texture2D exitTex = null;
    private Texture2D selectedExitTex = null;

    private Rect imageBackgroundRect;
    private Vector2 scrollPosition;

    private string xString, yString, widthString, heightString;
    private string xStringLast, yStringLast, widthStringLast, heightStringLast;

    private int calledExitIndexRef;

    public void Init(DialogReceiverInterface e, SceneDataControl scene, int exitIndex)
    {
        sceneRef = scene;
        calledExitIndexRef = exitIndex;

        string backgroundPath =
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getPreviewBackground();

        backgroundPreviewTex =
            (Texture2D) Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof (Texture2D));

        activeAreaTex = (Texture2D) Resources.Load("Editor/ActiveArea", typeof (Texture2D));
        exitTex = (Texture2D) Resources.Load("Editor/ExitArea", typeof (Texture2D));
        selectedExitTex = (Texture2D) Resources.Load("Editor/SelectedArea", typeof (Texture2D));

        imageBackgroundRect = new Rect(0f, 0f, backgroundPreviewTex.width, backgroundPreviewTex.height);

        xString = xStringLast = sceneRef.getExitsList().getExitsList()[exitIndex].getX().ToString();
        yString = yStringLast = sceneRef.getExitsList().getExitsList()[exitIndex].getY().ToString();
        widthString = widthStringLast = sceneRef.getExitsList().getExitsList()[exitIndex].getWidth().ToString();
        heightString = heightStringLast = sceneRef.getExitsList().getExitsList()[exitIndex].getHeight().ToString();

        base.Init(e, backgroundPreviewTex.width, backgroundPreviewTex.height);
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUI.DrawTexture(imageBackgroundRect, backgroundPreviewTex);

        for (int i = 0;
            i <
            sceneRef.getActiveAreasList().getActiveAreasList().Count;
            i++)
        {
            Rect aRect = new Rect(sceneRef.getActiveAreasList().getActiveAreasList()[i].getX(),
                sceneRef.getActiveAreasList().getActiveAreasList()[i].getY(),
                sceneRef.getActiveAreasList().getActiveAreasList()[i].getWidth(),
                sceneRef.getActiveAreasList().getActiveAreasList()[i].getHeight());
            GUI.DrawTexture(aRect, activeAreaTex);
        }

        for (int i = 0;
            i <
            sceneRef.getExitsList().getExitsList().Count;
            i++)
        {
            Rect eRect = new Rect(sceneRef.getExitsList().getExitsList()[i].getX(),
                sceneRef.getExitsList().getExitsList()[i].getY(), sceneRef.getExitsList().getExitsList()[i].getWidth(),
                sceneRef.getExitsList().getExitsList()[i].getHeight());
            GUI.DrawTexture(eRect, exitTex);

            // Frame around current area
            if (calledExitIndexRef == i)
                GUI.DrawTexture(eRect, selectedExitTex);
        }

        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();
        GUILayout.Box("X", GUILayout.Width(0.25f*backgroundPreviewTex.width));
        GUILayout.Box("Y", GUILayout.Width(0.25f*backgroundPreviewTex.width));
        GUILayout.Box("Width", GUILayout.Width(0.25f*backgroundPreviewTex.width));
        GUILayout.Box("Height", GUILayout.Width(0.25f*backgroundPreviewTex.width));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        xString = GUILayout.TextField(xString, GUILayout.Width(0.25f*backgroundPreviewTex.width));
        xString = (Regex.Match(xString, "^[0-9]{1,4}$").Success ? xString : xStringLast);
        if (!xString.Equals(xStringLast))
            OnChangeX(xString);

        yString = GUILayout.TextField(yString, GUILayout.Width(0.25f*backgroundPreviewTex.width));
        yString = (Regex.Match(yString, "^[0-9]{1,4}$").Success ? yString : yStringLast);
        if (!yString.Equals(yStringLast))
            OnChangeY(yString);

        widthString = GUILayout.TextField(widthString, GUILayout.Width(0.25f*backgroundPreviewTex.width));
        widthString = (Regex.Match(widthString, "^[0-9]{1,4}$").Success ? widthString : widthStringLast);
        if (!widthString.Equals(widthStringLast))
            OnChangeWidth(widthString);

        heightString = GUILayout.TextField(heightString, GUILayout.Width(0.25f*backgroundPreviewTex.width));
        heightString = (Regex.Match(heightString, "^[0-9]{1,4}$").Success ? heightString : heightStringLast);
        if (!heightString.Equals(heightStringLast))
            OnChangeHeight(heightString);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Ok"))
        {
            reference.OnDialogOk("Applied");
            this.Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            reference.OnDialogCanceled();
            this.Close();
        }
        GUILayout.EndHorizontal();
    }

    void OnChangeX(string val)
    {
        xStringLast = val;
        sceneRef.getExitsList().getExitsList()[calledExitIndexRef].setValues(int.Parse(xString), int.Parse(yString),
            int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeY(string val)
    {
        yStringLast = val;
        sceneRef.getExitsList().getExitsList()[calledExitIndexRef].setValues(int.Parse(xString), int.Parse(yString),
            int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeWidth(string val)
    {
        widthStringLast = val;
        sceneRef.getExitsList().getExitsList()[calledExitIndexRef].setValues(int.Parse(xString), int.Parse(yString),
            int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeHeight(string val)
    {
        heightStringLast = val;
        sceneRef.getExitsList().getExitsList()[calledExitIndexRef].setValues(int.Parse(xString), int.Parse(yString),
            int.Parse(widthString), int.Parse(heightString));
    }
}