using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvasController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _healthBar;

    [SerializeField]
    private RectTransform _armorBar;

    private float _healthWidth;
    private float _armorWidth;
    private EnemyController _targetController;

    public void Init(EnemyController targetController)
    {
        _targetController = targetController;
        _healthWidth = _healthBar.sizeDelta.x;
        _armorWidth = _armorBar.sizeDelta.y;
        UpdateHealthBar();
        UpdateArmorBar();

        _targetController.OnHealthChange += UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        var newWidth = _healthWidth * _targetController.HealthStatus;
        _healthBar.sizeDelta = new Vector2(newWidth, _healthBar.sizeDelta.y);
    }

    private void UpdateArmorBar()
    {
        var newWidth = _armorWidth * _targetController.Armor;
        _armorBar.sizeDelta = new Vector2(newWidth, _armorBar.sizeDelta.y);
    }

    private void Update()
    {
        if(_targetController == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = _targetController.transform.position;
    }
}