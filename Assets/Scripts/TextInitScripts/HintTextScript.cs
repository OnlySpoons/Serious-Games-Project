using System.Collections.Generic;
using UnityEngine;

public class HintTextScript : MonoBehaviour
{
    public List<TMPro.TextMeshProUGUI> hintList;

    private static int hintsUsed;
    public static int HintsUsed => hintsUsed;

    private void OnEnable()
    {
        // Subscribes to onObjectiveChanged event
        GameManagerScript.onObjectiveChanged += Init;
    }
    private void OnDisable()
    {
        // Unsubscribes from onObjectiveChanged event
        GameManagerScript.onObjectiveChanged -= Init;
    }

    void Init(ObjectiveData data)
    {
        hintsUsed = 0;

        for(int i = 0; i < hintList.Count; i++)
        {
            hintList[i].text = data.Hints[i];
            hintList[i].enabled = false;
        }
    }

    public void OnClick()
    {
        if (hintsUsed >= hintList.Count)
            return;

        hintList[hintsUsed++].enabled = true;
    }
}
