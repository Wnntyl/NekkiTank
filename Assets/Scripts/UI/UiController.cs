using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private MainCanvasController _canvasController;

    private void Start()
    {
        var tank = GameObject.Find("Tank");
        var tankController = tank.GetComponent<TankController>();
        _canvasController.Init(tankController);
    }
}