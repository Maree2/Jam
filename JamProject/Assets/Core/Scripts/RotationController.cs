using UnityEngine;
using System.Collections;

public class RotationController : MonoBehaviour
{
    public GameObject worldChild;
    public float maxAccelForward;
    public float minAccelForward;
    public float deAccelForward;
    public float maxAccelHorizontal;
    private float accelForward;
    private float accelHorizontal;
    private Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        accelForward = minAccelForward;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Wind"))
        {
            accelForward += maxAccelForward * Time.deltaTime;
        }
        else if (accelForward > minAccelForward)
        {
            accelForward -= deAccelForward * Time.deltaTime;
        }
        accelForward = Mathf.Clamp(accelForward, minAccelForward, maxAccelForward);
        velocity.x = accelForward * Time.deltaTime * -1;
        worldChild.transform.Rotate(velocity);

        accelHorizontal = Input.GetAxis("Horizontal") * maxAccelHorizontal;
        accelHorizontal = Mathf.Clamp(accelHorizontal, -maxAccelHorizontal, maxAccelHorizontal);
        transform.rotation = Quaternion.Euler(0f, accelHorizontal, 0f);
    }
}
