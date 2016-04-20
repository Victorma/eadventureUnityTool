using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using Screen = UnityEngine.Screen;

public abstract class BaseFileOpenDialog : EditorWindow
{
    public enum FileType
    {
        SCENE_BACKGROUND,
        SCENE_FOREGROUND,
        SCENE_MUSIC, 
        EXIT_MUSIC,
        EXIT_ICON, 
        CUTSCENE_MUSIC,
        CUTSCENE_VIDEO, 
        CUTSCENE_SLIDES, 
        FRAME_IMAGE,
        FRAME_MUSIC
    };

    protected const string DIR_PREFIX = "Assets/Resources";

    protected DialogReceiverInterface reference;
    protected FileType fileType;

    private System.Windows.Forms.OpenFileDialog ofd;
    protected string selectedAssetPath = "";

    protected string fileFilter;

    // Return string (for engine purpose)
    protected string returnPath;

    public virtual void Init(DialogReceiverInterface e, FileType fType)
    {
        reference = e;
        fileType = fType;
        ofd = new System.Windows.Forms.OpenFileDialog();

        OpenFileDialog();
    }

    public void OpenFileDialog()
    {
        Stream myStream = null;
        ofd.Filter = fileFilter;
        ofd.FilterIndex = 2;
        ofd.RestoreDirectory = true;

        if (ofd.ShowDialog() == DialogResult.OK)
        {

            if ((myStream = ofd.OpenFile()) != null)
            {
                using (myStream)
                {
                    // Insert code to read the stream here.
                    selectedAssetPath = ofd.FileName;
                    ChoosedCorrectFile();
                }
                myStream.Dispose();
            }
        }
        else
        {
            FileSelectionNotPerfromed();
        }
    }

    protected void CopySelectedAssset()
    {
        string assetTypeDir;

        switch (fileType)
        {
            case FileType.SCENE_BACKGROUND:
                assetTypeDir = AssetsController.CATEGORY_BACKGROUND_FOLDER;
                break;
            case FileType.SCENE_FOREGROUND:
                assetTypeDir = AssetsController.CATEGORY_BACKGROUND_FOLDER;
                break;
            case FileType.SCENE_MUSIC:
            case FileType.EXIT_MUSIC:
            case FileType.CUTSCENE_MUSIC:
            case FileType.FRAME_MUSIC:
                assetTypeDir = AssetsController.CATEGORY_AUDIO_PATH;
                break;
            case FileType.EXIT_ICON:
                assetTypeDir = AssetsController.CATEGORY_CURSOR_PATH;
                break;
            case FileType.CUTSCENE_VIDEO:
                assetTypeDir = AssetsController.CATEGORY_VIDEO_PATH;
                break;
            case FileType.CUTSCENE_SLIDES:
                //TODO: copy all assets files (slides, music)
                assetTypeDir = AssetsController.CATEGORY_ANIMATION_FOLDER;
                break;
            case FileType.FRAME_IMAGE:
                assetTypeDir = AssetsController.CATEGORY_ANIMATION_FOLDER;
                break;
            default:
                assetTypeDir = "";
                break;
        }


        DirectoryInfo path = new DirectoryInfo(DIR_PREFIX + "/" + assetTypeDir);
        if (!Directory.Exists(path.FullName))
            Directory.CreateDirectory(path.FullName);

        string nameOnly = Path.GetFileName(selectedAssetPath);

        File.Copy(selectedAssetPath, Path.Combine(path.FullName, nameOnly),true);

        returnPath = assetTypeDir + "/" + nameOnly;
    }

    protected abstract void ChoosedCorrectFile();

    protected abstract void FileSelectionNotPerfromed();
}
