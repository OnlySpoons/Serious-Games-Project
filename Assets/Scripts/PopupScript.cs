using UnityEngine;

public class PopupScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText, hintText;

    public void Init(int score, int hintsUsed)
    {
        scoreText.text = $"Your score: {score.ToString()}";
        hintText.text = $"You used {hintsUsed.ToString()} of 3 hints.";
    }
}
