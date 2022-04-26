using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    int index = 0;
    // Start is called before the first frame update
    public void TakeScreenshot()
    {
        Debug.Log(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
        //ScreenCapture.CaptureScreenshot(Application.dataPath + string.Format("/Screenshot_{0}.png", index));
        ScreenCapture.CaptureScreenshot(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + string.Format("/Screenshot_{0}.png", index));
        index++;
    }

}
