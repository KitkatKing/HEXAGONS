  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{


    [Header("Player ClientButtons")]
    public bool pressingCtrl;
    public bool pressingShift;
    public bool pressingSpace;
    public bool pressCtrl;
    public bool pressShift;
    public bool pressSpace;
    public Vector2 PlayerMouseInput;
    public Vector2 PlayerMovementInput;
    private float mouseVert;

    [Header("Ajustable Movement Values")]

    public float movementSpeed;
    public float jumpForce;
    public float generalSpeedMultiplier;
    public float fallAcceleration;
    public float fallMax;

    [Header("Player States")]

    public bool isMotionPressed;
    public bool isSprinting;
    public bool isCrouched;
    
    public bool isSliding;
    public bool isGrounded;
    public bool isMenuOpen;


    [Header("Movement Data")]

    public float slideForceCurrent;
    public float fallVelocity;
    public int sprintTimer;
    public Vector3 velocity;


    private LayerMask gravMask;

    public Transform cam;

    public Rigidbody rb;

    public int NetID;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        generalSpeedMultiplier = 1;
        gravMask = LayerMask.GetMask("FocalGround", "Ground", "MovingGround", "Default");
        sprintTimer = 0;
        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    
    void Update()
    {

        UpdateClientSide();

        UpdateServerSide();
    }


   
    void UpdateClientSide()
    {


        pressingCtrl = Input.GetKey(KeyCode.LeftControl);
        pressCtrl = Input.GetKeyDown(KeyCode.LeftControl);
        pressingShift = Input.GetKey(KeyCode.LeftShift);
        pressShift = Input.GetKeyDown(KeyCode.LeftShift);
        pressingSpace = Input.GetKey(KeyCode.Space);
        pressSpace = Input.GetKeyDown(KeyCode.Space);

        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        PlayerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        Camera.main.transform.position = cam.position;

        Camera.main.transform.rotation = cam.rotation;

    }

   
    void UpdateServerSide()
    {

        FirstPersonCameraMovement();

        if (pressSpace && Physics.CheckSphere(transform.position + transform.forward * 0.8f, 0.3f, gravMask) && isSliding == false)
        {
            rb.AddForce(rb.velocity - transform.forward * jumpForce, ForceMode.Impulse);
        }
        else if (pressSpace && Physics.CheckSphere(transform.position + transform.forward * 0.8f, 0.3f, gravMask) && isSliding == true)
        {
            isSliding = false;
            sprintTimer = 30;

            rb.AddForce(rb.velocity + ((-transform.forward * 3 + transform.up) / 4) * jumpForce * 2.5f * (slideForceCurrent / 2500), ForceMode.Impulse);

        }

        FirstPersonMovementModeShift();

        if (PlayerMovementInput.x < 0.1f && PlayerMovementInput.x > -0.1f && PlayerMovementInput.y < 0.1f && PlayerMovementInput.y > -0.1f)
        {
            isMotionPressed = false;
        }
        else
        {
            isMotionPressed = true;
        }


    }


    void FixedUpdate()
    {

        FirstPersonPlayerDragShift();

        FirstPersonPlayerGravity();

        FirstPersonPlayerMovement();

        FirstPersonPlayerCrouchSlide();

    }







    public void FirstPersonMovementModeShift()
    {
        LayerMask physicalMask = LayerMask.GetMask("FocalGround", "Ground", "default");

        if (pressingCtrl == true)
        {
            Debug.Log("ctrl");
        }

        if (pressingCtrl == true && isSprinting == true)
        {

            isSliding = true;
            isCrouched = false;
            slideForceCurrent = 2500;

        }
        if (pressingCtrl == true && isSliding == false)
        {
            isCrouched = true;
        }
        if (pressingCtrl == false && Physics.CheckSphere(transform.position - transform.forward * 0.75f, 0.5f, physicalMask) == false)
        {
            isCrouched = false;

        }

    }

    public void FirstPersonCameraMovement()
    {

        if (isMenuOpen == false)
        {


            if (isSliding == false)
            {
                transform.Rotate(0, 0, -PlayerMouseInput.x, Space.Self);

                mouseVert -= PlayerMouseInput.y;
                mouseVert = Mathf.Clamp(mouseVert, -180, 0);
                cam.transform.localEulerAngles = new Vector3(mouseVert, 0, 0);

            }
            else
            {
                mouseVert -= PlayerMouseInput.y;
                mouseVert = Mathf.Clamp(mouseVert, -180, 0);
                cam.localEulerAngles = new Vector3(mouseVert, 0, 0);

                transform.Rotate(0, 0, -PlayerMouseInput.x * 0.25f, Space.Self);

            }

        }

        if(isCrouched == true || isSliding == true)
        {
            cam.localPosition = new Vector3(0, 0, Mathf.Lerp(cam.localPosition.z, 0, 0.5f));
        }
        else
        {
            cam.localPosition = new Vector3(0, 0, Mathf.Lerp(cam.localPosition.z, -0.7f, 0.5f));
        }


    }

    public void FirstPersonPlayerMovement()
    {
        sprintTimer = Mathf.Clamp(sprintTimer - 1, 0, 30);

        Rigidbody rb = GetComponent<Rigidbody>();

        velocity = rb.velocity;

        if (isMenuOpen == false)
        {

            if (isGrounded == false)
            {

                if (PlayerMovementInput.x != 0)
                {

                    rb.AddForce(transform.right * PlayerMovementInput.x * movementSpeed * generalSpeedMultiplier * 0.85f);

                }

                if (PlayerMovementInput.y != 0)
                {

                    if (PlayerMovementInput.y > 0 && pressingShift && isSliding == false && isCrouched == false && sprintTimer == 0)
                    {
                        rb.AddForce(transform.up * PlayerMovementInput.y * movementSpeed * 2f * generalSpeedMultiplier * 0.85f);
                    }
                    else
                    {
                        rb.AddForce(transform.up * PlayerMovementInput.y * movementSpeed * generalSpeedMultiplier * 0.85f);
                    }

                }
            }
            else
            {

                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, 1.1f, gravMask))
                {


                    if (PlayerMovementInput.x != 0)
                    {

                        rb.AddForce(transform.right * PlayerMovementInput.x * movementSpeed * generalSpeedMultiplier);

                    }

                    if (PlayerMovementInput.y > 0 && pressingShift && isSliding == false && isCrouched == false && sprintTimer == 0)
                    {

                        isSprinting = true;
                        rb.AddForce(transform.up * PlayerMovementInput.y * movementSpeed * 2f * generalSpeedMultiplier);
                    }
                    else
                    {

                        isSprinting = false;
                        rb.AddForce(transform.up * PlayerMovementInput.y * movementSpeed * generalSpeedMultiplier);
                    }




                    Debug.DrawRay(transform.position, hit.normal - -transform.forward, Color.red);

                }

            }


        }

    }

    public void FirstPersonPlayerGravity()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        RaycastHit hit;

            //Debug.DrawLine(transform.position + transform.forward * 0.7f, transform.position);

            if (Physics.CheckSphere(transform.position + transform.forward * 1f, 0.05f, gravMask))
            {
                fallVelocity = 0f;
                isGrounded = true;
            }
            else
            {
                fallVelocity = Mathf.Clamp(fallVelocity + fallAcceleration, 0, fallMax);
                isGrounded = false;
            }

            rb.AddForce(new Vector3(0, -1, 0) * fallVelocity);
    }

    public void FirstPersonPlayerCrouchSlide()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        CapsuleCollider walkingCollider = GetComponent<CapsuleCollider>();


        if (isCrouched == true)
        {
            generalSpeedMultiplier = 0.75f;

            walkingCollider.center = new Vector3(0, 0, 0.25f);

            walkingCollider.height = 1.5f;

            
        }
        else
        if(isSliding == true)
        {
            generalSpeedMultiplier = 0f;

            walkingCollider.center = new Vector3(0, 0, 0.25f);

            walkingCollider.height = 1.5f;




            float upDist = Vector3.Distance(transform.up, transform.up);
            float forDist = Vector3.Distance(-transform.forward, transform.up);

            float slideSpeedLossMultiplier = Mathf.Clamp( 2500 / slideForceCurrent, 1, 10) * 3;



            if (isGrounded == true)
            {
                if (upDist > 0.1)
                {
                    if (forDist > 1.4142)
                    {

                        slideForceCurrent = Mathf.Clamp(slideForceCurrent + 25 * slideSpeedLossMultiplier, 0, 2500);

                    }
                    if (forDist < 1.4142)
                    {

                        slideForceCurrent = Mathf.Clamp(slideForceCurrent - 10 * slideSpeedLossMultiplier, 0, 2500);

                    }
                }

                slideForceCurrent = Mathf.Clamp(slideForceCurrent - 5 * slideSpeedLossMultiplier, 0, 2500);
            }
            else
            {
                slideForceCurrent = Mathf.Clamp(slideForceCurrent - 2.5f * slideSpeedLossMultiplier, 500, 2500) * 0.85f;
            }
            

            if(isGrounded == true)
            {
                rb.AddForce(transform.up * slideForceCurrent * 1.5f);
            }
            else
            {
                rb.AddForce(transform.up * PlayerMovementInput.y * slideForceCurrent * 1.5f);
            }
            


            if(slideForceCurrent <= 0)
            {
                isSliding = false;
                sprintTimer = 30;
            }

            if (Input.GetKey(KeyCode.LeftControl) == false)
            {
                isSliding = false;
                isCrouched = true;
            }

        }
        else
        {
            generalSpeedMultiplier = 1f;

            walkingCollider.center = new Vector3(0, 0, 0f);

            walkingCollider.height = 2f;

            
        }


    }

    public void FirstPersonPlayerDragShift()
    {

        if (isGrounded == true)
        {
            rb.velocity = rb.velocity * 0.85f;
        }
        else
        {
            rb.velocity = rb.velocity * 0.90f;
        }
    }    



}
