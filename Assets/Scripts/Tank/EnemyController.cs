using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;

    private Transform _target;

    public float angle;

    private void Awake()
    {
        var tank = GameObject.Find("Tank");
        SetTarget(tank.transform);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        var direction = (_target.position - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + direction * 1f * Time.deltaTime);

        angle = Vector2.Angle(Vector2.right, direction);
        if (_target.position.y < transform.position.y)
            angle = 360f - angle;

        _rigidbody.MoveRotation(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}