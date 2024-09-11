using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TargetObj : MonoBehaviour
{
    [SerializeField] int _hp;
    [SerializeField] int _overDamage;
    Rigidbody2D _rigidbody;
    BulletCharacter _hitObjIsCharacter;
    [SerializeField] int _score;
    [SerializeField] AudioClip _destroyAudio;
    [SerializeField] float _volume;
    AudioSource _audioSource;
    int Hp { get => _hp; set
        {
            _hp = value;
            if (_hp <= 0)
            {
                Destroy(gameObject);
                DestroyEffect();
            }
        } }
    private void Start()
    {
        OnStart();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource=gameObject.AddComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destroy") Hp = 0;
        else if (collision.gameObject.TryGetComponent(out _hitObjIsCharacter))
        {
            Hp -= CollisionDamage(collision, collision => _hitObjIsCharacter.BounsDamage(collision));
            return;
        }
        else Hp -= CollisionDamage(collision, collision => { return 0; });
    }
    int CollisionDamage(Collision2D collision,System.Func<Collision2D,int> bonusDamage)
    {
        //‰^“®•û’ö®H
        Vector2 relativeVelocity = collision.relativeVelocity;
        var force = _rigidbody.mass * relativeVelocity;
        if(force.magnitude <1)return 0;
        return (int)force.magnitude + bonusDamage(collision) / _overDamage;
    }
    public void DestroyEffect()
    {
        InGameManager.Score += _score;
        AudioSource.PlayClipAtPoint(_destroyAudio,Vector3.zero,_volume);
        OnDestroyEffect();
    }
    public virtual void OnDestroyEffect() { }
    public virtual void OnStart() { }
}
