using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour
{
    public Text tooltip;

    public void TakeAShot()
    {
        StartCoroutine(nameof(CaptureIt));
        tooltip.text = "Screenshot wurde erstellt.";
    }

    private IEnumerator CaptureIt()
    {
        var timeStamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        var fileName = "Screenshot" + timeStamp + ".png";
        var pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();

        //Instantiate (blink, new Vector2(0f, 0f), Quaternion.identity); --> Blitz s.o.
    }
}