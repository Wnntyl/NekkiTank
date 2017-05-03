using UnityEngine;
using UnityEngine.UI;

public class MainCanvasController : CanvasController
{
    [SerializeField]
    private Text speedText;

    public override void Init(EntityController targetController)
    {
        base.Init(targetController);
        speedText.text = targetController.MaxSpeed.ToString();
    }
}