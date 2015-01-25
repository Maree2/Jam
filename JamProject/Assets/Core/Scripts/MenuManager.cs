using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("Earth"))
        {
            Application.LoadLevel("SimpleJamLevel");
        }
        else if (Input.GetButtonDown("Wind"))
        {
            Application.LoadLevel("JamLevel");
        }
        else if (Input.GetButtonDown("Fire"))
        {
            Application.Quit();
        }
        else if (Input.GetButtonDown("Water"))
        {
            Application.LoadLevel("CreditsLevel");
        }
	}
}
