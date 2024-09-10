using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instans;
    public static int Score;
    public static List<Enemy> EnemyData = new List<Enemy>();
    public Scene SimulationScene;
    public PhysicsScene2D SimulationPhysics;
    [SerializeField] UnityEvent _clear;
    [SerializeField] UnityEvent _defeat;
    Transform _table;
    List<Rigidbody2D> _rbList=new List<Rigidbody2D>();
    [SerializeField] List<GameObject> Stage=new List<GameObject>();
    [SerializeField] GameObject Highscore;
    NextCharacterManager _nextCharacterManager;
    InputManager _inputManager;
    bool _isGameEnd;
    private void Awake()
    {
        Score = 0;
        if (Instans != null)
        {
            Destroy(gameObject);
            return;
        }
        Instans = this;
        Debug.Log(Stage.Count);
        Debug.Log(Actions.Stage);
        int stageNum = !(Stage.Count < Actions.Stage) ? Actions.Stage : 0;
        Debug.Log(stageNum);
        Stage.ForEach(obj => obj.SetActive(false));
        Stage[stageNum].SetActive(true);
        _table = Stage[stageNum].transform.GetChild(0);
    }
    private void Start()
    {
        EnemyData = FindObjectsOfType<Enemy>().ToList();
        CreatSimulationCcene();
        _rbList=FindObjectsOfType<Rigidbody2D>().ToList();
        _nextCharacterManager=FindAnyObjectByType<NextCharacterManager>();
        _inputManager=FindAnyObjectByType<InputManager>();
    }
    private void Update()
    {
        if (EnemyData.Count <= 0&& AllRigidbodyIsStoped()&&!_isGameEnd)
        {
            GameClear();
        }
        else if(_nextCharacterManager._charactersQueue.Count<=0&& 
            AllRigidbodyIsStoped() && !_isGameEnd&&_inputManager.Bullet==null)
        {
            GameDefeat();
        }
    }
    void GameDefeat()
    {
        _isGameEnd=true;
        _defeat.Invoke();

    }
    void GameClear()
    {
        Score += _nextCharacterManager.CharactersQueueCount * 1000;
        _isGameEnd = true;
        _clear.Invoke();
        HighScoreManager.HighScores["Stage"+Actions.Stage].Clear=true;
        if(HighScoreManager.HighScores["Stage" + Actions.Stage].HighScore < Score)
        {
            Highscore.SetActive(true);
            HighScoreManager.HighScores["Stage" + Actions.Stage].HighScore = Score;
        }
    }
    bool AllRigidbodyIsStoped()
    {
        foreach(var rb in _rbList){
            if (rb == null) { }
            // 速度と角速度が指定の閾値を超えていたら、停止していないと見なす
            else if (rb.velocity.magnitude > 1)
            {
                return false;  // 一つでも動いていたらfalse
            }
        }
        return true;
    }
    void CreatSimulationCcene()
    {
        SimulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        SimulationPhysics = SimulationScene.GetPhysicsScene2D();
        foreach (Transform obj in _table)
        {
            var clone = Instantiate(obj.gameObject, obj.position, obj.rotation);
            if(clone.TryGetComponent(out Renderer renderer))renderer.enabled=false;
            if(clone.TryGetComponent(out TilemapRenderer tRenderer))tRenderer.enabled=false;
            SceneManager.MoveGameObjectToScene(clone, SimulationScene);
            if (obj.TryGetComponent(out TilemapCollider2D tilemap))
            {
                clone.GetComponent<TilemapCollider2D>().usedByComposite=tilemap.usedByComposite;
            }
        }
    }
}
