using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class InputController : MonoBehaviour
{
    private TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _tankController.MoveTowards();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _tankController.MoveBackward();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _tankController.RotateRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _tankController.RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _tankController.Fire();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _tankController.NextWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _tankController.PreviousWeapon();
        }
    }
}