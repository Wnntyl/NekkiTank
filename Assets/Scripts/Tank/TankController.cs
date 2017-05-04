using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController : EntityController
{
    private const float ANGULAR_SPEED = 180f;

    [SerializeField]
    WeaponController _weaponController;

    public float CurrentMovementVelocity { get; private set; }

    private TankData _data;
    private int _currentWeaponIndex;
    private float _currentAngleVelocity;
    private float _currentAngle;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _data = LoadData<TankData>("Data/Tank");
        _rigidbody = GetComponent<Rigidbody2D>();
        CurrentHealth = _data.health;
        ChangeWeapon();
    }

    public float ReloadingProgress
    {
        get
        {
            return _weaponController.ReloadingProgress;
        }
    }

    public void Fire()
    {
        _weaponController.Fire();
    }

    public void MoveTowards()
    {
        CurrentMovementVelocity = _data.maxSpeed;
        InvokeMovementSpeedChangeEvent(CurrentMovementVelocity);
    }

    public void MoveBackward()
    {
        CurrentMovementVelocity = -_data.maxSpeed;
        InvokeMovementSpeedChangeEvent(CurrentMovementVelocity);
    }

    public void StopMovement()
    {
        CurrentMovementVelocity = 0f;
        InvokeMovementSpeedChangeEvent(CurrentMovementVelocity);
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

        ChangeWeapon();
    }

    public void PreviousWeapon()
    {
        if (--_currentWeaponIndex < 0)
            _currentWeaponIndex = _data.weapons.Length - 1;

        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        _weaponController.InstallWeapon(CurrentWeapon);
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
        _rigidbody.MovePosition(transform.position + transform.up * CurrentMovementVelocity * Time.deltaTime);
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