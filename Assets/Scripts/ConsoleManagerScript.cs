using UnityEngine;
using TMPro;

public class ConsoleManagerScript : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField outputConsole;
    [SerializeField]
    private TMP_InputField inputConsole;

    void OnEnable()
    {
        Application.logMessageReceived += LogMessageReceived;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessageReceived;
    }

    void LogMessageReceived(string log, string stackTrace, LogType type)
    {
        outputConsole.text += log + '\n';
    }

    public string ReadFromInput()
    {
        return inputConsole.text;
    }

    public void WriteToOutput(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        outputConsole.text += text+ '\n';
    }

    public void ClearOutput()
    {
        outputConsole.text = "";
    }

    public void ClearInput()
    {
        inputConsole.text = "";
    }
}
