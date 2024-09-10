using UnityEngine;
using UnityEngine.SceneManagement;

public class ImputManager : MonoBehaviour
{
    [SerializeField] float _clickLag = 0.2f;
    [SerializeField] float _power;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] int _simulationFrame;
    Vector2 _clickStartPos;
    Vector2 _clickEndPos;
    Vector2 _shootPos;
    float _timer;
    bool _clicked;
    bool _clickNow;
    GameObject _bullet;
    Rigidbody2D _bulletRigidbody;
    public GameObject Bullet
    {
        get => _bullet;
        set
        {
            if (_bullet == value || _bullet != null) return;
            value.GetComponent<BulletCharacter>().MovePos(gameObject);
            _bulletRigidbody = value.GetComponent<Rigidbody2D>();
            _bullet = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _bullet != null && _clicked)
        {
            _lineRenderer.enabled = true;
            _clickNow = true;
            _clickStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0) && _clickNow && _timer > _clickLag && _clicked)
        {
            ShootBullet();
            TimerReset();
            _lineRenderer.enabled = false;
        }
        else if (Input.GetMouseButton(1))//リセット用
        {
            TimerReset();
            _lineRenderer.enabled = false;
        }
        if (_clickNow)
        {
            _clickEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetVecter();
            Simulation(_bullet.transform, _bullet.transform.position, _shootPos);
            _timer += Time.deltaTime;
        }
        else _timer = 0;
    }
    public void ChangeClikedTrue()
    {
        _clicked = true;
    }
    void ShootBullet()
    {
        GetVecter();
        _bulletRigidbody.AddForce(_shootPos * _power, ForceMode2D.Impulse);
        _bullet = null;
        _clicked = false;
        _bulletRigidbody.gravityScale = 1;
    }
    void GetVecter()
    {
        //二点のベクトル取得
        _shootPos = (_clickStartPos - _clickEndPos) / 2;
        if (_shootPos.magnitude >= 3) _shootPos = _shootPos.normalized * 3;
    }
    void TimerReset()
    {
        _timer = 0;
        _clickNow = false;
    }
    void Simulation(Transform bullet, Vector2 pos, Vector2 _velocity)
    {
        var clone = Instantiate(bullet, pos, bullet.rotation);
        clone.GetComponent<BulletCharacter>()._isBullet = false;
        clone.GetComponent<Renderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(clone.gameObject,
            InGameManager.Instans.SimulationScene);
        var rb = clone.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.AddForce(_velocity*_power, ForceMode2D.Impulse);
        _lineRenderer.positionCount = _simulationFrame;
        for (int i = 0; i < _simulationFrame; i++)
        {
            InGameManager.Instans.SimulationPhysics.Simulate(Time.fixedDeltaTime);
            _lineRenderer.SetPosition(i, clone.transform.position);
        }
        Destroy(clone.gameObject);
    }
}
