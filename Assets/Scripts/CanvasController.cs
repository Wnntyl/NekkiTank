using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _healthBar;

    [SerializeField]
    private RectTransform _armorBar;

    private float _healthWidth;

    protected EntityController _targetController;

    public virtual void Init(EntityController targetController)
    {
        _targetController = targetController;
        _healthWidth = _healthBar.sizeDelta.x;

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
        var image = _armorBar.GetComponent<Image>();
        image.color = Color.Lerp(Color.white, Color.black, _targetController.Armor);
    }
}