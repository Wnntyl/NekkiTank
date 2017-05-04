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
    private Vector3 _currentDirection;

    private void Awake()
    {
        _enemyData = LoadData<EnemyData>("Data/Enemies/Enemy1");
        var tank = GameObject.Find("Tank");
        SetTarget(tank.transform);

        CurrentHealth = _enemyData.health;
        _renderer.sprite = _enemyData.sprite;

        CreateUi();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    protected override void Move()
    {
        _currentDirection = (_target.position - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + _currentDirection * _enemyData.maxSpeed * Time.deltaTime);
    }

    protected override void Rotate()
    {
        var angle = Vector2.Angle(Vector2.right, _currentDirection);
        if (_target.position.y < transform.position.y)
            angle = 360f - angle;

        _rigidbody.MoveRotation(angle);
    }

    protected override void HandleCollision(GameObject partner)
    {
        switch (partner.tag)
        {
            case "Bullet":
                var bullet = partner.GetComponent<BulletController>();
                SetDamage(bullet.Damage);
                Destroy(partner);
                break;
        }
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
            return CurrentHealth / _enemyData.health;
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