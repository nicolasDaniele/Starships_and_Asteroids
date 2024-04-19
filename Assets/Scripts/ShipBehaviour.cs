using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ShipBehaviour : MonoBehaviour
{
    public static Action OnShoot;
    [SerializeField] private float speed = 10f;
    private Vector2 moveVector;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Gameplay.Enable();
        playerInputActions.Gameplay.Shoot.started += Shoot;
    }

    private void Update()
    {
        moveVector = playerInputActions.Gameplay.Move.ReadValue<Vector2>();
        transform.position += new Vector3(moveVector.x * speed, moveVector.y * speed, 0f) * Time.deltaTime;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        OnShoot?.Invoke();
    }
}
