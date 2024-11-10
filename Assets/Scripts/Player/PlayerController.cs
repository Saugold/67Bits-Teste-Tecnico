using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 300;

    
    private Rigidbody rb;
    private Animator animator;

    Vector2 movement;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void SetMoviment(InputAction.CallbackContext value)
    {
        movement = value.ReadValue<Vector2>();
    }

    public void SetPunch(InputAction.CallbackContext value)
    {

    }
    private void Update()
    {
        if (movement != Vector2.zero)
        {
            Debug.Log("IsMoving");
        }
        else
        {
            Debug.Log("IsNOTMoving");
        }
        animator.SetBool("isRunning", movement != Vector2.zero);
    }
    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);


        rb.AddForce(moveDirection * moveSpeed);
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    
}
