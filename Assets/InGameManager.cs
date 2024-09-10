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
    [SerializeField] Transform _table;
    private void Awake()
    {
        if (Instans != null)
        {
            Destroy(gameObject);
            return;
        }
        Instans = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        EnemyData = FindObjectsOfType<Enemy>().ToList();
        CreatSimulationCcene();
    }
    private void Update()
    {
        if (EnemyData.Count <= 0)
        {
            GameClear();
        }
    }
    void GameClear()
    {
        _clear.Invoke();
        Debug.Log("Ÿ—˜");
    }
    void CreatSimulationCcene()
    {
        SimulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        SimulationPhysics = SimulationScene.GetPhysicsScene2D();
        foreach (Transform obj in _table)
        {
            var clone = Instantiate(obj.gameObject, obj.position, obj.rotation);
            if(clone.TryGetComponent(out Renderer renderer))renderer.enabled=false;
            SceneManager.MoveGameObjectToScene(clone, SimulationScene);
            if (obj.TryGetComponent(out TilemapCollider2D tilemap))
            {
                clone.GetComponent<TilemapCollider2D>().usedByComposite=tilemap.usedByComposite;
            }
        }
    }
}
