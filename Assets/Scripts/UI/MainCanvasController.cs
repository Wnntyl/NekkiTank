using UnityEngine;
using UnityEngine.UI;

public class MainCanvasController : CanvasController
{
    [SerializeField]
    private Text speedText;

    private TankController _tankController;

    public override void Init(EntityController targetController)
    {
        base.Init(targetController);
        speedText.text = "0";

        _tankController = targetController as TankController;
        if(_tankController != null)
        {
            _tankController.OnMovementSpeedChange += ShowSpeedValue;
        }
    }

    private void ShowSpeedValue(float value)
    {
        speedText.text = value.ToString();
    }
}