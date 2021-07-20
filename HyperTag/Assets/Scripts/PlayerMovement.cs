using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;
    private float playerSpeed = 0.3f;

    public float gravity = -9.81f;
    public float playerMass = 2;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    Vector3 move;
    Vector2 startPosition;
    float pixelThreshold = 20;
    float xRotation = 0f;
    float yRotation = 0f;

    public Transform groundCheck;
    public Transform cloudCheck;
    public LayerMask groundMask;

    public float scrollSensitivity;
    public Transform playerBody;

    public TextController textController;
    public Button jumpButton;


    // Start is called before the first frame update
    void Start()
    {
        jumpButton.onClick.AddListener(() => OnJumpButtonClick());

        scrollSensitivity = 1f;
        move = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Free fall physics
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime * playerMass);

        //Check if player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Reset player Y position when touches the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.touchCount > 0)
        {
            //Get input touches
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    startPosition = touch.position;
                    break;

                case TouchPhase.Moved:

                    Vector3 touchDelta = touch.position - startPosition;

                    foreach (Touch t in Input.touches)
                    {
                        //move around
                        if (t.position.x < Screen.width / 2 && (touchDelta.magnitude - pixelThreshold > 0))
                        {
                            float x = touchDelta.x;
                            float z = touchDelta.y;
                            move.y = transform.position.y;

                            move = transform.right * x + transform.forward * z;
                        }

                    }
                    break;

                case TouchPhase.Ended:
                    move = Vector3.zero;
                    break;
            }
        }

        //Move player on X and Z axiis
        playerController.Move(playerSpeed * Time.deltaTime * move);

    }

    //On click listener for jump button
    public void OnJumpButtonClick()
    {
        if (isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            textController.setText("JUMPING");
        }

    }
}
