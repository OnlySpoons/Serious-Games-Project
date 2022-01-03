using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public delegate void OnObjectiveChanged(ObjectiveData data);
    public static event OnObjectiveChanged onObjectiveChanged;

    public static ObjectiveData currentObjective;
    public List<ObjectiveData> objectives;
    public TMPro.TextMeshProUGUI scoreText, questionText;

    private ConsoleManagerScript consoleManager;
    private CompilerScript compiler;

    private int score = 0;
    private int questionNum = 0;

    void Start()
    {
        consoleManager = GetComponent<ConsoleManagerScript>();
        compiler = GetComponent<CompilerScript>();

        compiler.Init();

        currentObjective = objectives[0];

        scoreText.text = $"Score: {score}";
        questionText.text = $"Question {questionNum + 1} of {objectives.Count}";

        // Calls onObjectiveChanged event
        if (onObjectiveChanged != null)
            onObjectiveChanged(currentObjective);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRun()
    {
        //string source =
        // "using System;" +
        // "class Test" +
        // "{" +
        // " void Main()" +
        // " {" +
        // " Console.WriteLine(\"Hello World!\");" +
        // " }" +
        // "}";

        // Compile code
        //var type = compiler.Compile(source);
        var type = compiler.Compile(consoleManager.ReadFromInput());

        // Check for errors and display any
        if (type is null)
        {
            consoleManager.WriteToOutput(compiler.output);
            return;
        }

        // Run compiled code
        bool correct = compiler.Run(type);

        // If correct, add score
        if (!correct)
            return;

        score += 20 - (5 * (HintTextScript.index + 1));
        scoreText.text = $"Score: {score}";

        // Load next obj
        if (++questionNum >= objectives.Count)
        {
            // End program, return to menu scene(?)
            return;
        }

        questionText.text = $"Question {questionNum + 1} of {objectives.Count}";

        currentObjective = objectives[questionNum];

        // Calls onObjectiveChanged event
        if (onObjectiveChanged != null)
            onObjectiveChanged(currentObjective);
    }
}
