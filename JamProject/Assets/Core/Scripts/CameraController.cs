using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    [SerializeField]
    Transform center, lookat;

    [SerializeField]
    float distanceFromCenter;

    [SerializeField]
    [Range(0, 360)]
    float angleX, angleY, lookAngle;

    void Awake()
    { 
        
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = CalculatePosition(angleX, angleY);
        Camera.main.transform.LookAt(center, transform.up);
        Camera.main.transform.Rotate(Vector3.right, -lookAngle);
	}

    Vector3 CalculatePosition(float angleInX, float angleInY)
    {
        //angleInX -= 360f;
        angleInX *= Mathf.Deg2Rad;
        angleInY *= Mathf.Deg2Rad;

        float x = center.position.x + Mathf.Sin(angleInX) * distanceFromCenter * Mathf.Cos(angleInY);
        float y = center.position.y + Mathf.Cos(angleInX) * distanceFromCenter;
        float z = center.position.z + Mathf.Sin(angleInX) * distanceFromCenter * Mathf.Sin(angleInY);

        return new Vector3(x, y, z);
    }
}
