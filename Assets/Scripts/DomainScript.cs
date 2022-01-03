using RoslynCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainScript : MonoBehaviour
{
    private ScriptDomain domain = null;

    void Start()
    {
        domain = ScriptDomain.CreateDomain("MyDomain");
    }
}
