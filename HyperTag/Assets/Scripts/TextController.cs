using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    private string message;
    private TextMeshProUGUI textMesh;
    public SwipeDetection swipeObj;


    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void setText(string msg)
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        textMesh.text = msg;
    }
}
