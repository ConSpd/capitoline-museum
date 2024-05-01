using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScribbleButton : MonoBehaviour
{
    private Button button; 

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenScrible);
    }

    private void OpenScrible() {
        string path = Directory.GetCurrentDirectory();
        string finalPath;
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            finalPath = path + "\\Assets\\Scripts\\ScribblePages\\Scribble.htm";
        else
            finalPath = path + "/Assets/Scripts/ScribblePages/Scribble.htm";
        System.Diagnostics.Process.Start(finalPath);
    }
}
