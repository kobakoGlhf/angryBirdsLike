using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatHighScore : MonoBehaviour
{
    [SerializeField] int _stageCount;
    private void Start()
    {
        if (HighScoreManager.HighScores.Count == 0)
        {
            for(int i = 0; i < _stageCount; i++)
            {
                HighScoreManager.SetDictionary("Stage" + i, 0, false);
            }
        }
    }
}
public static class HighScoreManager
{
    public static Dictionary<string, StageData> HighScores = 
        new Dictionary<string, StageData>();
    public static void SetDictionary(string str, int score,bool clear)
    {
        HighScores.Add(str, new StageData(clear, score));
    }
}
public class StageData
{
    public bool Clear;
    public int HighScore;
    public StageData(bool clear, int score)
    {
        Clear = clear; HighScore = score;
    }
}
