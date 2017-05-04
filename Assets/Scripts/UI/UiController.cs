using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private MainCanvasController _canvasController;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private RectTransform _reloadingBar;

    private TankController _tankController;
    private float _reloadingWidth;

    private void Start()
    {
        var tank = GameObject.Find("Tank");
        _tankController = tank.GetComponent<TankController>();
        _canvasController.Init(_tankController);

        _tankController.OnDeath += () => { if(_restartButton != null) _restartButton.gameObject.SetActive(true); };
        _restartButton.onClick.AddListener(Restart);
        _reloadingWidth = _reloadingBar.sizeDelta.x;
    }

    private void Update()
    {
        UpdateReloadingBar();
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateReloadingBar()
    {
        var newWidth = _reloadingWidth * _tankController.ReloadingProgress;
        _reloadingBar.sizeDelta = new Vector2(newWidth, _reloadingBar.sizeDelta.y);
    }
}