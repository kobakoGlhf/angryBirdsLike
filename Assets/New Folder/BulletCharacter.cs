using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletCharacter : MonoBehaviour
{
    public bool _isBullet;
    [SerializeField] int _characterPower;
    [SerializeField] string _bounsDamageObjTagName;
    [SerializeField] float _moveTime;
    [SerializeField] float _animationJumpPower;
    private void Start()
    {
        if (_isBullet)
        {
            FindAnyObjectByType<NextCharacterManager>().
                SetCharactersQueue = this.gameObject;
        }
    }
    public int BounsDamage(Collision2D collision)
    {
        if (collision.gameObject.tag == _bounsDamageObjTagName && _bounsDamageObjTagName == "All")
        {
            return _characterPower * 2;
        }
        return _characterPower;
    }
    public void MovePos(GameObject obj)
    {
        var input = obj.GetComponent<ImputManager>();
        transform.DOJump(obj.transform.position, jumpPower:
            _animationJumpPower, numJumps: 1, duration: _moveTime).OnComplete(() => input.ChangeClikedTrue());
    }
}
