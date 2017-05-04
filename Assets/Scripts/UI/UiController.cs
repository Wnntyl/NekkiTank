using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private MainCanvasController _canvasController;
    [SerializeField]
    private Button _restartButton;

    private void Start()
    {
        var tank = GameObject.Find("Tank");
        var tankController = tank.GetComponent<TankController>();
        _canvasController.Init(tankController);

        tankController.OnDeath += () => { if(_restartButton != null) _restartButton.gameObject.SetActive(true); };
        _restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}