using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class ScenesWindowElementReference : LayoutWindow, DialogReceiverInterface
{

    private Texture2D backgroundPreviewTex = null;
    private Texture2D conditionTex = null;

    private Texture2D addTexture = null;
    private Texture2D moveUp, moveDown = null;
    private Texture2D clearImg = null;

    private string backgroundPath = "";

    private static float windowWidth, windowHeight;
    private static Rect tableRect;
    private static Rect previewRect;
    private Rect rightPanelRect;
    private static Rect infoPreviewRect;

    private static Vector2 scrollPosition;

    private static GUISkin selectedElementSkin;
    private static GUISkin defaultSkin;
    private static GUISkin noBackgroundSkin;

    private int selectedElement;
    private AddItemActionMenu addMenu;

    public ScenesWindowElementReference(Rect aStartPos, GUIContent aContent, GUIStyle aStyle,
        params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        clearImg = (Texture2D) Resources.Load("EAdventureData/img/icons/deleteContent", typeof (Texture2D));
        addTexture = (Texture2D) Resources.Load("EAdventureData/img/icons/addNode", typeof (Texture2D));
        moveUp = (Texture2D) Resources.Load("EAdventureData/img/icons/moveNodeUp", typeof (Texture2D));
        moveDown = (Texture2D) Resources.Load("EAdventureData/img/icons/moveNodeDown", typeof (Texture2D));

        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        if (GameRources.GetInstance().selectedSceneIndex >= 0)
            backgroundPath =
                Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getPreviewBackground();
        if (backgroundPath != null && !backgroundPath.Equals(""))
            backgroundPreviewTex =
                (Texture2D)
                    Resources.Load(backgroundPath.Substring(0, backgroundPath.LastIndexOf(".")), typeof (Texture2D));

        conditionTex = (Texture2D) Resources.Load("EAdventureData/img/icons/no-conditions-24x24", typeof (Texture2D));

        //TODO: do new skin?
        selectedElementSkin = (GUISkin) Resources.Load("Editor/EditorLeftMenuItemSkinConcreteOptions", typeof (GUISkin));
        noBackgroundSkin = (GUISkin) Resources.Load("Editor/EditorNoBackgroundSkin", typeof (GUISkin));

        tableRect = new Rect(0f, 0.1f*windowHeight, 0.9f*windowWidth, windowHeight*0.33f);
        rightPanelRect = new Rect(0.9f*windowWidth, 0.1f*windowHeight, 0.08f*windowWidth, 0.33f*windowHeight);
        infoPreviewRect = new Rect(0f, 0.45f*windowHeight, windowWidth, windowHeight*0.05f);
        previewRect = new Rect(0f, 0.5f*windowHeight, windowWidth, windowHeight*0.45f);

        selectedElement = -1;
        addMenu = new AddItemActionMenu();
    }

    public override void Draw(int aID)
    {
        GUILayout.BeginArea(tableRect);
        GUILayout.BeginHorizontal();
        GUILayout.Box("Layer", GUILayout.Width(windowWidth*0.12f));
        GUILayout.Box("", GUILayout.Width(windowWidth*0.06f));
        GUILayout.Box("Element references", GUILayout.Width(windowWidth*0.39f));
        GUILayout.Box(TC.get("Conditions.Title"), GUILayout.Width(windowWidth*0.29f));
        GUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0;
            i <
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl().Count;
            i++)
        {
            if (i == selectedElement)
                GUI.skin = selectedElementSkin;

            GUILayout.BeginHorizontal();

            if (
                GUILayout.Button(
                    Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                        GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                        .getAllReferencesDataControl()[i]
                        .getLayer().ToString(),
                    GUILayout.Width(windowWidth*0.12f)))
            {
                selectedElement = i;
            }

            if (Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                .getAllReferencesDataControl()[i].getErdc() != null)
            {
                // FOR ELEMENT ERDC
                Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[i]
                    .getErdc().setVisible(
                        GUILayout.Toggle(
                            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                                GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                                .getAllReferencesDataControl()[i].getErdc().isVisible(), "",
                            GUILayout.Width(windowWidth*0.06f)));
                if (
                    GUILayout.Button(
                        Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                            GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                            .getAllReferencesDataControl()[i]
                            .getErdc().getElementId(), GUILayout.Width(windowWidth*0.39f)))
                {
                    selectedElement = i;
                }

                if (GUILayout.Button(conditionTex, GUILayout.Width(windowWidth*0.29f)))
                {
                    selectedElement = i;
                    ConditionEditorWindow window =
                        (ConditionEditorWindow) ScriptableObject.CreateInstance(typeof (ConditionEditorWindow));
                    window.Init(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                        GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                        .getAllReferencesDataControl()[i].getErdc().getConditions());
                }
            }
            else
            {
                if (
                    GUILayout.Button("", GUILayout.Width(windowWidth*0.06f)))
                {
                    selectedElement = i;
                }
                if (
                    GUILayout.Button("", GUILayout.Width(windowWidth*0.39f)))
                {
                    selectedElement = i;
                }

                if (GUILayout.Button(conditionTex, GUILayout.Width(windowWidth*0.29f)))
                {
                    selectedElement = i;
                }
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
        if (GUILayout.Button(addTexture, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            addMenu.menu.ShowAsContext();
        }
        if (GUILayout.Button(moveUp, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Up");
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                .moveElementUp(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[
                        selectedElement].getErdc());
        }
        if (GUILayout.Button(moveDown, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Down");
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                .moveElementDown(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[
                        selectedElement].getErdc());
        }
        if (GUILayout.Button(clearImg, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Debug.Log("Clear");
            Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                .deleteElement(Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[
                        selectedElement].getErdc(), false);
        }
        GUI.skin = defaultSkin;
        GUILayout.EndArea();


        if (backgroundPath != "")
        {

            GUILayout.BeginArea(infoPreviewRect);
            // Show preview dialog
            // Button visible only is there is at least 1 object
            if (selectedElement != -1 && Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList().getAllReferencesDataControl()[
                        selectedElement].getErdc() != null)
            {
                if (GUILayout.Button("Show preview/edit window"))
                {
                    ObjectInSceneRefrencesEditor window =
                        (ObjectInSceneRefrencesEditor)
                            ScriptableObject.CreateInstance(typeof (ObjectInSceneRefrencesEditor));
                    window.Init(this,
                        Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                            GameRources.GetInstance().selectedSceneIndex], selectedElement);
                }
            }
            GUILayout.EndArea();
            GUI.DrawTexture(previewRect, backgroundPreviewTex, ScaleMode.ScaleToFit);

        }
        else
        {
            GUILayout.BeginArea(infoPreviewRect);
            GUILayout.Button("No background!");
            GUILayout.EndArea();
        }
    }

    public void OnDialogOk(string message, object workingObject = null, object workingObjectSecond = null)
    {
        Debug.Log("Apply");
    }

    public void OnDialogCanceled(object workingObject = null)
    {
        Debug.Log("Cancel");
    }

    #region Add ref action options

    class AddItemActionMenu : WindowMenuContainer
    {
        private AddItemAction itemAction;
        private AddSetItemAction setItemAction;
        private AddNPCAction npcAction;
      
        public AddItemActionMenu()
        {
            SetMenuItems();
        }

        protected override void Callback(object obj)
        {
            if ((obj as AddItemAction) != null)
                itemAction.OnCliked();
            else if ((obj as AddSetItemAction) != null)
                setItemAction.OnCliked();
            else if ((obj as AddNPCAction) != null)
                npcAction.OnCliked();
        }

        protected override void SetMenuItems()
        {
            menu = new GenericMenu();

            itemAction = new AddItemAction("Add item refrence");
            setItemAction = new AddSetItemAction("Add set item reference");
            npcAction = new AddNPCAction("Add npc reference");

            menu.AddItem(new GUIContent(itemAction.Label), false, Callback, itemAction);
            menu.AddItem(new GUIContent(setItemAction.Label), false, Callback, setItemAction);
            menu.AddItem(new GUIContent(npcAction.Label), false, Callback, npcAction);
        }
    }

    class AddItemAction : IMenuItem, DialogReceiverInterface
    {
        public AddItemAction(string name_)
        {
            this.Label = name_;
        }

        public string Label { get; set; }

        public void OnCliked()
        {
            ObjectAddItemReference window =
                (ObjectAddItemReference) ScriptableObject.CreateInstance(typeof (ObjectAddItemReference));
            window.Init(this);
        }

        public void OnDialogOk(string message, object workingObject = null, object workingObjectSecond = null)
        {
            if (workingObject is ObjectAddItemReference)
            {
                Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                    .addElement(Controller.ITEM_REFERENCE, message);
            }
        }

        public void OnDialogCanceled(object workingObject = null)
        {
            Debug.Log("Cancel");
        }
    }

    class AddSetItemAction : IMenuItem, DialogReceiverInterface
    {
        public AddSetItemAction(string name_)
        {
            this.Label = name_;
        }

        public string Label { get; set; }

        public void OnCliked()
        {

            ObjectAddSetItemReference window =
                (ObjectAddSetItemReference) ScriptableObject.CreateInstance(typeof (ObjectAddSetItemReference));
            window.Init(this);
        }

        public void OnDialogOk(string message, object workingObject = null, object workingObjectSecond = null)
        {
            if (workingObject is ObjectAddSetItemReference)
            {
                Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                    .addElement(Controller.ATREZZO_REFERENCE, message);
            }
        }

        public void OnDialogCanceled(object workingObject = null)
        {
            Debug.Log("Cancel");
        }
    }

    class AddNPCAction : IMenuItem, DialogReceiverInterface
    {
        public AddNPCAction(string name_)
        {
            this.Label = name_;
        }

        public string Label { get; set; }

        public void OnCliked()
        {
            ObjectAddNPCReference window =
                (ObjectAddNPCReference) ScriptableObject.CreateInstance(typeof (ObjectAddNPCReference));
            window.Init(this);
        }

        public void OnDialogOk(string message, object workingObject = null, object workingObjectSecond = null)
        {
            if (workingObject is ObjectAddNPCReference)
            {
                Controller.getInstance().getSelectedChapterDataControl().getScenesList().getScenes()[
                    GameRources.GetInstance().selectedSceneIndex].getReferencesList()
                    .addElement(Controller.NPC_REFERENCE, message);
            }
        }

        public void OnDialogCanceled(object workingObject = null)
        {
            Debug.Log("Cancel");
        }
    }

    #endregion
}