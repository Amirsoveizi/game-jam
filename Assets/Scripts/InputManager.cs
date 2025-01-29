using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static Vector2 Direction;

    private InputAction _moveAction;
    private InputAction _dirAction;


    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        _moveAction = playerInput.actions["Move"];
        _dirAction = playerInput.actions["Dir"];

    }

    void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        Direction = _dirAction.ReadValue<Vector2>();
    }
}
