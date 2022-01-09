using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText, victoryScoreText;

	private int score = 0;
    public int Score => score;

	void Start ()
	{
		scoreText.text = $"Score: { score }";
	}

	void OnEnable ()
	{
		ObjectiveManagerScript.onVictoryAchieved += OnVictoryAchieved;
	}

	void OnDisable ()
	{
		ObjectiveManagerScript.onVictoryAchieved -= OnVictoryAchieved;
	}

	public void UpdateScore( int scoreToAdd )
	{
		score += scoreToAdd;
		scoreText.text = $"Score: { score }";
	}

	public void OnVictoryAchieved ()
	{
		victoryScoreText.text = $"Your score: { score }";
		GameSettingsScript.PlayerScore = score;
	}
}
