using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    PlanetController planet;

    //[SerializeField]
    //Transform center;

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
    
    [SerializeField]
    [Range(1f, 10f)]
    float speedMod = 1f;

    bool usingSpeedMod = false;

    [SerializeField]
    float lowestSpeed, currentSpeed, maxSpeed;

    [SerializeField]
    float speedAcc = 5f, speedDecc = 5f;

    [SerializeField]
    ParticleSystem fireParticles;

    float bongoCooldownTime = 0f, fireCooldownTime = 0f, waterCooldownTime = 0f;
    
    float index = 0f;



    // Awake is called when the script instance is being loaded (Since v1.0)
    public void Awake()
    {
        movementDirection.Normalize();        
    }


	// Use this for initialization
	void Start () 
    {
        fireParticles.Stop();
	}

    void RegulateMovementSpeed()
    {
        if (usingSpeedMod)
        {
            currentSpeed += speedAcc * Time.deltaTime;
        }
        else
        {
            currentSpeed -= speedDecc * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, lowestSpeed, maxSpeed);
    }

    void ActionUpdate()
    {

        // EARTH /////////
        if (bongoCooldownTime > 0f)
        {
            bongoCooldownTime -= Time.deltaTime;

            if (bongoCooldownTime <= 0f)
            {
                planet.QuitBong();
                bongoCooldownTime = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.U)) // GetMouseButtonDown(0))
        {
            planet.Bong(Random.RandomRange(0.2f, 0.45f));
            
            bongoCooldownTime = 2f;
        }
        else if (Input.GetKeyUp(KeyCode.U)) //Input.GetMouseButtonUp(0))
        {
            //ResetPlanet();
            //planet.Bong(0);
        }


        // AIR /////////
        if (Input.GetKey(KeyCode.P))
        {
            usingSpeedMod = true;

        }
        else
        {
            usingSpeedMod = false;
            index = 0f;
        }


        // Water /////////
        if (waterCooldownTime > 0f)
        {
            waterCooldownTime -= Time.deltaTime;

            if (waterCooldownTime <= 0f)
            {
                planet.QuitLife();
                waterCooldownTime = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) // GetMouseButtonDown(0))
        {
            planet.Life();
            waterCooldownTime = 1f;
            //bongoCooldownTime = 1f;
        }
        else if (Input.GetKeyUp(KeyCode.O)) //Input.GetMouseButtonUp(0))
        {
                //ResetPlanet();
                //planet.Bong(0);
            //planet.QuitLife();
        }



        // Fire /////////
        if (fireCooldownTime > 0f)
        {
            fireCooldownTime -= Time.deltaTime;

            if (fireCooldownTime <= 0f)
            {
                fireCooldownTime = 0f;
                fireParticles.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) // GetMouseButtonDown(0))
        {
            fireParticles.Play();
            fireCooldownTime = 2f;
        }
        else if (Input.GetKeyUp(KeyCode.I)) //Input.GetMouseButtonUp(0))
        {
        }

    }

    void MovementUpdate()
    {
        RegulateMovementSpeed();

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

        longitudeAngle += movementDirection.x * currentSpeed * Time.deltaTime;// *(usingSpeedMod ? speedMod : 1f); //movementSpeed.x
        latitudeAngle += movementDirection.y * currentSpeed * Time.deltaTime;// *(usingSpeedMod ? speedMod : 1f);

        longitudeAngle %= 360f;
        latitudeAngle %= 360f;

        transform.position = CalculatePosition(longitudeAngle, latitudeAngle);

        transform.LookAt(planet.transform.position, transform.up);
        transform.Rotate(Vector3.left, lookAngle);

        /*Vector3 relativeUp = center.TransformDirection(Vector3.left);
        Vector3 relativePos = center.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos, relativeUp);*/
    }
	
	// Update is called once per frame
	void Update () 
    {
        ActionUpdate();
        MovementUpdate();
	}

    Vector3 CalculatePosition(float longitude, float latitude)
    {
        //angleInX -= 360f;
        longitude *= Mathf.Deg2Rad;
        latitude *= Mathf.Deg2Rad;

        Vector3 center = planet.transform.position;
        float d = distanceFromCenter;

        if (false) //usingSpeedMod)
        {
            index += Time.deltaTime;
            d = distanceFromCenter + Mathf.Abs(distanceFromCenter * 0.125f * Mathf.Sin(distanceFromCenter * 0.1f * index));
     
            //d = Mathf.Lerp(d, distanceFromCenter * 1.5f, Time.deltaTime);
        }

        float x = center.x + Mathf.Cos(longitude) * d * Mathf.Cos(latitude);
        float y = center.y + Mathf.Sin(longitude) * d;
        float z = center.z + Mathf.Cos(longitude) * d * Mathf.Sin(latitude);

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
