using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private float _width;
    private float _height;
    private Vector3 position;
    public float _moveSpeed;
    private Vector3 _targetPosition;

    public UnityEvent<string> OnJump = new UnityEvent<string>();

    private void Awake()
    {
        //OnJump.AddListener((x) => { Debug.Log });
        _width = (float)Screen.width;
        _height = (float)Screen.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.position;
        _moveSpeed = 10;
        _moveSpeed = 11;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
    }

    public void Move(Vector3 moveDirection)
    {
        OnJump.Invoke(moveDirection.ToString());
        _targetPosition += moveDirection;
        _targetPosition *= _moveSpeed;
    }

    public void setTargetPosition(float x, float y, float z)
    {
        _targetPosition.x = x;
        _targetPosition.y = y;
        _targetPosition.z = z;
    }
}
