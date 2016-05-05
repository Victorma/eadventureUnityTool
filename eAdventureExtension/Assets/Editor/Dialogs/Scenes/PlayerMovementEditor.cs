using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Text.RegularExpressions;

public class PlayerMovementEditor : BaseAreaEditablePopup
{
    private SceneDataControl sceneRef;
    private Texture2D backgroundPreviewTex = null;
    private Texture2D selectedPlayerTex = null;
    private Texture2D playerTex = null;

    private Vector2 scrollPosition;

    private Rect imageBackgroundRect;

    bool dragging = false;
    Vector2 startPos;
    Vector2 currentPos;

    private bool useTrajectory;
    private bool useInitialPosition, useInitialPositionLast;

    private string xStringInitialPos, yStringInitialPos, scaleInitialPos;
    private string xStringInitialPosLast, yStringInitialPosLast, scaleInitialPosLast;

    // Wrzucić do listy?
    private Rect playerRect;
    private float nextActionTime = 0.0f;
    public float period = 0.3f;

    public void Init(DialogReceiverInterface e, SceneDataControl scene)
    {
        sceneRef = scene;

        string backgroundPath =
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getPreviewBackground();
        backgroundPreviewTex =
            (Texture2D) Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof (Texture2D));

        string playerPath =
            Controller.getInstance().getSelectedChapterDataControl().getPlayer().getPreviewImage();
        playerTex = (Texture2D) Resources.Load(playerPath.Substring(0, playerPath.LastIndexOf(".")), typeof (Texture2D));

        selectedPlayerTex = (Texture2D) Resources.Load("Editor/SelectedArea", typeof (Texture2D));

        imageBackgroundRect = new Rect(0f, 0f, backgroundPreviewTex.width, backgroundPreviewTex.height);
        playerRect = new Rect(0f, 0f, playerTex.width, playerTex.height);

        useInitialPosition = useInitialPositionLast = sceneRef.hasDefaultInitialPosition();

        useTrajectory = !useInitialPosition;

        xStringInitialPos = xStringInitialPosLast = sceneRef.getDefaultInitialPositionX().ToString();
        yStringInitialPos = yStringInitialPosLast = sceneRef.getDefaultInitialPositionY().ToString();
        scaleInitialPos = scaleInitialPosLast = sceneRef.getPlayerScale().ToString();

        playerRect = new Rect(float.Parse(xStringInitialPos), float.Parse(yStringInitialPos), playerTex.width, playerTex.height);

        base.Init(e, backgroundPreviewTex.width, backgroundPreviewTex.height);
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUI.DrawTexture(imageBackgroundRect, backgroundPreviewTex);
        //if (dragging)
        //    GUI.DrawTexture(playerRect, selectedPlayerTex);
        GUI.DrawTexture(playerRect, playerTex);
        GUILayout.EndScrollView();

        if (Event.current.type == EventType.mouseDrag)
        {
            // Check if start position is over player
            if (playerRect.Contains(Event.current.mousePosition))
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

        if (dragging)
        {
            OnBeingDragged();
        }
        /*
        * Properties part
        */
        GUILayout.Label("Use trajectory or initial position");
        GUILayout.Space(5);
        useTrajectory = GUILayout.Toggle(!useInitialPosition, new GUIContent("Use trajectory"));
        useInitialPosition = GUILayout.Toggle(!useTrajectory, new GUIContent("Use initial position"));
        if (useInitialPosition != useInitialPositionLast)
            OnMovementTypeChange(useInitialPosition);
        GUILayout.Space(5);

        if (useInitialPosition)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Box("X", GUILayout.Width(0.33f * backgroundPreviewTex.width));
            GUILayout.Box("Y", GUILayout.Width(0.33f * backgroundPreviewTex.width));
            GUILayout.Box("Scale", GUILayout.Width(0.3f * backgroundPreviewTex.width));
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            xStringInitialPos = GUILayout.TextField(xStringInitialPos, GUILayout.Width(0.33f * backgroundPreviewTex.width));
            xStringInitialPos = (Regex.Match(xStringInitialPos, "^[0-9]{1,4}$").Success ? xStringInitialPos : xStringInitialPosLast);
            if (!xStringInitialPos.Equals(xStringInitialPosLast))
                OnChangeXInitialPos(xStringInitialPos);

            yStringInitialPos = GUILayout.TextField(yStringInitialPos, GUILayout.Width(0.33f * backgroundPreviewTex.width));
            yStringInitialPos = (Regex.Match(yStringInitialPos, "^[0-9]{1,4}$").Success ? yStringInitialPos : yStringInitialPosLast);
            if (!yStringInitialPos.Equals(yStringInitialPosLast))
                OnChangeYInitialPos(yStringInitialPos);

            scaleInitialPos = GUILayout.TextField(scaleInitialPos, GUILayout.Width(0.33f * backgroundPreviewTex.width));
            scaleInitialPos = (Regex.Match(scaleInitialPos, "^(\\d+[\\.]\\d*$)").Success ? scaleInitialPos : scaleInitialPosLast);
            if (!scaleInitialPos.Equals(scaleInitialPosLast) && !scaleInitialPos.EndsWith("."))
                OnChangeScaleInitialPos(scaleInitialPos);
            GUILayout.EndHorizontal();
        }
    }

    private void OnBeingDragged()
    {
        sceneRef.setDefaultInitialPosition((int)currentPos.x, (int)currentPos.y);
        xStringInitialPos = xStringInitialPosLast = sceneRef.getDefaultInitialPositionX().ToString();
        yStringInitialPos = yStringInitialPosLast = sceneRef.getDefaultInitialPositionY().ToString();

        UpdatePlayerRect();
    }

    private void UpdatePlayerRect()
    {
        float scale = float.Parse(scaleInitialPos);
        playerRect = new Rect(currentPos.x - 0.5f * playerTex.width * scale, currentPos.y - 0.5f * playerTex.height * scale, playerTex.width * scale, playerTex.height * scale);
    }

    private void OnMovementTypeChange(bool val)
    {
        useInitialPositionLast = val;
    }

    void OnChangeXInitialPos(string val)
    {
        xStringInitialPosLast = val;
        sceneRef.setDefaultInitialPosition(int.Parse(xStringInitialPos), int.Parse(yStringInitialPos));

        float y = currentPos.y;
        currentPos = new Vector2(float.Parse(xStringInitialPos), y);
        UpdatePlayerRect();
    }

    void OnChangeYInitialPos(string val)
    {
        yStringInitialPosLast = val;
        sceneRef.setDefaultInitialPosition(int.Parse(xStringInitialPos), int.Parse(yStringInitialPos));

        float x = currentPos.x;
        currentPos = new Vector2(x, float.Parse(yStringInitialPos));
        UpdatePlayerRect();
    }

    void OnChangeScaleInitialPos(string val)
    {
        scaleInitialPosLast = val;
        sceneRef.setPlayerScale(float.Parse(scaleInitialPos));

        UpdatePlayerRect();
    }


    //void Update()
    //{
    //    if (EditorApplication.timeSinceStartup > nextActionTime)
    //    {
    //        nextActionTime = (float)EditorApplication.timeSinceStartup + period;
    //        RecalculatePosition();
    //    }
    //}

    //void RecalculatePosition()
    //{
    //    //TODO:
    //    // Granice
    //    // wprowadzenie wielu graczy
    //    playerRect = new Rect(currentPos.x - 0.5f* playerTex.width, currentPos.y - 0.5f * playerTex.height, playerTex.width, playerRect.height);
    //}
}
