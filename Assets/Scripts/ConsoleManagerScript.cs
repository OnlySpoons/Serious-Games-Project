using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleManagerScript : MonoBehaviour
{
    public TMP_InputField outputConsole;
    public TMP_InputField inputConsole;

    public string ReadFromInput()
    {
        return inputConsole.text;
    }

    public void WriteToOutput(string text)
    {
        Debug.Log(text);
        outputConsole.text = text;
    }
}
