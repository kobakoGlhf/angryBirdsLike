using System.Collections.Generic;
using UnityEngine;

public class NextCharacterManager : MonoBehaviour
{
    [SerializeField] public Queue<GameObject> _charactersQueue = new Queue<GameObject>();
    InputManager _inputManager;
    public int CharactersQueueCount
    {
        get
        {
            int bulletCount = _inputManager.Bullet ? 1 : 0;
            return _charactersQueue.Count + bulletCount;
        }
    }
    public int CharactersCount
    {
        get
        {
            return _charactersQueue.Count;
        }
    }
    public GameObject SetCharactersQueue
    {
        set
        {
            _charactersQueue.Enqueue(value);
        }
    }
    private void Start()
    {
        _inputManager = FindAnyObjectByType<InputManager>();
    }
    void Update()
    {
        if (_charactersQueue.Count > 0 && _inputManager.Bullet == null)
        {
            _inputManager.Bullet = _charactersQueue.Dequeue();
        }
    }
}
