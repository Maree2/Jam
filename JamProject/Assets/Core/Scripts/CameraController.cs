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
    float longitudeAngle, latitudeAngle, lookAngle;

    [SerializeField]
    float longitudePerSecond = 0f, latitudePerSecond = 0f;

    [SerializeField]
    float longitudeSpeed = 0f, latitudeSpeed = 0f;

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
        int h = Mathf.RoundToInt( Input.GetAxis("Horizontal"));
        int v = Mathf.RoundToInt(Input.GetAxis("Vertical"));

        Debug.Log("H: " + h + " | V: " + v);

        //h = latitudeSpeed;
        //v = longitudeSpeed;

        latitudeAngle += latitudePerSecond * Time.deltaTime * h;
        longitudeAngle += longitudePerSecond * Time.deltaTime * v;

        transform.position = CalculatePosition(longitudeAngle, latitudeAngle);

        //transform.Rotate(Vector3.up, 20 * Time.deltaTime, Space.Self);
        
        //Camera.main.transform.LookAt(center, transform.up);
        //Camera.main.transform.Rotate(Vector3.right, -lookAngle);        
	}

    Vector3 CalculatePosition(float longitude, float latitude)
    {
        //angleInX -= 360f;
        longitude *= Mathf.Deg2Rad;
        latitude *= Mathf.Deg2Rad;
        
        float x = center.position.x + Mathf.Cos(longitude) * distanceFromCenter * Mathf.Cos(latitude);
        float y = center.position.y + Mathf.Sin(longitude) * distanceFromCenter;
        float z = center.position.z + Mathf.Sin(latitude) * distanceFromCenter * Mathf.Cos(longitude);

        return new Vector3(x, y, z);
    }
}
