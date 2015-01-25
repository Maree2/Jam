using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    Transform center;

    [SerializeField]
    float distanceFromCenter;

    [SerializeField]
    [Range(0, 360)]
    float longitudeAngle, latitudeAngle;
    
    [SerializeField]
    [Range(-180, 180)]
    float lookAngle;

    
    [SerializeField]
    Vector2 movementDirection, movementSpeed;

    // Awake is called when the script instance is being loaded (Since v1.0)
    public void Awake()
    {
        movementDirection.Normalize();
        
    }


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 temp = Vector3.zero;

        if (h < 0)
        {
            temp += transform.TransformDirection(-transform.right);
        }
        else if (h > 0)
        {
            temp += transform.TransformDirection(transform.right);
        }

        /*if (v < 0)
        {
            temp += transform.TransformDirection(-transform.up);
        }
        else if (v > 0)
        {
            temp += transform.TransformDirection(transform.up);
        }*/

        temp = CartesianToSpherical(temp);

        Vector2 newDir = new Vector2(temp.y, temp.z);
        newDir.Normalize();

        /*if (newDir.SqrMagnitude() != 0)
            movementDirection = newDir;*/

        /*
        int h = Mathf.RoundToInt(h1);
        int v = Mathf.RoundToInt(v1);

        Vector2 newDir = new Vector2(h, v);
        */

        //movementDirection.y += h;

        //Debug.Log("H: " + h + " | V: " + v);

        //h = latitudeSpeed;
        //v = longitudeSpeed;



        //latitudeAngle += latitudePerSecond * Time.deltaTime * h;
        //longitudeAngle += longitudePerSecond * Time.deltaTime * v;

        longitudeAngle += movementDirection.x * movementSpeed.x * Time.deltaTime;
        latitudeAngle += movementDirection.y * movementSpeed.y * Time.deltaTime;

        longitudeAngle %= 360f;
        latitudeAngle %= 360f;

        transform.position = CalculatePosition(longitudeAngle, latitudeAngle);

        transform.LookAt(center, transform.up);
        transform.Rotate(Vector3.left, lookAngle);
        
        /*Vector3 relativeUp = center.TransformDirection(Vector3.left);
        Vector3 relativePos = center.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos, relativeUp);*/
	}

    Vector3 CalculatePosition(float longitude, float latitude)
    {
        //angleInX -= 360f;
        longitude *= Mathf.Deg2Rad;
        latitude *= Mathf.Deg2Rad;

        float x = center.position.x + Mathf.Cos(longitude) * distanceFromCenter * Mathf.Cos(latitude);
        float y = center.position.y + Mathf.Sin(longitude) * distanceFromCenter;
        float z = center.position.z + Mathf.Cos(longitude) * distanceFromCenter * Mathf.Sin(latitude);

        return new Vector3(x, y, z);
    }


    public Vector3 CartesianToSpherical(Vector3 cartCoords) //, out float outRadius, out float outPolar, out float outElevation)
    {
        Vector3 result = Vector3.zero;

        if (cartCoords.x == 0)
            cartCoords.x = Mathf.Epsilon;

        result.x = Mathf.Sqrt((cartCoords.x * cartCoords.x)
                        + (cartCoords.y * cartCoords.y)
                        + (cartCoords.z * cartCoords.z));
        result.y = Mathf.Atan(cartCoords.z / cartCoords.x);
        if (cartCoords.x < 0)
            result.y += Mathf.PI;
        result.z = Mathf.Asin(cartCoords.y / result.x);

        return result;
    }

}
