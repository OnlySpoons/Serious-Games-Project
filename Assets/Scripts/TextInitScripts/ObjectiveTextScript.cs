using UnityEngine;

public class ObjectiveTextScript : MonoBehaviour
{    
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
        GetComponent<TMPro.TextMeshProUGUI>().text = data.Objective;
    }
}
