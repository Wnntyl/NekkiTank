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

    private DangerData _data;
    private Transform _target;
    private Vector3 _currentDirection;

    public void Init(string name, Transform target)
    {
        _data = LoadData<DangerData>("Data/Enemies/" + name);

        Damage = _data.damage;
        _target = target;
        CurrentHealth = _data.health;
        _renderer.sprite = _data.sprite;

        CreateUi();
    }

    protected override void Move()
    {
        if (_target == null)
            return;

        _currentDirection = (_target.position - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + _currentDirection * _data.maxSpeed * Time.deltaTime);
    }

    protected override void Rotate()
    {
        if (_target == null)
            return;

        var angle = Vector2.Angle(Vector2.right, _currentDirection);
        if (_target.position.y < transform.position.y)
            angle = 360f - angle;

        _rigidbody.MoveRotation(angle);
    }

    protected override void HandleCollision(GameObject partner)
    {
        switch (partner.tag)
        {
            case "Projectile":
                var projectile = partner.GetComponent<ProjectileController>();
                SetDamage(projectile.Damage);
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

        var root = GameObject.Find("EnemyCanvases");
        canvas.transform.SetParent(root.transform, false);
    }

    public override float HealthStatus
    {
        get
        {
            return CurrentHealth / _data.health;
        }
    }

    public override float Armor
    {
        get
        {
            return _data.armor;
        }
    }

    public override float MaxSpeed
    {
        get
        {
            return _data.maxSpeed;
        }
    }
}