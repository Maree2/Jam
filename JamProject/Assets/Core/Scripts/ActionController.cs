using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour
{

    public Camera mainCamera;
    public float pizzicatoThreshold = 0.36f;
    public SfxController sfxController;


    public Color earthColor;
    public Color fireColor;
    public Color waterColor;
    public Color windColor;
    private Color defaultColor;

    private bool isEarth;
    private bool isEarthOld;

    private bool isFire;
    private bool isFireOld;

    private bool isWater;
    private bool isWaterHold;

    private bool isWind;
    private bool isWindHold;

    private float earthTime;
    private float fireTime;
    private float waterTime;
    private float windTime;

    public void Start()
    {
        if (mainCamera == null)
            Debug.LogError("No camera assigned");
        defaultColor = mainCamera.backgroundColor;
        if (sfxController == null)
            Debug.LogError("No SfxController assigned");
    }

    public void Update()
    {

        #region button validation

        if (Input.GetButtonDown("Earth"))
        {
            mainCamera.backgroundColor = earthColor;
            isEarth = true;
            sfxController.Play(0);
        }

        if (Input.GetButtonUp("Earth"))
        {
            earthTime = 0f;
            isEarth = false;
            mainCamera.backgroundColor = defaultColor;
        }

        if (Input.GetButtonDown("Fire"))
        {
            mainCamera.backgroundColor = fireColor;
            isFire = true;
            sfxController.Play(1);
        }

        if (Input.GetButtonUp("Fire"))
        {
            fireTime = 0f;
            isFire = false;
            mainCamera.backgroundColor = defaultColor;
        }

        if (Input.GetButtonDown("Water"))
        {
            mainCamera.backgroundColor = waterColor;
            isWater = true;
        }

        if (Input.GetButtonUp("Water"))
        {
            isWaterHold = false;
            if (waterTime <= pizzicatoThreshold)
            {
                // TODO
                // call pizzicato here no loop
                sfxController.Pizzicato(2);
            }
            else
            {
                // TODO
                // stop playing the loop
                sfxController.Stop(2);
            }
            waterTime = 0f;
            isWater = false;
            mainCamera.backgroundColor = defaultColor;
        }

        if (Input.GetButtonDown("Wind"))
        {
            mainCamera.backgroundColor = windColor;
            isWind = true;
        }

        if (Input.GetButtonUp("Wind"))
        {
            isWindHold = false;
            if (windTime <= pizzicatoThreshold)
            {
                // TODO
                // call pizzicato here no loop
            }
            else
            {
                // TODO
                // stop playing the loop
            }
            windTime = 0f;
            isWind = false;
            mainCamera.backgroundColor = defaultColor;
        }
        #endregion

        if (isEarth)
            earthTime += Time.deltaTime;
        if (isFire)
            fireTime += Time.deltaTime;
        if (isWater)
            waterTime += Time.deltaTime;
        if (isWind)
            windTime += Time.deltaTime;

        if (waterTime > pizzicatoThreshold && !isWaterHold)
        {
            // TODO
            // play indefinitely
            sfxController.Play(2);
            isWaterHold = true;
        }

        if (windTime > pizzicatoThreshold && !isWindHold)
        {
            // TODO
            // play indefinitely
            isWindHold = true;
        }
    }
}
