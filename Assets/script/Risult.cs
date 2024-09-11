using DG.Tweening;
using TMPro;
using UnityEngine;

public class Risult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _stageName;
    [SerializeField] TextMeshProUGUI _isClear;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _highScore;
    NextCharacterManager _characters;
    private void Awake()
    {
        _characters = FindAnyObjectByType<NextCharacterManager>();
        Debug.Log(_characters);
    }
    public void RisultTextChange(bool clear)
    {
        _stageName.text = "Stage" + Actions.Stage.ToString();
        _isClear.text = clear ? "victory" : "Defeat";
        int Scored = InGameManager.Score - _characters.CharactersQueueCount * 2000;
        if (clear)
        {
            DOTween.To(() => Scored,
                x => _score.text = "Score : " + x.ToString(), InGameManager.Score, 1);
            _highScore.text = "HighScore : " + InGameManager.Score.ToString();
        }
        else
        {
            _score.gameObject.SetActive(false);
        }
    }
}
