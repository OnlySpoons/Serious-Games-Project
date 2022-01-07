using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManagerScript : MonoBehaviour
{
    public delegate void OnObjectiveChanged ( ObjectiveData data );
    public static event OnObjectiveChanged onObjectiveChanged;

    public delegate void OnVictoryAchieved ();
    public static event OnVictoryAchieved onVictoryAchieved;

    [SerializeField]
    private TextMeshProUGUI objectiveNumText;

    [SerializeField]
    private List<ObjectiveData> objectives;

    private static ObjectiveData currentObjective;
    public static ObjectiveData CurrentObjective => currentObjective;

    private int objectiveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentObjective = objectives[ 0 ];

        objectiveNumText.text = $"Question {objectiveIndex + 1} of {objectives.Count}";

        // Calls onObjectiveChanged event
        if ( onObjectiveChanged != null )
            onObjectiveChanged( currentObjective );
    }

    public void LoadNextObjective ()
    {
        // Load next obj
        if ( ++objectiveIndex >= objectives.Count )
        {
            if( onVictoryAchieved != null )
                onVictoryAchieved();

            return;
        }

        objectiveNumText.text = $"Question {objectiveIndex + 1} of {objectives.Count}";

        currentObjective = objectives[ objectiveIndex ];

        // Calls onObjectiveChanged event
        if ( onObjectiveChanged != null )
            onObjectiveChanged( currentObjective );
    }
}
