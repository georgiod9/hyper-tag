using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    public Player player;
    private Vector2 startPosition;
    public int pixelThreshold = 20;
    private bool touchDown;
    public float width;
    public float height;
    private string message;
    private string message2;
    private string message3;

    public TextController textController;
    public TextController textController2;
    public TextController textController3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        touchDown = false;
        width = (float)Screen.width;
        height = (float)Screen.height;
        textController = GameObject.Find("x_value").GetComponent<TextController>();
        textController2 = GameObject.Find("start_position").GetComponent<TextController>();
        //textController3 = GameObject.Find("direction").GetComponent<TextController>();

        textController.setText(Screen.width.ToString());

        message2 = "TESTING";
        textController2.setText(message2);

        //message3 = "no direction";
        //textController3.setText(message3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            message = "X: " + touch.position.x.ToString() + " Y: 0" + " Z: " + touch.position.y.ToString();
            //textController.setText(message);

            if ((int)touch.position.x <= Screen.width / 2)
            {

                if (touchDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    startPosition = Input.touches[0].position;
                    touchDown = true;

                    message2 = "X: " + startPosition.x.ToString() + "\nY: 0" + "\nZ: " + startPosition.y.ToString();
                    //textController2.setText(message2);

                }

                if (touchDown)
                {
                    if (touch.position.y >= startPosition.y + pixelThreshold) //Input.touches[0].position.y >= startPosition.y + pixelThreshold
                    {
                        touchDown = false;
                        Debug.Log("Swipe up");
                        player.Move(Vector3.up);
                        //textController.setText("Moving Up");
                    }

                    if (Input.touches[0].position.x <= startPosition.x - pixelThreshold)
                    {
                        touchDown = false;
                        Debug.Log("Swipe left");
                        player.Move(Vector3.left);
                    }

                    if (Input.touches[0].position.x >= startPosition.x + pixelThreshold)
                    {
                        touchDown = false;
                        Debug.Log("Swipe right");
                        player.Move(Vector3.right);
                    }

                    if (Input.touches[0].position.y <= startPosition.y - pixelThreshold)
                    {
                        touchDown = false;
                        Debug.Log("Swipe down");
                        player.Move(Vector3.down);
                    }
                }
                else
                {
                    player.setTargetPosition(transform.position.x, transform.position.y, transform.position.z);
                }
            }
        }        
    }

    public bool getTouchState()
    {
        return touchDown;
    }
}
