using UnityEngine;
using System;

public abstract class EntityController : MonoBehaviour
{
    public event Action OnDeath;
    public event Action OnHealthChange;
    public event Action<float> OnMovementSpeedChange;

    public float CurrentHealth { get; protected set; }
    public float Damage { get; protected set; }

    public abstract float HealthStatus { get; }
    public abstract float Armor { get; }
    public abstract float MaxSpeed { get; }

    protected virtual void Move() { }
    protected virtual void Rotate() { }
    protected virtual void HandleCollision(GameObject partner) { }

    protected void InvokeHealthChangeEvent()
    {
        if (OnHealthChange != null)
            OnHealthChange();
    }

    protected void InvokeMovementSpeedChangeEvent(float value)
    {
        if (OnMovementSpeedChange != null)
            OnMovementSpeedChange(value);
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

        InvokeHealthChangeEvent();

        if (CurrentHealth <= 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

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

    private void OnDestroy()
    {
        if (OnDeath != null)
            OnDeath();
    }
}