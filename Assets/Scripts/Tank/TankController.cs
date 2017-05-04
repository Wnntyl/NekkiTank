using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController : EntityController
{
    private const float ANGULAR_SPEED = 60f;

    [SerializeField]
    private SpriteRenderer _weaponRenderer;
    [SerializeField]
    private Transform _launchPoint;

    private TankData _data;
    private int _currentWeaponIndex;
    private float _currentMovementVelocity;
    private float _currentAngleVelocity;
    private float _currentAngle;
    private Rigidbody2D _rigidbody;
    private ProjectileController _projectilePrefab;

    private void Awake()
    {
        _data = LoadData<TankData>("Data/Tank");
        _rigidbody = GetComponent<Rigidbody2D>();
        _projectilePrefab = Resources.Load<ProjectileController>("Prefabs/Projectile");
        CurrentHealth = _data.health;
    }

    public void Fire()
    {
        var projectile = Instantiate(_projectilePrefab, _launchPoint.position, transform.rotation);
        projectile.Init(CurrentWeapon.projectile);
    }

    public void MoveTowards()
    {
        _currentMovementVelocity = _data.maxSpeed;
    }

    public void MoveBackward()
    {
        _currentMovementVelocity = -_data.maxSpeed;
    }

    public void StopMovement()
    {
        _currentMovementVelocity = 0f;
    }

    public void RotateRight()
    {
        _currentAngleVelocity = -ANGULAR_SPEED;
    }

    public void RotateLeft()
    {
        _currentAngleVelocity = ANGULAR_SPEED;
    }

    public void StopRotation()
    {
        _currentAngleVelocity = 0f;
    }

    public void NextWeapon()
    {
        if (++_currentWeaponIndex >= _data.weapons.Length)
            _currentWeaponIndex = 0;

        SetWeaponSprite();
    }

    public void PreviousWeapon()
    {
        if (--_currentWeaponIndex < 0)
            _currentWeaponIndex = _data.weapons.Length - 1;

        SetWeaponSprite();
    }

    private void SetWeaponSprite()
    {
        _weaponRenderer.sprite = CurrentWeapon.sprite;
    }

    protected override void HandleCollision(GameObject partner)
    {
        switch (partner.tag)
        {
            case "Enemy":
                var enemy = partner.GetComponent<EnemyController>();
                SetDamage(enemy.Damage);
                Destroy(partner);
                break;
        }
    }

    protected override void Move()
    {
        _rigidbody.MovePosition(transform.position + transform.up * _currentMovementVelocity * Time.deltaTime);
    }

    protected override void Rotate()
    {
        _currentAngle += _currentAngleVelocity * Time.deltaTime;
        _rigidbody.MoveRotation(_currentAngle);
    }

    private WeaponData CurrentWeapon
    {
        get
        {
            return _data.GetWeapon(_currentWeaponIndex);
        }
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