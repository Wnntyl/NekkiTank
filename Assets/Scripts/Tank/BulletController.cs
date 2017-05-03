using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletController : MonoBehaviour
{
    private const float SPEED = 10f;

    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Rigidbody2D _rigidbody;

    public float Damage { get; private set; }

    public void SetParameters(string spriteName, float damage)
    {
        var tanksSpritesheet = Resources.LoadAll<Sprite>("GFX/tanks_spritesheet");
        _renderer.sprite = tanksSpritesheet.Single(s => s.name == spriteName);

        Damage = damage;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.up * SPEED * Time.deltaTime);
    }
}