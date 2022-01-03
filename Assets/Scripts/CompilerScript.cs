using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoslynCSharp;
using System.IO;
using System;
using UnityEngine.Assertions;

public class CompilerScript : MonoBehaviour
{
    public string output;
    private ScriptDomain domain = null;

    private ConsoleManagerScript consoleManager;

    public void Init()
    {
        consoleManager = GetComponent<ConsoleManagerScript>();
        domain = ScriptDomain.CreateDomain("MyDomain", true);
    }

    public ScriptType Compile(string code)
    {
        ScriptType type = null;

        try
        {
            type = domain.CompileAndLoadMainSource(code);
        }
        catch (Exception e)
        {
            output = e.ToString();
        }

        return type;
    }

    public bool Run(ScriptType type)
    {
        ScriptProxy proxy = type.CreateInstance(gameObject);

        var stringReader = new StringReader(GameManagerScript.currentObjective.input);
        Console.SetIn(stringReader);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        proxy.Call("Main");

        consoleManager.WriteToOutput(stringWriter.ToString());

        return GameManagerScript.currentObjective.expectedOutput == stringWriter.ToString();
    }
}
