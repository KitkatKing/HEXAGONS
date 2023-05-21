using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{

    [Header("Player Stuffs")]
    private bool isActive;

    private LayerMask gravMask;
    public Transform cam;
    public Rigidbody rb;

    public Vector2 PlayerMouseInput;
    public Vector2 PlayerMovementInput;

    public int forc;
    public int fallVelocity;


    // Start is called before the first frame update
    void Start()
    {

        gravMask = LayerMask.GetMask("FocalGround", "Ground", "MovingGround", "Default");
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        PlayerCam();
        PlayerGravity();

    }

    private void FixedUpdate()
    {
        PlayerMovementStandard();
    }

    public void PlayerCam()
    {
        transform.Rotate(0, PlayerMouseInput.x, 0, Space.Self);

    }

   

    public void GetPlayerInput()
    {
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        PlayerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }


    public void PlayerMovementStandard()
    {
        rb.AddForce(transform.forward * PlayerMovementInput.y * forc + transform.right * PlayerMovementInput.x * forc);

        if (Physics.CheckSphere(transform.position - transform.up * 1f, 0.05f, gravMask) && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 1, 0) * 7500);
        }

    }

    public void PlayerGravity()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        RaycastHit hit;


        if (Physics.CheckSphere(transform.position - transform.up * 1f, 0.05f, gravMask))
        {
            fallVelocity = 0;
        }
        else
        {
            fallVelocity = Mathf.Clamp(fallVelocity + 1, 0, 60);

        }

        rb.AddForce(new Vector3(0, -1, 0) * fallVelocity);
    }


}
