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
    Transform _table;
    List<Rigidbody2D> _rbList=new List<Rigidbody2D>();
    [SerializeField] List<GameObject> Stage=new List<GameObject>();
    [SerializeField] GameObject Highscore;
    private void Awake()
    {
        if (Instans != null)
        {
            Destroy(gameObject);
            return;
        }
        Instans = this;
        int stageNum = (Stage.Count < SceneChanger.Stage) ? SceneChanger.Stage : 0;
        Stage.ForEach(obj => obj.SetActive(false));
        Stage[stageNum].SetActive(true);
        _table = Stage[stageNum].transform.GetChild(0);
    }
    private void Start()
    {
        EnemyData = FindObjectsOfType<Enemy>().ToList();
        CreatSimulationCcene();
        _rbList=FindObjectsOfType<Rigidbody2D>().ToList();
    }
    private void Update()
    {
        if (EnemyData.Count <= 0&& AllRigidbodyIsStoped())
        {
            GameClear();
        }
    }
    void GameClear()
    {
        _clear.Invoke();
        HighScoreManager.HighScores["Stage"+Score].Clear=true;
        if(HighScoreManager.HighScores["Stage" + Score].HighScore < Score)
        {
            Highscore.SetActive(true);
            HighScoreManager.HighScores["Stage" + Score].HighScore=Score;
        }
        Debug.Log("勝利");
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
