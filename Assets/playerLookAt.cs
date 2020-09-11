using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLookAt : MonoBehaviour
{

    public Animator anim;
    public Camera cam;
    public float rotationSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, camForward, rotationSpeed * Time.deltaTime);

    }

    // Update is called once per frame
    void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(1f, 1f, 1f, .5f, .5f);

        Ray lookAtRay = new Ray(transform.position, cam.transform.forward);
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));

    }
}
