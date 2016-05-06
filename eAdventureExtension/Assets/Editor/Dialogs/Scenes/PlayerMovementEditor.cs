using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Text.RegularExpressions;

public class PlayerMovementEditor : BaseAreaEditablePopup
{
    private enum TrajectoryToolType
    {
        EDIT_NODE,
        EDIT_SIDE,
        INIT_NODE,
        DELETE_NODE
    }


    private SceneDataControl sceneRef;
    private Texture2D backgroundPreviewTex = null;
    private Texture2D selectedPlayerTex = null;
    private Texture2D playerTex = null;

    private static GUISkin selectedAreaSkin;
    private static GUISkin defaultSkin;

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


    // Trajectory
    private Texture2D editNodeTex = null;
    private Texture2D editSideTex = null;
    private Texture2D setInitialNodeTex = null;
    private Texture2D deleteTex = null;
    private Texture2D initialNodeTex = null;

    private Trajectory trajectory;

    private TrajectoryToolType trajectoryTool;

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

        editNodeTex = (Texture2D) Resources.Load("EAdventureData/img/icons/nodeEdit", typeof (Texture2D));
        editSideTex = (Texture2D) Resources.Load("EAdventureData/img/icons/sideEdit", typeof (Texture2D));
        setInitialNodeTex = (Texture2D) Resources.Load("EAdventureData/img/icons/selectStartNode", typeof (Texture2D));
        deleteTex = (Texture2D) Resources.Load("EAdventureData/img/icons/deleteTool", typeof (Texture2D)); 

        initialNodeTex = (Texture2D)Resources.Load("EAdventureData/img/icons/selectStartNode", typeof(Texture2D));

        selectedAreaSkin = (GUISkin) Resources.Load("Editor/ButtonSelected", typeof (GUISkin));

        imageBackgroundRect = new Rect(0f, 0f, backgroundPreviewTex.width, backgroundPreviewTex.height);
        playerRect = new Rect(0f, 0f, playerTex.width, playerTex.height);

        useTrajectory = sceneRef.getTrajectory().hasTrajectory();
        useInitialPosition = useInitialPositionLast = !useTrajectory;

        trajectory = sceneRef.getTrajectory().GetTrajectory();

        xStringInitialPos = xStringInitialPosLast = sceneRef.getDefaultInitialPositionX().ToString();
        yStringInitialPos = yStringInitialPosLast = sceneRef.getDefaultInitialPositionY().ToString();
        scaleInitialPos = scaleInitialPosLast = sceneRef.getPlayerScale().ToString();

        playerRect = new Rect(float.Parse(xStringInitialPos), float.Parse(yStringInitialPos), playerTex.width,
            playerTex.height);

        base.Init(e, backgroundPreviewTex.width, backgroundPreviewTex.height);
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUI.DrawTexture(imageBackgroundRect, backgroundPreviewTex);
        //if (dragging)
        //    GUI.DrawTexture(playerRect, selectedPlayerTex);
        GUILayout.EndScrollView();

        if (Event.current.type == EventType.mouseDrag)
        {
            if (!useTrajectory)
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
            }
            // For editing trajectory nodes recognizing object under mouse pointer is done
            // during iterating over all nodes
            else
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



