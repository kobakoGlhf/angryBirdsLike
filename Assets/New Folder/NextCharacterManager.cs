using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCharacterManager : MonoBehaviour
{
    [SerializeField] Queue<GameObject> _charactersQueue=new Queue<GameObject>();
    ImputManager _imputManager;
    public int CharactersCount
    {
        get
        {
            Debug.Log(_charactersQueue.Count);
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
        _imputManager=FindAnyObjectByType<ImputManager>();
    }
    void Update()
    {
        if (_charactersQueue.Count > 0 && _imputManager.Bullet == null)
        {
            _imputManager.Bullet=_charactersQueue.Dequeue();
        }
    }
}
