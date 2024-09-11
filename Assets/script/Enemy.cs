using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : TargetObj
{
    [SerializeField] UnityEvent _event;
    // Update is called once per frame
    public override void OnDestroyEffect()
    {
        _event?.Invoke();
        InGameManager.EnemyData.Remove(this);
    }
}
