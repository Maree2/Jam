using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{

    public Text chomiak;
    public Text palacios;
    public Text ordaz;
    public Text pulido;

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        chomiak.enabled = Input.GetButton("Earth");
        palacios.enabled = Input.GetButton("Fire");
        ordaz.enabled = Input.GetButton("Water");
        pulido.enabled = Input.GetButton("Wind");
    }
}
