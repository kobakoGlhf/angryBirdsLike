using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : TargetObj
{
    UnityEvent _event;
    // Update is called once per frame
    public override void DestroyEffect()
    {
        _event?.Invoke();
        InGameManager.EnemyData.Remove(this);
    }
}
