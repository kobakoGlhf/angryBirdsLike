using TMPro;
using UnityEngine;

public class Risult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _stageName;
    [SerializeField] TextMeshProUGUI _isClear;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _highScore;
    public void RisultTextChange(bool clear)
    {
        _stageName.text = "Stage" + SceneChanger.Stage;
        _isClear.text = clear ? "victory" : "Defeat";
        if (clear)
        {
            _score.text = "Score : " + InGameManager.Score.ToString();
            _highScore.text = "HighScore : " + InGameManager.Score.ToString();
        }
        else
        {
            _score.gameObject.SetActive(false);
            _highScore.gameObject.SetActive(false);
        }
    }
}
