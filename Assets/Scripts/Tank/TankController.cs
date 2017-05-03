using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private const float ANGULAR_SPEED = 20f;

    [SerializeField]
    private SpriteRenderer _weaponRenderer;

    private TankData _tankData;
    private int _currentWeaponIndex;
    private float _currentMovementVelocity;
    private float _currentAngleVelocity;
    private float _currentAngle;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        LoadData();
        _rigidbody = GetComponent<Rigidbody2D>();
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

        SetWeaponSprite(_currentWeaponIndex);
    }

    public void PreviousWeapon()
    {
        if (--_currentWeaponIndex < 0)
            _currentWeaponIndex = _tankData.weapons.Length - 1;

        SetWeaponSprite(_currentWeaponIndex);
    }

    private void SetWeaponSprite(int index)
    {
        var sprite = _tankData.GetWeapon(index).sprite;
        _weaponRenderer.sprite = sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trigger");
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.up * _currentMovementVelocity * Time.deltaTime);

        _currentAngle += _currentAngleVelocity * Time.deltaTime;
        _rigidbody.MoveRotation(_currentAngle);
    }
}