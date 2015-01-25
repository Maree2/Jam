using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    [SerializeField]
    Transform lookat;

    
    //[SerializeField]
    //float longitudePerSecond = 0f, latitudePerSecond = 0f;

    //[SerializeField]
    //float longitudeSpeed = 0f, latitudeSpeed = 0f;



    void Awake()
    {
        Camera.main.transform.LookAt(lookat);

        //Camera.main.transform.LookAt(transform.position + transform.up);
    }

	// Use this for initialization
	void Start () 
    {
	
	}

	
	// Update is called once per frame
	void Update () 
    {

        //transform.Rotate(
        //transform.Rotate(Vector3.up, 20 * Time.deltaTime, Space.Self);

        //Camera.main.transform.LookAt(transform.position + transform.up, transform.position);
        
        //Camera.main.transform.LookAt(center, transform.up);
        //Camera.main.transform.Rotate(Vector3.left, lookAngle);

	}


}
