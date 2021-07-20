using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private double _speed;
    private Vector3 _position;
    private float _width;
    private float _height;
    public GameObject obj;
    public Vector2 _startPosition;
    public Vector2 _endPosition;
    public Vector2 _direction;
    public Vector2 _currentPosition;
    public bool _touchDown;
    public Vector2 _difference;
    public string message;
    public string message2;
    public string message3;
    public TextController textController;
    public TextController textController2;
    public TextController textController3;

    public float radius;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        _touchDown = false;
        _difference = new Vector2(0, 0);
        rotation = Vector3.zero;

        _speed = 0.002;
        _width = (float)Screen.width;
        _height = (float)Screen.height;

        textController = GameObject.Find("x_value").GetComponent<TextController>();
        textController2 = GameObject.Find("start_position").GetComponent<TextController>();
        textController3 = GameObject.Find("direction").GetComponent<TextController>();
        radius = _height / 4;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*(float)_speed, 0, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical")*(float)_speed);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _currentPosition = touch.position;

            message = "X: " + touch.position.x.ToString() + "\nY: 0" + "\nZ: " + touch.position.y.ToString();
            textController.setText(message);

            //if ((int)touch.position.x <= Screen.width / 2)
            //{
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startPosition = touch.position;
                        _touchDown = true;
                        message3 = "touch started";
                        textController3.setText(message3);
                        break;

                    case TouchPhase.Moved:
                        message3 = "touch moving";
                        textController3.setText(message3);

                        Vector2 difference = touch.position - _startPosition;

                        if (difference.magnitude <= radius)
                        {
                            _direction = difference;
                        }
                        else
                        {
                            //if (difference.x > 0) _direction.x = difference.x - (_direction.x - _height / 4);
                            //else if (difference.x < 0) _direction.x = difference.x + (_direction.x + _height / 4);

                            //if (difference.y > 0) _direction.y = difference.y - (_direction.y - _height / 4);
                            //else if (difference.y < 0) _direction.y = difference.y + (_direction.y + _height / 4);

                            //_direction = difference;
                        }

                        message2 = "X: " + _direction.x.ToString() + "\nY: 0" + "\nZ: " + _direction.y.ToString();
                        textController2.setText(message2);

                        if (_touchDown)
                        {
                            _difference.x = _currentPosition.x - _startPosition.x;
                            _difference.y = _currentPosition.y - _startPosition.y;
                        }

                        

                        break;

                    case TouchPhase.Ended:
                        _touchDown = false;
                        _endPosition = touch.position;
                        message3 = "touch ended";
                        textController3.setText(message3);

                        break;
                }
            //}

            if ((int)touch.position.x <= Screen.width / 2)
            {
                transform.Translate(_difference.x * (float)_speed, 0, _difference.y * (float)_speed);
            }

            if ((int)touch.position.x > Screen.width / 2)
            {

                if (touch.position.x > _startPosition.x) // rotating right
                {
                    rotation = Vector3.up;
                }
                else if (touch.position.x < _startPosition.x) //rotating left
                {
                    rotation = Vector3.down;
                }

                transform.Rotate(rotation);
            }




        }

    }
}
