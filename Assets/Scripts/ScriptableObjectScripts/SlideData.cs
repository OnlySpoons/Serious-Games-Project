using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SlideData", menuName = "ScriptableObject/SlideData")]
public class SlideData : ScriptableObject
{
    [SerializeField]
    private string slideTitle;

    [SerializeField, TextArea(10, 100)]
    private string slideContents;

    public string Title => slideTitle;
    public string Contents => slideContents;
}
