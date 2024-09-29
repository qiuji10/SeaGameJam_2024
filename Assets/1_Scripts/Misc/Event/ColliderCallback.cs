using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderCallback : MonoBehaviour
{
    [SerializeField, NaughtyAttributes.Tag] private string _tag;
    public event Action<ColliderCallback> OnEnter;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(_tag))
            OnEnter?.Invoke(this);
    }
}