        /*
        * Initial positon
        */
        if (useInitialPosition)
        {
            // EVENT
            if (dragging)
            {
                OnBeingDragged();
            }

            GUI.DrawTexture(playerRect, playerTex);

            GUILayout.BeginHorizontal();
            GUILayout.Box("X", GUILayout.Width(0.33f*backgroundPreviewTex.width));
            GUILayout.Box("Y", GUILayout.Width(0.33f*backgroundPreviewTex.width));
            GUILayout.Box("Scale", GUILayout.Width(0.3f*backgroundPreviewTex.width));
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            xStringInitialPos = GUILayout.TextField(xStringInitialPos, GUILayout.Width(0.33f*backgroundPreviewTex.width));
            xStringInitialPos = (Regex.Match(xStringInitialPos, "^[0-9]{1,4}$").Success
                ? xStringInitialPos
                : xStringInitialPosLast);
            if (!xStringInitialPos.Equals(xStringInitialPosLast))
                OnChangeXInitialPos(xStringInitialPos);

            yStringInitialPos = GUILayout.TextField(yStringInitialPos, GUILayout.Width(0.33f*backgroundPreviewTex.width));
            yStringInitialPos = (Regex.Match(yStringInitialPos, "^[0-9]{1,4}$").Success
                ? yStringInitialPos
                : yStringInitialPosLast);
            if (!yStringInitialPos.Equals(yStringInitialPosLast))
                OnChangeYInitialPos(yStringInitialPos);

            scaleInitialPos = GUILayout.TextField(scaleInitialPos, GUILayout.Width(0.33f*backgroundPreviewTex.width));
            scaleInitialPos = (Regex.Match(scaleInitialPos, "^(\\d+[\\.]\\d*$)").Success
                ? scaleInitialPos
                : scaleInitialPosLast);
            if (!scaleInitialPos.Equals(scaleInitialPosLast) && !scaleInitialPos.EndsWith("."))
                OnChangeScaleInitialPos(scaleInitialPos);
            GUILayout.EndHorizontal();
        }
        /*
        * Trajectory
        */
        else
        {
            // EVENTS
            if (Event.current.type == EventType.mouseDown && imageBackgroundRect.Contains(Event.current.mousePosition))
            {
                int clickedIndex = -1;

                for (int i = 0; i < trajectory.getNodes().Count; i++)
                {
                    if (trajectory.getNodes()[i].getEditorRect(playerTex.width, playerTex.height).Contains(Event.current.mousePosition))
                        clickedIndex = i;
                }

                if (trajectoryTool == TrajectoryToolType.EDIT_NODE)
                {
                    if (clickedIndex == -1)
                        AddNode(Event.current.mousePosition);
                }
                else if (trajectoryTool == TrajectoryToolType.DELETE_NODE)
                {
                    if (clickedIndex!= -1)
                        DeleteNode(clickedIndex);
                }

                if (trajectoryTool == TrajectoryToolType.INIT_NODE)
                {
                    if(clickedIndex!= -1)
                        SetInitNode(clickedIndex);
                }
            }
            if (dragging)
            {
                if (trajectoryTool == TrajectoryToolType.EDIT_NODE)
                {
                    int clickedIndex = -1;

                    for (int i = 0; i < trajectory.getNodes().Count; i++)
                    {
                        if (
                            trajectory.getNodes()[i].getEditorRect(playerTex.width, playerTex.height)
                                .Contains(Event.current.mousePosition))
                            clickedIndex = i;
                    }
                    if (clickedIndex != -1)
                        OnBeingDraggedTrajectoryNode(clickedIndex);
                }
            }


            // DRAW NODES
            foreach (Trajectory.Node node in trajectory.getNodes())
            {
                GUI.DrawTexture(node.getEditorRect(playerRect.width, playerRect.height), playerTex);
            }

            // DRAW INITIAL NODE
            if(trajectory.getInitial() != null)
                GUI.DrawTexture(trajectory.getInitial().getEditorRect(initialNodeTex.width, initialNodeTex.height), initialNodeTex);

            // BUTTONS
            GUILayout.BeginHorizontal();

            if (trajectoryTool == TrajectoryToolType.EDIT_NODE)
                GUI.skin = selectedAreaSkin;
            if (GUILayout.Button(editNodeTex, GUILayout.MaxWidth(0.15f*backgroundPreviewTex.width)))
            {
                OnEditNodeSelected();
            }
            if (trajectoryTool == TrajectoryToolType.EDIT_NODE)
                GUI.skin = defaultSkin;

            if (trajectoryTool == TrajectoryToolType.EDIT_SIDE)
                GUI.skin = selectedAreaSkin;
            if (GUILayout.Button(editSideTex, GUILayout.MaxWidth(0.15f*backgroundPreviewTex.width)))
            {
                OnEditSideSelected();
            }
            if (trajectoryTool == TrajectoryToolType.EDIT_SIDE)
                GUI.skin = defaultSkin;

            if (trajectoryTool == TrajectoryToolType.INIT_NODE)
                GUI.skin = selectedAreaSkin;
            if (GUILayout.Button(setInitialNodeTex, GUILayout.MaxWidth(0.15f*backgroundPreviewTex.width)))
            {
                OnInitialNodeSelected();
            }
            if (trajectoryTool == TrajectoryToolType.INIT_NODE)
                GUI.skin = defaultSkin;

            if (trajectoryTool == TrajectoryToolType.DELETE_NODE)
                GUI.skin = selectedAreaSkin;
            if (GUILayout.Button(deleteTex, GUILayout.MaxWidth(0.15f*backgroundPreviewTex.width)))
            {
                OnDeleteNodeSelected();
            }
            if (trajectoryTool == TrajectoryToolType.DELETE_NODE)
                GUI.skin = defaultSkin;

            GUILayout.EndHorizontal();
        }
    }

    private void UpdatePlayerRect()
    {
        float scale = float.Parse(scaleInitialPos);
        float newX = Mathf.Clamp(currentPos.x - 0.5f*playerTex.width*scale, -0.5f*playerTex.width*scale,
            backgroundPreviewTex.width + 0.5f*playerTex.width*scale);
        float newY = Mathf.Clamp(currentPos.y - 0.5f*playerTex.height*scale, -0.5f*playerTex.height*scale,
            backgroundPreviewTex.height + 0.5f*playerTex.height*scale);
        playerRect = new Rect(newX, newY, playerTex.width*scale, playerTex.height*scale);
    }

    private void OnMovementTypeChange(bool val)
    {
        useInitialPositionLast = val;

        if (useTrajectory)
        {
            trajectory = new Trajectory();
            sceneRef.setTrajectory(trajectory);
        }
        else
        {
            sceneRef.setTrajectory(null);
        }
    }

    /*
    * Initial pos
    */

    private void OnBeingDragged()
    {
        if (useInitialPosition)
        {
            sceneRef.setDefaultInitialPosition((int)currentPos.x, (int)currentPos.y);
            xStringInitialPos = xStringInitialPosLast = sceneRef.getDefaultInitialPositionX().ToString();
            yStringInitialPos = yStringInitialPosLast = sceneRef.getDefaultInitialPositionY().ToString();

            UpdatePlayerRect();
        }
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


    /*
    * Trajectory
    */

    private void OnBeingDraggedTrajectoryNode(int nodeIndex)
    {
        if (useTrajectory)
        {
            trajectory.getNodes()[nodeIndex].setValues((int)currentPos.x, (int)currentPos.y, trajectory.getNodes()[nodeIndex].getScale());
        }
    }

    void OnEditNodeSelected()
    {
        trajectoryTool = TrajectoryToolType.EDIT_NODE;
    }

    void OnEditSideSelected()
    {
        trajectoryTool = TrajectoryToolType.EDIT_SIDE;
    }

    void OnInitialNodeSelected()
    {
        trajectoryTool = TrajectoryToolType.INIT_NODE;
    }

    void OnDeleteNodeSelected()
    {
        trajectoryTool = TrajectoryToolType.DELETE_NODE;
    }

    void AddNode(Vector2 pos)
    {
        trajectory.addNode(UnityEngine.Random.Range(0, 10000).ToString(), (int)pos.x, (int)pos.y, sceneRef.getPlayerScale());
    }

    void DeleteNode(int i)
    {
        trajectory.removeNode(trajectory.getNodes()[i]);
    }

    void SetInitNode(int i)
    {
        trajectory.setInitial(trajectory.getNodes()[i].getID());
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
    //    // wprowadzenie wielu graczy
    //    playerRect = new Rect(currentPos.x - 0.5f* playerTex.width, currentPos.y - 0.5f * playerTex.height, playerTex.width, playerRect.height);
    //}
}
