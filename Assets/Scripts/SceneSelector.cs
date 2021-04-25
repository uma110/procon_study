using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class SceneSelector : MonoBehaviour
{
    private string sceneNameTitle = "Title";
    private string sceneNameBasic = "ARbasic";
    private string sceneNameMap = "ARWorldMap";
    private string sceneNameObjectRecognition = "ObjectRecognition";

    public void LoadSceneToBasic()
    {
        LoaderUtility.Initialize();
        SceneManager.LoadScene(sceneNameBasic);
    }

    public void LoadSceneToMap()
    {
        LoaderUtility.Initialize();
        SceneManager.LoadScene(sceneNameMap);
    }

    public void LoadSceneToObjectRecognition()
    {
        LoaderUtility.Initialize();
        SceneManager.LoadScene(sceneNameObjectRecognition);
    }
}
