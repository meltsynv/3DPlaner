using System.Collections;
using UnityEngine;


public class Screenshot : MonoBehaviour
{
    
    // GameObject blink; --> Für Blitz geht noch nicht
    
    public void TakeAShot()
    {
        StartCoroutine (nameof(CaptureIt));
    }

    private IEnumerator CaptureIt()
    {
        
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
        
        //Instantiate (blink, new Vector2(0f, 0f), Quaternion.identity); --> Blitz s.o.
        
    }
}
