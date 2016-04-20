﻿using UnityEngine;
using System.Collections;

public class CutscenesWindowAppearance : LayoutWindow, DialogReceiverInterface
{
    private enum AssetType
    {
        SELECT_SLIDES,
        MUSIC,
        VIDEOSCENE
    };

    private Texture2D clearImg = null;
    private Texture2D slidesPreview = null;
    private Texture2D slidePreviewMovie = null;
    private static float windowWidth, windowHeight;
    private static Rect previewRect;

    private string slidesPath = "";
    private string musicPath = "";
    private string videoscenePath = "";

    private bool canSkipVideo, canSkipVideoLast;

    public CutscenesWindowAppearance(Rect aStartPos, GUIContent aContent, GUIStyle aStyle,
        params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        clearImg = (Texture2D) Resources.Load("EAdventureData/img/icons/deleteContent", typeof (Texture2D));

        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        if (GameRources.GetInstance().selectedCutsceneIndex >= 0)
        {
            slidesPath =
                Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                    GameRources.GetInstance().selectedCutsceneIndex].getPreviewImage();
            canSkipVideo =
                canSkipVideoLast =
                    Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
                        GameRources.GetInstance().selectedCutsceneIndex].getCanSkip();
            // Get videopath
        }

        //musicPath =
        //    Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
        //        GameRources.GetInstance().selectedCutsceneIndex].music();

        if (slidesPath != null && !slidesPath.Equals(""))
            slidesPreview =
                (Texture2D) Resources.Load(slidesPath.Substring(0, slidesPath.LastIndexOf(".")), typeof (Texture2D));


        slidePreviewMovie = (Texture2D) Resources.Load("EAdventureData/img/icons/video", typeof (Texture2D));
        previewRect = new Rect(0f, 0.5f*windowHeight, windowWidth, windowHeight*0.45f);
    }


    public override void Draw(int aID)
    {
        /*
        * View for videoscene
        */
        if (Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
            GameRources.GetInstance().selectedCutsceneIndex].isVideoscene())
        {
            // Background chooser
            GUILayout.Label("Video of videoscene");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(clearImg, GUILayout.Width(0.1f*windowWidth)))
            {
                OnVideosceneChanged("");
            }
            GUILayout.Box(videoscenePath, GUILayout.Width(0.6f*windowWidth));
            if (GUILayout.Button("Select", GUILayout.Width(0.1f*windowWidth)))
            {
                ShowAssetChooser(AssetType.VIDEOSCENE);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(30);

            GUILayout.Label("Video skipping");
            canSkipVideo = GUILayout.Toggle(canSkipVideo, new GUIContent("Users can skip the video with a mouse click"));
            if (canSkipVideo != canSkipVideoLast)
                OnVideosceneCanSkipVideoChanged(canSkipVideo);
        }
        /*
        * View for slidescene
        */
        else
        {
            // Background chooser
            GUILayout.Label("Set of slides of the slidescene");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(clearImg, GUILayout.Width(0.1f*windowWidth)))
            {
                OnSlidesceneChanged("");
            }
            GUILayout.Box(slidesPath, GUILayout.Width(0.6f*windowWidth));
            if (GUILayout.Button("Select", GUILayout.Width(0.1f*windowWidth)))
            {
                ShowAssetChooser(AssetType.SELECT_SLIDES);
            }
            // Create/edit slidescene
            if (GUILayout.Button("Create/Edit", GUILayout.Width(0.2f*windowWidth)))
            {
                // For not-existing cutscene - show new cutscene name dialog
                if (slidesPath == null || slidesPath.Equals(""))
                {
                    CutsceneNameInputPopup createCutsceneDialog =
                        (CutsceneNameInputPopup) ScriptableObject.CreateInstance(typeof (CutsceneNameInputPopup));
                    createCutsceneDialog.Init(this, "");
                }
                else
                {
                    EditCutscene();
                }
            }
            GUILayout.EndHorizontal();

            // Music chooser
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(clearImg, GUILayout.Width(0.1f*windowWidth)))
            {
                musicPath = "";
            }
            GUILayout.Box(musicPath, GUILayout.Width(0.7f*windowWidth));
            if (GUILayout.Button("Select", GUILayout.Width(0.19f*windowWidth)))
            {
                ShowAssetChooser(AssetType.MUSIC);
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(30);

        GUILayout.Label("Preview");
        if (Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
            GameRources.GetInstance().selectedCutsceneIndex].isVideoscene())
        {
            GUI.DrawTexture(previewRect, slidePreviewMovie, ScaleMode.ScaleToFit);
        }
        else
        {
            if (slidesPath != "")
            {
                GUI.DrawTexture(previewRect, slidesPreview, ScaleMode.ScaleToFit);
            }
        }
    }

    void ShowAssetChooser(AssetType type)
    {
        switch (type)
        {
            case AssetType.SELECT_SLIDES:
                AnimationFileOpenDialog animationDialog =
                    (AnimationFileOpenDialog) ScriptableObject.CreateInstance(typeof (AnimationFileOpenDialog));
                animationDialog.Init(this, BaseFileOpenDialog.FileType.CUTSCENE_SLIDES);
                break;
            case AssetType.MUSIC:
                MusicFileOpenDialog musicDialog =
                    (MusicFileOpenDialog) ScriptableObject.CreateInstance(typeof (MusicFileOpenDialog));
                musicDialog.Init(this, BaseFileOpenDialog.FileType.CUTSCENE_MUSIC);
                break;
            case AssetType.VIDEOSCENE:
                VideoFileOpenDialog videoDialog =
                    (VideoFileOpenDialog) ScriptableObject.CreateInstance(typeof (VideoFileOpenDialog));
                videoDialog.Init(this, BaseFileOpenDialog.FileType.CUTSCENE_VIDEO);
                break;
        }
    }

    public void OnDialogOk(string message, object workingObject = null)
    {

        // After new cutscene name was choosed
        if (workingObject is CutsceneNameInputPopup)
        {
            //TODO: create file
            OnSlidesceneCreated(message);
            EditCutscene();
            return;
        }

        if (workingObject is BaseFileOpenDialog.FileType)
        {
            switch ((BaseFileOpenDialog.FileType) workingObject)
            {
                case BaseFileOpenDialog.FileType.CUTSCENE_SLIDES:
                    OnSlidesceneChanged(message);
                    break;
                case BaseFileOpenDialog.FileType.CUTSCENE_VIDEO:
                    OnVideosceneChanged(message);
                    break;
                case BaseFileOpenDialog.FileType.CUTSCENE_MUSIC:
                    OnSlidesceneMusicChanged(message);
                    break;
                default:
                    break;
            }
        }

    }

    public void OnDialogCanceled(object workingObject = null)
    {
        Debug.Log("Wiadomość nie OK");
    }


    void OnSlidesceneChanged(string val)
    {
        slidesPath = val;
    }

    void OnVideosceneChanged(string val)
    {
        videoscenePath = val;
    }

    void OnSlidesceneMusicChanged(string val)
    {
        musicPath = val;
    }

    void OnSlidesceneCreated(string val)
    {

        OnSlidesceneChanged(val);
    }

    void OnVideosceneCanSkipVideoChanged(bool val)
    {
        canSkipVideoLast = val;
        Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
            GameRources.GetInstance().selectedCutsceneIndex].setCanSkip(val);
    }

    void EditCutscene()
    {
        CutsceneSlidesEditor slidesEditor =
            (CutsceneSlidesEditor) ScriptableObject.CreateInstance(typeof (CutsceneSlidesEditor));
        // TODO: CHANGE
        slidesEditor.Init(this, Controller.getInstance().getSelectedChapterDataControl().getCutscenesList().getCutscenes()[
            GameRources.GetInstance().selectedCutsceneIndex].getPathToSlides());
    }
}