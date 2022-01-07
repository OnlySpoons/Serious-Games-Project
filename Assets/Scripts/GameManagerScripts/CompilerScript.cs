using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoslynCSharp;
using System.IO;
using System;
using System.Linq;
using System.Reflection;

public class CompilerScript : MonoBehaviour
{
    [HideInInspector]
    public string output;

    private ScriptDomain domain = null;
    private ConsoleManagerScript consoleManager;

    void Start()
    {
        consoleManager = GetComponent<ConsoleManagerScript>();
        domain = ScriptDomain.CreateDomain("MyDomain", true);
    }

    public ScriptType Compile(string code)
    {
        ScriptType type = null;
        output = string.Empty;

        try
        {
            type = domain.CompileAndLoadMainSource(code);
        }
        catch (Exception e)
        {
            consoleManager.WriteToOutput(e.ToString());
        }

        foreach ( var error in domain.CompileResult.Errors )
		{
            consoleManager.WriteToOutput( $"Error { error.Code } on line { error.SourceLine + 1 } - { error.Message }" );
		}

        return type;
    }

    public bool Run(ScriptType type)
    {
        if(type is null)
            return false;

        ScriptProxy proxy = type.CreateInstance(gameObject);

        var stringReader = new StringReader( ObjectiveManagerScript.CurrentObjective.Input );
        Console.SetIn(stringReader);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        try
        {
            proxy.Call("Main", new string[1]);
        }
        catch(TargetParameterCountException e)
        {
            consoleManager.WriteToOutput($"Error: No definition for Main(string[]).");
            return false;
        }
        catch(Exception e)
        {
            consoleManager.WriteToOutput($"Exception at: {e.Message}");
            return false;
        }

        output = stringWriter.ToString().Trim('\r','\n');
        consoleManager.WriteToOutput( output );

        return true;
    }
}
