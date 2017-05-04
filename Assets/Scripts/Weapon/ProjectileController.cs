using UnityEngine;

public class ProjectileController : EntityController
{
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Rigidbody2D _rigidbody;

    private ProjectileData _data;

    public void Init(string projectileName)
    {
        _data = LoadData<ProjectileData>("Data/Projectiles/" + projectileName);
        Damage = _data.damage;
        _renderer.sprite = _data.sprite;
    }

    protected override void Move()
    {
        _rigidbody.MovePosition(transform.position + transform.up * _data.maxSpeed * Time.deltaTime);
    }

    protected override void HandleCollision(GameObject partner)
    {
        if (partner.tag == "Bound" || partner.tag == "Obstacle")
            Destroy(gameObject);
    }

    public override float HealthStatus { get { return 0; } }
    public override float Armor { get { return 0; } }
    public override float MaxSpeed { get { return _data.maxSpeed; } }
}