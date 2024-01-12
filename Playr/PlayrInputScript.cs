using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayrInputScript : MonoBehaviour
{
    public bool CanDodge = true;
    private Rigidbody rbody;
    private PlayerInput playerInput;
    private PlayrInputActions PlayrInputactions;
    Vector2 inputVector;
    [SerializeField] public bool Alive = true;

    private float distance = 30;

    [SerializeField] private Transform target;
    [SerializeField] private CinemachineVirtualCamera virtualCameraR, virtualCameraL;

    private void Awake()
    {

        StartCoroutine(DodgeCD());

        virtualCameraR.enabled = true; virtualCameraL.enabled = false;
        rbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        PlayrInputactions = new PlayrInputActions();
        PlayrInputactions.PlayrMoves.Enable();
    }

    private void FixedUpdate()
    {
        inputVector = PlayrInputactions.PlayrMoves.Move.ReadValue<Vector2>();

        this.transform.LookAt(target.position);
        transform.position = (transform.position - target.position).normalized * distance + target.position;

        MoveHorizontal();
        MoveVertical();
    }

    public void AFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("fire"); 
        }
    }

    public void ADodge(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            if (CanDodge)
            {
                float HSpeed = 100f;
                float VSpeed = 75f;

                rbody.AddForce(transform.right * inputVector.x * HSpeed, ForceMode.Impulse);

                if (inputVector.y > 0) 
                { 
                    rbody.AddForce(transform.up * inputVector.y * VSpeed, ForceMode.Impulse);
                }

                CanDodge = false;
            }

        }
    }

    public void ASpecialMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("special");
        }
    }

    public void MoveHorizontal() 
    {
        float speed = 75f;
        rbody.AddForce(this.transform.right * inputVector.x * speed, ForceMode.Force);

        if(inputVector.x > 0)
        {
            virtualCameraR.enabled = true;
            virtualCameraL.enabled = false;
        }
        else if(inputVector.x < 0) 
        {
            virtualCameraL.enabled = true;
            virtualCameraR.enabled = false;
        }
    }
    public void MoveVertical()
    {
        float speed = 55f;
        rbody.AddForce(this.transform.up * inputVector.y * speed, ForceMode.Force); 
        
    }

    IEnumerator DodgeCD()
    {
        while (!CanDodge)
        {
            yield return new WaitForSeconds(1);
            CanDodge = true; 
        }        
    }
}
