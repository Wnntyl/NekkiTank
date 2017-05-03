using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private SpriteRenderer _renderer;

    private EnemyData _enemyData;
    private Transform _target;
    private float _currentHealth;

    private void Awake()
    {
        LoadData();
        var tank = GameObject.Find("Tank");
        SetTarget(tank.transform);

        _currentHealth = _enemyData.health;
        _renderer.sprite = _enemyData.sprite;

        CreateUi();
    }

    private void LoadData()
    {
        var textAsset = Resources.Load("Data/Enemies/Enemy1") as TextAsset;
        if (textAsset == null)
        {
            Debug.LogError("Can't load Enemy data!");
            return;
        }

        _enemyData = JsonUtility.FromJson<EnemyData>(textAsset.text);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        var direction = (_target.position - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + direction * _enemyData.maxSpeed * Time.deltaTime);

        var angle = Vector2.Angle(Vector2.right, direction);
        if (_target.position.y < transform.position.y)
            angle = 360f - angle;

        _rigidbody.MoveRotation(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null)
            return;

        switch (collision.gameObject.tag)
        {
            case "Bullet":
                var bullet = collision.gameObject.GetComponent<BulletController>();
                SetDamage(bullet.Damage);
                Destroy(collision.gameObject);
                break;
        }
    }

    private void SetDamage(float value)
    {
        _currentHealth -= value * (1f - _enemyData.armor);

        InvokeHealthChangedEvent();

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    private void CreateUi()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/EnemyCanvas");
        var canvas = Instantiate(prefab);
        var controller = canvas.GetComponent<EnemyCanvasController>();
        controller.Init(this);
    }

    public override float HealthStatus
    {
        get
        {
            return _currentHealth / _enemyData.health;
        }
    }

    public override float Armor
    {
        get
        {
            return _enemyData.armor;
        }
    }

    public float Damage
    {
        get
        {
            return _enemyData.damage;
        }
    }

    public override float MaxSpeed
    {
        get
        {
            return _enemyData.maxSpeed;
        }
    }
}