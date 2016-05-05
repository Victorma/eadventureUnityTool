using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class BarrierEditor : BaseAreaEditablePopup
{
    private SceneDataControl sceneRef;
    private Texture2D backgroundPreviewTex = null;
    private Texture2D barrierTex = null;
    private Texture2D selectedBarrierTex = null;

    private Rect imageBackgroundRect;
    private Vector2 scrollPosition;

    private string xString, yString, widthString, heightString;
    private string xStringLast, yStringLast, widthStringLast, heightStringLast;

    private int calledBarrierIndexRef;

    public void Init(DialogReceiverInterface e, SceneDataControl scene, int areaIndex)
    {
        sceneRef = scene;
        calledBarrierIndexRef = areaIndex;

        string backgroundPath =
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getPreviewBackground();

        backgroundPreviewTex = (Texture2D)Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof(Texture2D));

        barrierTex = (Texture2D)Resources.Load("Editor/BarrierArea", typeof(Texture2D));
        selectedBarrierTex = (Texture2D)Resources.Load("Editor/SelectedArea", typeof(Texture2D));

        imageBackgroundRect = new Rect(0f, 0f, backgroundPreviewTex.width, backgroundPreviewTex.height);

        xString = xStringLast = sceneRef.getBarriersList().getBarriersList()[areaIndex].getX().ToString();
        yString = yStringLast = sceneRef.getBarriersList().getBarriersList()[areaIndex].getY().ToString();
        widthString = widthStringLast = sceneRef.getBarriersList().getBarriersList()[areaIndex].getWidth().ToString();
        heightString = heightStringLast = sceneRef.getBarriersList().getBarriersList()[areaIndex].getHeight().ToString();

        base.Init(e, backgroundPreviewTex.width, backgroundPreviewTex.height);
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUI.DrawTexture(imageBackgroundRect, backgroundPreviewTex);

        for (int i = 0;
            i <
            sceneRef.getBarriersList().getBarriersList().Count;
            i++)
        {
            Rect aRect = new Rect(sceneRef.getBarriersList().getBarriersList()[i].getX(),
                sceneRef.getBarriersList().getBarriersList()[i].getY(),
                sceneRef.getBarriersList().getBarriersList()[i].getWidth(),
                sceneRef.getBarriersList().getBarriersList()[i].getHeight());
            GUI.DrawTexture(aRect, barrierTex);

            // Frame around current area
            if (calledBarrierIndexRef == i)
                GUI.DrawTexture(aRect, selectedBarrierTex);
        }
        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();
        GUILayout.Box("X", GUILayout.Width(0.25f * backgroundPreviewTex.width));
        GUILayout.Box("Y", GUILayout.Width(0.25f * backgroundPreviewTex.width));
        GUILayout.Box("Width", GUILayout.Width(0.25f * backgroundPreviewTex.width));
        GUILayout.Box("Height", GUILayout.Width(0.25f * backgroundPreviewTex.width));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        xString = GUILayout.TextField(xString, GUILayout.Width(0.25f * backgroundPreviewTex.width));
        xString = (Regex.Match(xString, "^[0-9]{1,4}$").Success ? xString : xStringLast);
        if (!xString.Equals(xStringLast))
            OnChangeX(xString);

        yString = GUILayout.TextField(yString, GUILayout.Width(0.25f * backgroundPreviewTex.width));
        yString = (Regex.Match(yString, "^[0-9]{1,4}$").Success ? yString : yStringLast);
        if (!yString.Equals(yStringLast))
            OnChangeY(yString);

        widthString = GUILayout.TextField(widthString, GUILayout.Width(0.25f * backgroundPreviewTex.width));
        widthString = (Regex.Match(widthString, "^[0-9]{1,4}$").Success ? widthString : widthStringLast);
        if (!widthString.Equals(widthStringLast))
            OnChangeWidth(widthString);

        heightString = GUILayout.TextField(heightString, GUILayout.Width(0.25f * backgroundPreviewTex.width));
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
        sceneRef.getBarriersList().getBarriersList()[calledBarrierIndexRef].setValues(int.Parse(xString), int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeY(string val)
    {
        yStringLast = val;
        sceneRef.getBarriersList().getBarriersList()[calledBarrierIndexRef].setValues(int.Parse(xString), int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeWidth(string val)
    {
        widthStringLast = val;
        sceneRef.getBarriersList().getBarriersList()[calledBarrierIndexRef].setValues(int.Parse(xString), int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }

    void OnChangeHeight(string val)
    {
        heightStringLast = val;
        sceneRef.getBarriersList().getBarriersList()[calledBarrierIndexRef].setValues(int.Parse(xString), int.Parse(yString), int.Parse(widthString), int.Parse(heightString));
    }
}
