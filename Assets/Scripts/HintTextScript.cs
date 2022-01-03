using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextScript : MonoBehaviour
{
    public List<TMPro.TextMeshProUGUI> hintList;

    public static int index;

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
        index = 0;

        for(int i = 0; i < hintList.Count; i++)
        {
            hintList[i].text = data.hints[i];
            hintList[i].enabled = false;
        }
    }

    public void OnClick()
    {
        if (index >= hintList.Count)
            return;

        hintList[index++].enabled = true;
    }
}
