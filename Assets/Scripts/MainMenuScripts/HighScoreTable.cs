using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour 
{
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;
    HighScores highScores;
    bool updateTable;

    void OnEnable() 
    {
        if(GameSettingsScript.GameCompleted)
        {
            AddHighScoreTableEntry(GameSettingsScript.PlayerScore, GameSettingsScript.PlayerName);
            GameSettingsScript.GameCompleted = false;
            GameSettingsScript.PlayerScore = 0;
        }

        entryTemplate.gameObject.SetActive(false);

        string jsonString = "";
        highScores = new HighScores();

        if (PlayerPrefs.HasKey("HighScoreTable"))
        {
            jsonString = PlayerPrefs.GetString("HighScoreTable");
            highScores = JsonUtility.FromJson<HighScores>(jsonString);
        }
        else
        {
            // Initialise default table
            var temp = new List<HighScoreEntry>();
            highScores.highScoreEntryList = temp;

            jsonString = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("HighScoreTable", jsonString);
        }

        UpdateHighScoreTable(highScores);
        updateTable = true;
    }

    void OnDisable()
    {
        updateTable = false;
    }

    void Update()
    {
        if (updateTable)
        {
            UpdateHighScoreTable(highScores);
            updateTable = false;
        }
    }

    private void CreateHighScoreEntry(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList) 
    {
        float templateHeight = 31f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank) 
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.Score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.Name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        // Change background for every odd entry
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        
        // Highlight first
        if (rank == 1) 
        {
            entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("NameText").GetComponent<Text>().color = Color.green;
        }

        switch (rank) 
        {
        default:
            entryTransform.Find("TrophyImg").gameObject.SetActive(false);
            break;
        case 1:
            entryTransform.Find("TrophyImg").GetComponent<Image>().color = Utils.GetColorFromString("FFD200");
            break;
        case 2:
            entryTransform.Find("TrophyImg").GetComponent<Image>().color = Utils.GetColorFromString("C6C6C6");
            break;
        case 3:
            entryTransform.Find("TrophyImg").GetComponent<Image>().color = Utils.GetColorFromString("B76F56");
            break;
        }

        transformList.Add(entryTransform);
    }

    public void AddHighScoreTableEntry(int score, string name) 
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry { Score = score, Name = name };
        
        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
            highScores = new HighScores() { highScoreEntryList = new List<HighScoreEntry>() };

        highScores.highScoreEntryList.Add(highScoreEntry);

        if (highScores.highScoreEntryList.Count > 10)
        {
            for (int h = highScores.highScoreEntryList.Count; h > 10; h--)
            {
                highScores.highScoreEntryList.RemoveAt(10);
            }
        }

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
    }

    public void AddScore()
    {
        AddHighScoreTableEntry(69, "Harry's Burd");
    }

    private void UpdateHighScoreTable(HighScores highScores)
    {
        DeleteHighScoreTableContents();

        // Sort
        for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j].Score > highScores.highScoreEntryList[i].Score)
                {
                    HighScoreEntry temp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = temp;
                }
            }
        }

        // Cap list at 10 entries
        if (highScores.highScoreEntryList.Count > 10)
        {
            for (int h = highScores.highScoreEntryList.Count; h > 10; h--)
            {
                highScores.highScoreEntryList.RemoveAt(10);
            }
        }

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScores.highScoreEntryList)
        {
            CreateHighScoreEntry(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }

    private class HighScores 
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    [System.Serializable] 
    private class HighScoreEntry {
        public int Score;
        public string Name;
    }

    public void ClearHighScores()
    {
        PlayerPrefs.DeleteKey("HighScoreTable");
        DeleteHighScoreTableContents();
    }

    // Tmeporary
    public void DeleteHighScoreTableContents()
    {
        foreach (Transform child in entryContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
