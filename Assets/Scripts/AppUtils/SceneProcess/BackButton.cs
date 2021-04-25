using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class BackButton : MonoBehaviour
{
    private string sceneNameTitle = "Title";
    [SerializeField]
    GameObject m_BackButton;
    public GameObject backButton
    {
        get { return m_BackButton; }
        set { m_BackButton = value; }
    }

    void Start()
    {
        if (Application.CanStreamedLevelBeLoaded(sceneNameTitle))
        {
            m_BackButton.SetActive(true);
        }
    }

    public void BackButtonPressed()
    {
        SceneManager.LoadScene(sceneNameTitle, LoadSceneMode.Single);
        LoaderUtility.Deinitialize();
    }
}
