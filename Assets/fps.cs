using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{
    public float xRot = 5;
    public float yRot = 5;
    public float minY = -20;
    public float maxY = 20;

    private float xRotation;
    private float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X") * xRot;
        float v = Input.GetAxis("Mouse Y") * yRot;



        Vector3 temp = transform.localEulerAngles;
        xRotation = Mathf.Clamp(xRotation - v * Time.deltaTime, minY, maxY);
        yRotation = yRotation + h * Time.deltaTime;
        temp.x = xRotation;
        temp.y = yRotation;
        temp.z = 0;
        transform.localEulerAngles = temp;
    }
}
