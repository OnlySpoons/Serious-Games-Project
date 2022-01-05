using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoslynCSharp;
using System.IO;
using System;

public class CompilerScript : MonoBehaviour
{
    [HideInInspector]
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
        if(type is null)
            return false;

        ScriptProxy proxy = type.CreateInstance(gameObject);

        var stringReader = new StringReader(GameManagerScript.CurrentObjective.Input);
        Console.SetIn(stringReader);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        proxy.Call("Main", new string[1]);

        var output = stringWriter.ToString().Trim('\r','\n');
        consoleManager.WriteToOutput(output);

        return GameManagerScript.CurrentObjective.ExpectedOutput == output;
    }
}
