using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public delegate void OnObjectiveChanged(ObjectiveData data);
    public static event OnObjectiveChanged onObjectiveChanged;

    private static ObjectiveData currentObjective;
    public static ObjectiveData CurrentObjective => currentObjective;

    [SerializeField]
    private List<ObjectiveData> objectives;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText, questionText, victoryScoreText;

    private ConsoleManagerScript consoleManager;
    private CompilerScript compiler;

    [SerializeField]
    private Canvas gameWindow, popupWindow, slidesWindow, victoryWindow;

    private int score = 0;
    public int Score => score;

    private int questionNum = 0;

    void Start()
    {
        AudioManager.m_instance.Play("GameMusic");

        consoleManager = GetComponent<ConsoleManagerScript>();
        compiler = GetComponent<CompilerScript>();

        compiler.Init();

        currentObjective = objectives[0];

        scoreText.text = $"Score: {score}";
        questionText.text = $"Question {questionNum + 1} of {objectives.Count}";

        // Calls onObjectiveChanged event
        if (onObjectiveChanged != null)
            onObjectiveChanged(currentObjective);

        slidesWindow.enabled = true;
    }

    public void OnRun()
    {
        // Clear output window
        consoleManager.ClearOutput();

        // Compile code
        var type = compiler.Compile(consoleManager.ReadFromInput());

        // Check for errors and display any
        if (type is null)
        {
            consoleManager.WriteToOutput(compiler.output);
        }

        // Run compiled code
        bool correct = compiler.Run(type);

        // If correct, add score
        if (!correct)
        {
            consoleManager.WriteToOutput("Output does not match expected output!");
            return;
        }

        var scoreToAdd = 20 - (5 * (HintTextScript.HintsUsed));

        gameWindow.GetComponent<CanvasGroup>().interactable = false;
        popupWindow.enabled = true;
        popupWindow.GetComponent<PopupScript>().Init(scoreToAdd, HintTextScript.HintsUsed);

        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    public void OnLoadNextObjective()
    {
        popupWindow.enabled = false;
        gameWindow.GetComponent<CanvasGroup>().interactable = true;

        // Load next obj
        if (++questionNum >= objectives.Count)
        {
            victoryWindow.enabled = true;
            victoryScoreText.text = $"Your score: {Score}";
            return;
        }

        questionText.text = $"Question {questionNum + 1} of {objectives.Count}";

        currentObjective = objectives[questionNum];

        // Calls onObjectiveChanged event
        if (onObjectiveChanged != null)
            onObjectiveChanged(currentObjective);

        consoleManager.ClearInput();
        consoleManager.ClearOutput();

        slidesWindow.enabled = true;
        GetComponent<SlideManagerScript>().Init();
    }
    public void SetPlayerScore()
    {
        GameSettingsScript.PlayerScore = Score;
        GameSettingsScript.GameCompleted = true;
    }
}
