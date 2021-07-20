using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public CharacterController playerController;
    private float rotationSpeed;

    Vector2 startPosition;
    float pixelThreshold = 20;
    float xRotation = 0f;
    float yRotation = 0f;

    public float scrollSensitivity;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        scrollSensitivity = 1f;
        rotationSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
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
                        //look around
                        if (t.position.x > Screen.width / 2 && (touchDelta.magnitude - pixelThreshold > 0))
                        {
                            xRotation -= touchDelta.y * Time.deltaTime * rotationSpeed;
                            yRotation += touchDelta.x * Time.deltaTime * rotationSpeed;
      
                            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    break;
            }
        }

        //Rotate camera when player looks around
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
