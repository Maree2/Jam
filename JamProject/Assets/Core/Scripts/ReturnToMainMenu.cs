using UnityEngine;
using System.Collections;

public class ReturnToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("MenuLevel");
        }
	}
}
