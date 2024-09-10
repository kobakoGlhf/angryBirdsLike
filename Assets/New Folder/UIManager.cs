using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    NextCharacterManager _characterManager;
    [SerializeField] TextMeshProUGUI _characterCountText;
    [SerializeField] TextMeshProUGUI _scoreText;
    int _characterCount;
    int _scoreCount;
    [SerializeField] string _scoreFormat;
    private void Start()
    {
        _characterManager=FindObjectOfType<NextCharacterManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_scoreCount != InGameManager.Score)
        {
            DOTween.To(() =>  _scoreCount,
                x =>
                {
                    _scoreText.text = "Score : " + x;
                }, InGameManager.Score, 1); ;
            _scoreCount = InGameManager.Score;
        }
        if (_characterCount != _characterManager.CharactersCount)
        {
            _characterCountText.text = (_characterManager.CharactersCount + 1).ToString();
            _characterCount = _characterManager.CharactersCount;
        }
    }
}
