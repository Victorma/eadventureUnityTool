using System;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor;

public class ActiveAreasEditor : BaseAreaEditablePopup
{
    private SceneDataControl sceneRef;
    private Texture2D backgroundPreviewTex = null;
    private Texture2D activeAreaTex = null;
    private Texture2D selectedAreaTex = null;
    private Texture2D exitTex = null;

    private Rect imageBackgroundRect;
    private Vector2 scrollPosition;

    private string xString, yString, widthString, heightString;
    private string xStringLast, yStringLast, widthStringLast, heightStringLast;

    private int calledAreaIndexRef;

    private Rect currentRect;
    private bool dragging;
    private Vector2 startPos;
    private Vector2 currentPos;

    public void Init(DialogReceiverInterface e, SceneDataControl scene, int areaIndex)
    {
        sceneRef = scene;
        calledAreaIndexRef = areaIndex;

        string backgroundPath =
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getPreviewBackground();

        backgroundPreviewTex =
            (Texture2D) Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof (Texture2D));

        activeAreaTex = (Texture2D) Resources.Load("Editor/ActiveArea", typeof (Texture2D));
        exitTex = (Texture2D) Resources.Load("Editor/ExitArea", typeof (Texture2D));
        selectedAreaTex = (Texture2D) Resources.Load("Editor/SelectedArea", typeof (Texture2D));

        imageBackgroundRect = new Rect(0f, 0f, backgroundPreviewTex.width, backgroundPreviewTex.height);

        xString = xStringLast = sceneRef.getActiveAreasList().getActiveAreasList()[areaIndex].getX().ToString();
        yString = yStringLast = sceneRef.getActiveAreasList().getActiveAreasList()[areaIndex].getY().ToString();
        widthString =
            widthStringLast = sceneRef.getActiveAreasList().getActiveAreasList()[areaIndex].getWidth().ToString();
        heightString =
            heightStringLast = sceneRef.getActiveAreasList().getActiveAreasList()[areaIndex].getHeight().ToString();

        base.Init(e, backgroundPreviewTex.width, backgroundPreviewTex.height);
    }

    void OnGUI()
    {
        // Dragging events
        if (Event.current.type == EventType.mouseDrag)
        {
            if (currentRect.Contains(Event.current.mousePosition))
            {
                if (!dragging)
                {
                    dragging = true;
                    startPos = currentPos;
                }
            }
            currentPos = Event.current.mousePosition;
        }

        if (Event.current.type == EventType.mouseUp)
        {
            dragging = false;
        }

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

            // Frame around current area
            if (calledAreaIndexRef == i)
            {
                currentRect = aRect;
                GUI.DrawTexture(aRect, selectedAreaTex);
            }
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
        }

        /*
         *HANDLE EVENTS
         */
        if (dragging)
        {
            OnBeingDragged();
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
        //if (GUILayout.Button("Cancel"))
        //{
        //    reference.OnDialogCanceled();
        //    this.Close();
        //}
        GUILayout.EndHorizontal();
    }

    void OnChangeX(string val)
    {
        xStringLast = val;
        sceneRef.getActiveAreasList().getActiveAreasList()[calledAreaIndexRef].setValues(int.Parse(xString),
            int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeY(string val)
    {
        yStringLast = val;
        sceneRef.getActiveAreasList().getActiveAreasList()[calledAreaIndexRef].setValues(int.Parse(xString),
            int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeWidth(string val)
    {
        widthStringLast = val;
        sceneRef.getActiveAreasList().getActiveAreasList()[calledAreaIndexRef].setValues(int.Parse(xString),
            int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeHeight(string val)
    {
        heightStringLast = val;
        sceneRef.getActiveAreasList().getActiveAreasList()[calledAreaIndexRef].setValues(int.Parse(xString),
            int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }

    private void OnBeingDragged()
    {
        xStringLast = xString = ((int) currentPos.x - (int) (0.5f*int.Parse(widthString))).ToString();
        yStringLast = yString = ((int) currentPos.y - (int) (0.5f*int.Parse(heightString))).ToString();
        sceneRef.getActiveAreasList().getActiveAreasList()[calledAreaIndexRef].setValues(int.Parse(xString),
            int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }
}