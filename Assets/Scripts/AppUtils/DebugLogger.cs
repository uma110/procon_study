using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugLogger : MonoBehaviour
{
    private TextMeshProUGUI logField;
    private void Awake()
    {
        logField = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.logMessageReceived += LogCallBackHandler;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= LogCallBackHandler;
    }

    private void LogCallBackHandler(string content,string stackTrace,LogType type)
    {
        if(logField.text.Length >= 3000)
        {
            logField.text = "";
        }
        logField.text += $"\n{content}";
    }
}
