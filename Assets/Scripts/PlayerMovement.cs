using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerCharacterBase controller = null;
    [SerializeField] private PlayerInputActions playerInput = null;
    [SerializeField] private NavMeshAgent meshAgent = null;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;


    private Vector2 previousInput;
    private Controls controls;  

    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
        enabled = true;
        Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Player.Move.canceled += ctx => ResetMovement();
  
    }


    #region Server

    #endregion

    #region Client  
    [ClientCallback]
private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    [ClientCallback]

    private void Update()
    {
        Move();
    }

    [Client]
    private void SetMovement(Vector2 movement) => previousInput = movement;

    [Client]
    private void ResetMovement() => previousInput = Vector2.zero;


    [Client]
    private void Move()
    {
        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;
        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

       // meshAgent.Move(movement * movementSpeed * Time.deltaTime);
        meshAgent.SetDestination(controller.transform.position + movement);
    }
    #endregion
}
