using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveData", menuName = "ScriptableObject/ObjectiveData", order = 1)]
public class ObjectiveData : ScriptableObject
{
    public string objective, input, expectedOutput;
    public string[] hints = new string[3];
}
