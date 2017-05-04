using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _weaponRenderer;
    [SerializeField]
    private Transform _launchPoint;

    public float ReloadingProgress { get; private set; }
    
    private bool _ready;
    private ProjectileController _projectilePrefab;
    private WeaponData _data;

    private void Awake()
    {
        _projectilePrefab = Resources.Load<ProjectileController>("Prefabs/Projectile");
        _ready = true;
        ReloadingProgress = 1f;
    }

    public void Fire()
    {
        if (!_ready)
            return;

        var projectile = Instantiate(_projectilePrefab, _launchPoint.position, transform.rotation);
        projectile.Init(_data.projectile);

        if (_data.reloadingTime > 0f)
        {
            _ready = false;
            StartCoroutine(ReloadingCrtn());
        }
    }

    public void InstallWeapon(WeaponData data)
    {
        _data = data;
        _weaponRenderer.sprite = data.sprite;
    }

    private IEnumerator ReloadingCrtn()
    {
        for(var t = 0f; t < _data.reloadingTime; t += Time.deltaTime)
        {
            ReloadingProgress = t / _data.reloadingTime;
            yield return null;
        }

        _ready = true;
    }
}