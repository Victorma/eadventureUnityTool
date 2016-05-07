using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class AdvencedFeaturesWindowListOfTimers : LayoutWindow
{
    private Texture2D addTex = null;
    private Texture2D duplicateTex = null;
    private Texture2D clearTex = null;

    private static float windowWidth, windowHeight;
    private static Rect timerTableRect, rightPanelRect, settingsTable;

    private static GUISkin defaultSkin;
    private static GUISkin noBackgroundSkin;
    private static GUISkin selectedAreaSkin;

    private int selectedTimer;

    private string timerTime, timerTimeLast;
    private string fullTimerDescription = "", fullTimerDescriptionLast = "";
    private string displayName = "", displayNameLast = "";

    private Vector2 scrollPosition;

    private GUIStyle smallFontStyle;

    public AdvencedFeaturesWindowListOfTimers(Rect aStartPos, GUIContent aContent, GUIStyle aStyle,
        params GUILayoutOption[] aOptions)
        : base(aStartPos, aContent, aStyle, aOptions)
    {
        clearTex = (Texture2D) Resources.Load("EAdventureData/img/icons/deleteContent", typeof (Texture2D));
        addTex = (Texture2D) Resources.Load("EAdventureData/img/icons/addNode", typeof (Texture2D));
        duplicateTex = (Texture2D) Resources.Load("EAdventureData/img/icons/duplicateNode", typeof (Texture2D));

        windowWidth = aStartPos.width;
        windowHeight = aStartPos.height;

        noBackgroundSkin = (GUISkin) Resources.Load("Editor/EditorNoBackgroundSkin", typeof (GUISkin));
        selectedAreaSkin = (GUISkin) Resources.Load("Editor/EditorLeftMenuItemSkinConcreteOptions", typeof (GUISkin));

        timerTableRect = new Rect(0f, 0.1f*windowHeight, 0.9f*windowWidth, 0.2f*windowHeight);
        rightPanelRect = new Rect(0.9f*windowWidth, 0.1f*windowHeight, 0.08f*windowWidth, 0.2f*windowHeight);
        settingsTable = new Rect(0f, 0.3f*windowHeight, windowWidth, windowHeight*0.65f);

        smallFontStyle = new GUIStyle();
        smallFontStyle.fontSize = 8;

        selectedTimer = -1;
    }

    public override void Draw(int aID)
    {
        /*
        * Timer table
        */
        GUILayout.BeginArea(timerTableRect);

        GUILayout.BeginHorizontal();
        GUILayout.Box("Timer", GUILayout.MaxWidth(windowWidth*0.3f));
        GUILayout.Box("Time", GUILayout.MaxWidth(windowWidth*0.3f));
        GUILayout.Box("Display in game", GUILayout.MaxWidth(windowWidth*0.3f));
        GUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0;
            i < Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers().Count;
            i++)
        {
            if (i == selectedTimer)
                GUI.skin = selectedAreaSkin;
            else
                GUI.skin = noBackgroundSkin;

            GUILayout.BeginHorizontal();

            if (i == selectedTimer)
            {
                if (GUILayout.Button("Timer #" + i, GUILayout.MaxWidth(windowWidth*0.3f)))
                {
                    OnTimerSelectedChange(i);
                }

                timerTime = GUILayout.TextField(timerTime, GUILayout.MaxWidth(windowWidth * 0.3f));
                timerTime = (Regex.Match(timerTime, "^[0-9]{1,4}$").Success ? timerTime : timerTimeLast);
                if (timerTime != timerTimeLast)
                    OnTimerTime(timerTime);
                
                Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[i].setShowTime(GUILayout
                    .Toggle(
                        Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[i]
                            .isShowTime(),
                        "", GUILayout.MaxWidth(windowWidth*0.3f)));
            }
            else
            {
                if (GUILayout.Button("Timer #" + i, GUILayout.MaxWidth(windowWidth*0.3f)))
                {
                    OnTimerSelectedChange(i);
                }
                if (
                    GUILayout.Button(
                        Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[i].getTime()
                            .ToString(), GUILayout.MaxWidth(windowWidth*0.3f)))
                {
                    OnTimerSelectedChange(i);
                }
                if (
                    GUILayout.Button(
                    Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[i].isShowTime().ToString(), GUILayout.MaxWidth(windowWidth*0.3f)))
                {
                    OnTimerSelectedChange(i);
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
        if (GUILayout.Button(addTex, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Controller.getInstance().getSelectedChapterDataControl().getTimersList().addElement(Controller.TIMER, "");
        }
        if (GUILayout.Button(duplicateTex, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList()
                .duplicateElement(
                    Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[selectedTimer]);
        }
        if (GUILayout.Button(clearTex, GUILayout.MaxWidth(0.08f*windowWidth)))
        {
            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList()
                .deleteElement(
                    Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[selectedTimer],
                    false);
        }
        GUI.skin = defaultSkin;
        GUILayout.EndArea();

        /*
        *Properties panel
        */

        if (selectedTimer != -1 &&
            Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[selectedTimer] != null)
        {
            GUILayout.BeginArea(settingsTable);

            GUILayout.Label("Documentation of timer");
            fullTimerDescription = GUILayout.TextArea(fullTimerDescription, GUILayout.MinHeight(0.1f*windowHeight));
            if (fullTimerDescription != fullTimerDescriptionLast)
                OnTimerDocumentationChanged(fullTimerDescription);


            GUILayout.FlexibleSpace();


            GUILayout.Label("Time");

            GUILayout.BeginHorizontal();
            if (
                !Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[selectedTimer]
                    .isShowTime())
                GUI.enabled = false;
            GUILayout.Label("Display name");
            displayName = GUILayout.TextField(displayName);
            if (displayName != displayNameLast)
                OnTimerDisplayNameChanged(displayName);

            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList().getTimers()[selectedTimer].setCountDown(GUILayout.Toggle(Controller.getInstance()
                    .getSelectedChapterDataControl()
                    .getTimersList().getTimers()[selectedTimer].isCountDown(), "Count-down"));

            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList().getTimers()[selectedTimer].setShowWhenStopped(GUILayout.Toggle(Controller.getInstance()
                    .getSelectedChapterDataControl()
                    .getTimersList().getTimers()[selectedTimer].isShowWhenStopped(), "Show when stoped"));
            GUI.enabled = true;
            GUILayout.EndHorizontal();


            GUILayout.FlexibleSpace();


            GUILayout.Label("Loop control");
            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList().getTimers()[selectedTimer].setMultipleStarts(GUILayout.Toggle(Controller.getInstance()
                    .getSelectedChapterDataControl()
                    .getTimersList().getTimers()[selectedTimer].isMultipleStarts(), "Multiple starts"));
            GUILayout.Label(
                "Set -> If the timer is stopped, it will start again once the conditions to start are met\nNot set -> Once the timer stops it will not start again even if the conditions to start are met", smallFontStyle);
            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList().getTimers()[selectedTimer].setRunsInLoop(GUILayout.Toggle(Controller.getInstance()
                    .getSelectedChapterDataControl()
                    .getTimersList().getTimers()[selectedTimer].isRunsInLoop(), "Runs in loops"));
            GUILayout.Label(
                "Set -> The timer will run once and again until it is stopped\nNot set->The timer will run once and stop. ", smallFontStyle);


            GUILayout.FlexibleSpace();


            GUILayout.Label("Conditions to start the timer");
            if (GUILayout.Button("Edit init condition"))
            {

            }


            GUILayout.FlexibleSpace();


            GUILayout.Label("Conditions to stop the timer");
            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList().getTimers()[selectedTimer].setUsesEndCondition(
                    GUILayout.Toggle(Controller.getInstance()
                        .getSelectedChapterDataControl()
                        .getTimersList().getTimers()[selectedTimer].isUsesEndCondition(),
                        "Uses end conditions (if not, the timer will stop when start condition's aren't met)"));
            if (
                !Controller.getInstance().getSelectedChapterDataControl().getTimersList().getTimers()[selectedTimer]
                    .isUsesEndCondition())
                GUI.enabled = false;
            if (GUILayout.Button("Edit end condition"))
            {

            }
            GUI.enabled = true;


            GUILayout.FlexibleSpace();


            GUILayout.BeginHorizontal();
            GUILayout.Label("Effects to be executed if the timer expires", GUILayout.Width(0.45f*windowWidth));
            GUILayout.Label("Effects to be executed if the timer is stopped", GUILayout.Width(0.45f*windowWidth));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Edit effects", GUILayout.Width(0.45f*windowWidth)))
            {
            }
            if (GUILayout.Button("Edit post effects", GUILayout.Width(0.45f*windowWidth)))
            {
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

    }

    void OnTimerSelectedChange(int i)
    {
        selectedTimer = i;

        fullTimerDescription = fullTimerDescriptionLast =
            Controller.getInstance()
                .getSelectedChapterDataControl()
                .getTimersList().getTimers()[selectedTimer].getDocumentation();
        if (fullTimerDescription == null)
            fullTimerDescription = fullTimerDescriptionLast = "";


        displayName = displayNameLast = Controller.getInstance()
            .getSelectedChapterDataControl()
            .getTimersList().getTimers()[selectedTimer].getDisplayName();
        if (displayName == null)
            displayName = displayNameLast = "";


        timerTime = timerTimeLast = Controller.getInstance()
            .getSelectedChapterDataControl()
            .getTimersList().getTimers()[selectedTimer].getTime().ToString();
        if (timerTime == null)
            timerTime = timerTime = "0";
    }

    void OnTimerDocumentationChanged(string val)
    {
        fullTimerDescriptionLast = val;
        Controller.getInstance()
            .getSelectedChapterDataControl()
            .getTimersList().getTimers()[selectedTimer].setDocumentation(val);
    }

    void OnTimerDisplayNameChanged(string val)
    {
        displayNameLast = val;
        Controller.getInstance()
            .getSelectedChapterDataControl()
            .getTimersList().getTimers()[selectedTimer].setDisplayName(val);
    }

    void OnTimerTime(string val)
    {
        timerTimeLast = val;
        Controller.getInstance()
            .getSelectedChapterDataControl()
            .getTimersList().getTimers()[selectedTimer].setTime(long.Parse(val));
    }
}