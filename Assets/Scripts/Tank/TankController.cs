using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankController : EntityController
{
    private const float ANGULAR_SPEED = 40f;

    [SerializeField]
    private SpriteRenderer _weaponRenderer;
    [SerializeField]
    private Transform _launchPoint;

    private TankData _tankData;
    private int _currentWeaponIndex;
    private float _currentMovementVelocity;
    private float _currentAngleVelocity;
    private float _currentAngle;
    private Rigidbody2D _rigidbody;
    private BulletController _bulletPrefab;
    private float _currentHealth;

    private void Awake()
    {
        LoadData();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bulletPrefab = Resources.Load<BulletController>("Prefabs/Bullet");
        _currentHealth = _tankData.health;
    }

    private void LoadData()
    {
        var textAsset = Resources.Load("Data/Tank") as TextAsset;
        if(textAsset == null)
        {
            Debug.LogError("Can't load Tank data!");
            return;
        }

        _tankData = JsonUtility.FromJson<TankData>(textAsset.text);
    }

    public void Fire()
    {
        var bullet = Instantiate(_bulletPrefab, _launchPoint.position, transform.rotation);
        bullet.SetParameters(CurrentWeapon.bulletSpriteName, CurrentWeapon.damage);
    }

    public void MoveTowards()
    {
        _currentMovementVelocity = _tankData.maxSpeed;
    }

    public void MoveBackward()
    {
        _currentMovementVelocity = -_tankData.maxSpeed;
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
        if (++_currentWeaponIndex >= _tankData.weapons.Length)
            _currentWeaponIndex = 0;

        SetWeaponSprite();
    }

    public void PreviousWeapon()
    {
        if (--_currentWeaponIndex < 0)
            _currentWeaponIndex = _tankData.weapons.Length - 1;

        SetWeaponSprite();
    }

    private void SetWeaponSprite()
    {
        _weaponRenderer.sprite = CurrentWeapon.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null)
            return;

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                var enemy = collision.gameObject.GetComponent<EnemyController>();
                SetDamage(enemy.Damage);
                Destroy(collision.gameObject);
                break;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.up * _currentMovementVelocity * Time.deltaTime);

        _currentAngle += _currentAngleVelocity * Time.deltaTime;
        _rigidbody.MoveRotation(_currentAngle);
    }

    private WeaponData CurrentWeapon
    {
        get
        {
            return _tankData.GetWeapon(_currentWeaponIndex);
        }
    }

    private void SetDamage(float value)
    {
        _currentHealth -= value * (1f - _tankData.armor);

        InvokeHealthChangedEvent();

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    public override float HealthStatus
    {
        get
        {
            return _currentHealth / _tankData.health;
        }
    }

    public override float Armor
    {
        get
        {
            return _tankData.armor;
        }
    }
}