﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EntityController : MonoBehaviour
{
    public event Action OnHealthChange;

    public float CurrentHealth { get; protected set; }
    public abstract float HealthStatus { get; }
    public abstract float Armor { get; }
    public abstract float MaxSpeed { get; }

    protected abstract void HandleCollision(GameObject partner);

    protected void InvokeHealthChangedEvent()
    {
        if (OnHealthChange != null)
            OnHealthChange();
    }

    protected T LoadData<T>(string path)
    {
        var textAsset = Resources.Load(path) as TextAsset;
        if (textAsset == null)
        {
            Debug.LogErrorFormat("Can't load \'{0}\'!", path);
            return default(T);
        }

        return JsonUtility.FromJson<T>(textAsset.text);
    }

    protected void SetDamage(float value)
    {
        CurrentHealth -= value * (1f - Armor);

        InvokeHealthChangedEvent();

        if (CurrentHealth <= 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    protected virtual void Move() { }
    protected virtual void Rotate() { }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == null)
            return;

        HandleCollision(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == null)
            return;

        HandleCollision(col.gameObject);
    }
}