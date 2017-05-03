using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EntityController : MonoBehaviour
{
    public event Action OnHealthChange;

    protected void InvokeHealthChangedEvent()
    {
        if (OnHealthChange != null)
            OnHealthChange();
    }

    public abstract float HealthStatus { get; }
    public abstract float Armor { get; }
}