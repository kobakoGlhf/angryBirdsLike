using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    NextCharacterManager _characterManager;
    [SerializeField] TextMeshProUGUI _characterCountText;
    [SerializeField] TextMeshProUGUI _scoreText;
    int _characterCount;
    [SerializeField] string _scoreFormat;
    private void Start()
    {
        _characterManager=FindObjectOfType<NextCharacterManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_characterCount != InGameManager.Score)
        {
            _scoreText.text = _scoreFormat + InGameManager.Score.ToString();
            _characterCount = InGameManager.Score;
        }
        if (_characterCount != _characterManager.CharactersCount)
        {
            _characterCountText.text = _characterManager.CharactersCount + 1.ToString();
            _characterCount = _characterManager.CharactersCount;
        }
    }
}
