using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveData", menuName = "ScriptableObject/ObjectiveData", order = 1)]
public class ObjectiveData : ScriptableObject
{
    [SerializeField]
    private string objective, input, expectedOutput;
    [SerializeField]
    private string[] hints = new string[3];

    public string Objective => objective;
    public string Input => input;
    public string ExpectedOutput => expectedOutput;
    public string[] Hints => hints;
}
