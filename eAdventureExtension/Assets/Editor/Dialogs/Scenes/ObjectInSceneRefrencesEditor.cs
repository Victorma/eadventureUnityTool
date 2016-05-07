using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ObjectInSceneRefrencesEditor : BaseAreaEditablePopup
{
    private SceneDataControl sceneRef;
    private Texture2D backgroundPreviewTex = null;
    private Texture2D selectedObjectTex = null;
    private List<Sprite> objectsTex = null;

    private Rect imageBackgroundRect;
    private Vector2 scrollPosition;

    private string xString, yString, scaleString;
    private string xStringLast, yStringLast, scaleStringLast;

    private int calledItemIndexRef;

    private Rect currentRect;
    private bool dragging;
    private Vector2 startPos;
    private Vector2 currentPos;

    public void Init(DialogReceiverInterface e, SceneDataControl scene, int areaIndex)
    {
        sceneRef = scene;
        calledItemIndexRef = areaIndex;

        string backgroundPath =
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getPreviewBackground();

        backgroundPreviewTex =
            (Texture2D) Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof (Texture2D));

        selectedObjectTex = (Texture2D) Resources.Load("Editor/SelectedArea", typeof (Texture2D));

        imageBackgroundRect = new Rect(0f, 0f, backgroundPreviewTex.width, backgroundPreviewTex.height);

        xString = xStringLast = Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
            GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[
                calledItemIndexRef]
            .getErdc().getElementX().ToString();
        yString = yStringLast = Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
            GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[
                calledItemIndexRef]
            .getErdc().getElementY().ToString();
        scaleString =
            scaleStringLast = Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[
                    calledItemIndexRef]
                .getErdc().getElementScale().ToString();

        objectsTex = new List<Sprite>();
        foreach (
            ElementContainer element in
                Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl())
        {
            objectsTex.Add(element.getImage());
        }

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
            sceneRef.getReferencesList().getAllReferencesDataControl().Count;
            i++)
        {
            Rect aRect = new Rect(sceneRef.getReferencesList().getAllReferencesDataControl()[i].getErdc().getElementX(),
                sceneRef.getReferencesList().getAllReferencesDataControl()[i].getErdc().getElementX(),
                sceneRef.getReferencesList().getAllReferencesDataControl()[i].getErdc().getElementScale()*
                backgroundPreviewTex.width,
                sceneRef.getReferencesList().getAllReferencesDataControl()[i].getErdc().getElementScale()*
                backgroundPreviewTex.height);
            if(objectsTex[i] != null)
                GUI.DrawTexture(aRect, objectsTex[i].texture);

            // Frame around current area
            if (calledItemIndexRef == i)
            {
                currentRect = aRect;
                GUI.DrawTexture(aRect, selectedObjectTex);
            }
        }
        GUILayout.EndScrollView();
        /*
          *HANDLE EVENTS
          */
        if (dragging)
        {
            OnBeingDragged();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Box("X", GUILayout.Width(0.33f*backgroundPreviewTex.width));
        GUILayout.Box("Y", GUILayout.Width(0.33f*backgroundPreviewTex.width));
        GUILayout.Box("Scale", GUILayout.Width(0.3f*backgroundPreviewTex.width));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        xString = GUILayout.TextField(xString, GUILayout.Width(0.33f*backgroundPreviewTex.width));
        xString = (Regex.Match(xString, "^[0-9]{1,4}$").Success ? xString : xStringLast);
        if (!xString.Equals(xStringLast))
            OnChangeX(xString);

        yString = GUILayout.TextField(yString, GUILayout.Width(0.33f*backgroundPreviewTex.width));
        yString = (Regex.Match(yString, "^[0-9]{1,4}$").Success ? yString : yStringLast);
        if (!yString.Equals(yStringLast))
            OnChangeY(yString);

        scaleString = GUILayout.TextField(scaleString, GUILayout.Width(0.33f*backgroundPreviewTex.width));
        scaleString = (Regex.Match(scaleString, "^(\\d+[\\.]\\d*$)").Success ? scaleString : scaleStringLast);
        if (!scaleString.Equals(scaleStringLast) && !scaleString.EndsWith("."))
            OnChangeScale(scaleString);

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
        sceneRef.getReferencesList().getAllReferencesDataControl()[calledItemIndexRef].getErdc()
            .setElementPosition(int.Parse(xString), int.Parse(yString));
    }

    void OnChangeY(string val)
    {
        yStringLast = val;
        sceneRef.getReferencesList().getAllReferencesDataControl()[calledItemIndexRef].getErdc()
            .setElementPosition(int.Parse(xString), int.Parse(yString));
    }

    void OnChangeScale(string val)
    {
        scaleStringLast = val;
        sceneRef.getReferencesList().getAllReferencesDataControl()[calledItemIndexRef].getErdc()
            .setElementScale(float.Parse(scaleString));
    }

    private void OnBeingDragged()
    {
        xStringLast = xString = ((int)currentPos.x - (int)(0.5f * currentRect.width)).ToString();
        yStringLast = yString = ((int)currentPos.y - (int)(0.5f * currentRect.height)).ToString();
        sceneRef.getReferencesList().getAllReferencesDataControl()[calledItemIndexRef].getErdc()
            .setElementPosition(int.Parse(xString), int.Parse(yString));
    }
}