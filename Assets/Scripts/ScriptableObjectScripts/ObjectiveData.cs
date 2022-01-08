using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "ObjectiveData", menuName = "ScriptableObject/ObjectiveData" )]
public class ObjectiveData : ScriptableObject
{
	[SerializeField]
	private string objective, input;
	[SerializeField, TextArea(10, 50)]
	private string expectedOutput;
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
