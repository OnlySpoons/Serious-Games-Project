using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "ObjectiveData", menuName = "ScriptableObject/ObjectiveData" )]
public class ObjectiveData : ScriptableObject
{

	[SerializeField, TextArea(10, 50)]
	private string objective, input, expectedOutput;
	[SerializeField]
	private string[] hints = new string[3];
	[SerializeField]
	private List<SlideData> slides;

	public string Objective => objective;
	public string Input => input;
	public string ExpectedOutput => expectedOutput;
	public string[] Hints => hints;
	public List<SlideData> Slides => slides;
}
