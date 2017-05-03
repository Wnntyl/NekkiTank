using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private TankData _tankData;
    private WeaponData [] _weaponDatas;
    private WeaponData _currentWeaponData;
    private float _currentMovementVelocity;
    private float _currentAngleVelocity;
    private float _currentAngle;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Fire()
    {

    }

    public void MoveTowards()
    {
        _currentMovementVelocity = 1f;
    }

    public void MoveBackward()
    {
        _currentMovementVelocity = -1f;
    }

    public void Stop()
    {
        _currentMovementVelocity = 0f;
    }

    public void RotateRight()
    {
        _currentAngleVelocity = -20f;
    }

    public void RotateLeft()
    {
        _currentAngleVelocity = 20f;
    }

    public void StopRotation()
    {
        _currentAngleVelocity = 0f;
    }

    public void NextWeapon()
    {

    }

    public void PreviousWeapon()
    {

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