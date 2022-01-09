using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private CompilerScript compiler;
    private ConsoleManagerScript consoleManager;
    private ScoreManagerScript scoreManager;

    [SerializeField]
    private Canvas gameWindow, pauseWindow, popupWindow, slidesWindow, victoryWindow;


    void Start()
    {
        AudioManager.m_instance.Play("GameMusic");

        compiler = GetComponent<CompilerScript>();
        consoleManager = GetComponent<ConsoleManagerScript>();
        scoreManager = GetComponent<ScoreManagerScript>();
        
        slidesWindow.enabled = true;
    }

	void OnEnable ()
	{
        ObjectiveManagerScript.onObjectiveChanged += OnObjectiveChanged;
        ObjectiveManagerScript.onVictoryAchieved += OnVictoryAchieved;
	}

    void OnDisable ()
    {
        ObjectiveManagerScript.onObjectiveChanged -= OnObjectiveChanged;
        ObjectiveManagerScript.onVictoryAchieved -= OnVictoryAchieved;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pauseWindow.enabled = !pauseWindow.enabled;
    }

    public void OnRun()
    {
        // Clear output window
        consoleManager.ClearOutput();

        // Compile code
        var type = compiler.Compile( consoleManager.ReadFromInput() );

        // Check for errors and display any
        if (type is null)
        {
            consoleManager.WriteToOutput( compiler.output );
            return;
        }

        // Run compiled code
        bool correct = compiler.Run(type);

        // If correct, add score
        if (!correct)
            return;

        if(ObjectiveManagerScript.CurrentObjective.ExpectedOutput != compiler.output)
        {
            consoleManager.WriteToOutput("Output does not match expected output!");
            return;
        }

        var scoreToAdd = 20 - (5 * HintTextScript.HintsUsed);

        gameWindow.GetComponent<CanvasGroup>().interactable = false;

        scoreManager.UpdateScore( scoreToAdd );

        popupWindow.enabled = true;
        popupWindow.GetComponent<PopupScript>().Init( scoreToAdd, HintTextScript.HintsUsed );
    }

    public void OnObjectiveChanged(ObjectiveData data)
    {
        popupWindow.enabled = false;
        gameWindow.GetComponent<CanvasGroup>().interactable = true;

        consoleManager.ClearInput();
        consoleManager.ClearOutput();

        slidesWindow.enabled = true;
    }

    public void OpenSlides ()
    {
        slidesWindow.enabled = true;
    }

    public void CloseSlides ()
    {
        slidesWindow.enabled = false;
    }

    public void OnVictoryAchieved()
	{
        AudioManager.m_instance.StopPlaying("GameMusic");

        GameSettingsScript.GameCompleted = true;
        victoryWindow.enabled = true;
    }
}
