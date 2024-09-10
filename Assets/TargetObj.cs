using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TargetObj : MonoBehaviour
{
    [SerializeField] int _hp;
    [SerializeField] int _overDamage;
    Rigidbody2D _rigidbody;
    BulletCharacter _hitObjIsCharacter;
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
        //�^���������H
        Vector2 relativeVelocity = collision.relativeVelocity;
        var force = _rigidbody.mass * relativeVelocity;
        if(force.magnitude <1)return 0;
        return (int)force.magnitude + bonusDamage(collision) / _overDamage;
    }
    public virtual void DestroyEffect()
    {
        InGameManager.Score += 1000;
    }
    public virtual void OnStart() { }
}
