using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour 
{
    [SerializeField]
    float maxPerlinNoiseMagnitude = 0.2f;

    [SerializeField]
    Color baseColor, strongBaseColor;

    Vector3[] originalNormals;
    Vector3[] originalVertices;
    Color[] originalColors;

    Vector3[] bongoNormals;
    Vector3[] bongoVertices;
    Color[] bongoColors;

    Mesh planetMesh;

    void Awake()
    {
        planetMesh = GetComponent<MeshFilter>().mesh;

        originalVertices = planetMesh.vertices;
        originalColors = planetMesh.colors;
        originalNormals = planetMesh.normals;

        CalculateBongoState();
    }

    void CalculateBongoState(float strength = 1f)
    {
        bongoNormals = new Vector3[originalNormals.Length];
        planetMesh.normals.CopyTo(bongoNormals, 0);

        bongoVertices = new Vector3[originalVertices.Length];
        planetMesh.vertices.CopyTo(bongoVertices, 0);

        bongoColors = new Color[originalColors.Length];
        planetMesh.colors.CopyTo(bongoColors, 0);


        HashSet<int> mountainPeaks = new HashSet<int>();
        int numberOfPeaks = Mathf.RoundToInt(Random.Range(bongoVertices.Length * 0.1f, bongoVertices.Length * 0.4f));

        int tries = 0;
        while (mountainPeaks.Count != numberOfPeaks && tries < 100)
        {
            mountainPeaks.Add(Random.Range(0, bongoVertices.Length));
            tries++;
        }


        float m;
        for (int i = 0; i < bongoVertices.Length; i++)
        {
            m = bongoVertices[i].magnitude;

            float distance = float.MaxValue;
            int nearestPeak = -1;
            foreach (int iii in mountainPeaks)
            {
                float newDistance = Vector3.Distance(bongoVertices[i], bongoVertices[iii]);
                if (newDistance < distance && newDistance < 50f)
                {
                    nearestPeak = iii;
                    distance = newDistance;
                }
            }

            if (distance > 0f)
            {
                float power = (1 / distance) * strength;
                bongoVertices[i] += bongoNormals[i] * power;

                float diff = bongoVertices[i].magnitude - m;

                bongoColors[i] = Color.Lerp(strongBaseColor, baseColor, power); //Color.green;
            }
            else if (distance == 0f)
            {
                bongoVertices[i] += planetMesh.normals[i] * strength * 2f;
                bongoColors[i] = strongBaseColor; ; //Color.green;
                //colors[i] = Color.red;
            }
        }

        /*planetMesh.colors = bongoColors;
        planetMesh.vertices = bongoVertices;
        planetMesh.RecalculateNormals();
        planetMesh.RecalculateBounds();*/
    }

	// Use this for initialization
	void Start ()
    {        
        Vector3[] vertices = new Vector3[originalVertices.Length]; //planetMesh.vertices;
        originalVertices.CopyTo(vertices, 0);

        Color[] colors = new Color[originalColors.Length]; // = new Color[vertices.Length];
        originalColors.CopyTo(colors, 0);
        
        float m;
        for (int i = 0; i < vertices.Length; i++)
        {
            m = vertices[i].magnitude;

            vertices[i] += planetMesh.normals[i] * Mathf.PerlinNoise(vertices[i].x, vertices[i].y) * maxPerlinNoiseMagnitude;
            
            float diff = vertices[i].magnitude - m;

            /*if (diff >= 0 && diff < 0.05f)
            {
                colors[i] = Color.blue;
            }
            else if (diff >= 0.05f && diff < 0.15f)
            {
                colors[i] = Color.Lerp(Color.blue, Color.green,  1 - (0.15f - diff) / (0.15f - 0.05f)) ;
            }
            else if (diff >= 0.15f && diff < 0.18f)
            {
                colors[i] = Color.green;
            }
            else if (diff >= 0.18f && diff < 0.22f)
            {
                colors[i] = Color.Lerp(Color.green, Color.white, 1 - (0.22f - diff) / (0.22f - 0.18f));
            }
            else*/
            colors[i] = Color.Lerp(strongBaseColor, baseColor, 1 - (maxPerlinNoiseMagnitude - diff) / (maxPerlinNoiseMagnitude)); //Color.green;
        }

        planetMesh.colors = colors;
        planetMesh.vertices = vertices;
        planetMesh.RecalculateNormals();
        planetMesh.RecalculateBounds();
    }
	
    void ResetPlanet()
    {
        planetMesh.vertices = originalVertices;
        planetMesh.colors = originalColors;
        planetMesh.normals = originalNormals;

        planetMesh.RecalculateNormals();
        planetMesh.RecalculateBounds();
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Bong(Random.RandomRange(0.2f, 0.45f));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //ResetPlanet();
            Bong(0);
        }
	}
    
    public void Bong(float strength)
    {
        StartCoroutine("BongoGrow", 1f);

        return;

        //ResetPlanet();

        strength = Mathf.Clamp01(strength);

        Vector3[] vertices = new Vector3[originalVertices.Length]; //planetMesh.vertices;
        originalVertices.CopyTo(vertices, 0);

        Color[] colors = new Color[originalColors.Length]; // = new Color[vertices.Length];
        originalColors.CopyTo(colors, 0);
        
        HashSet<int> mountainPeaks = new HashSet<int>();
        int numberOfPeaks = Mathf.RoundToInt( Random.Range(vertices.Length * 0.1f, vertices.Length * 0.4f));

        int tries = 0;
        while (mountainPeaks.Count != numberOfPeaks && tries < 100)
        {
            mountainPeaks.Add(Random.Range(0, vertices.Length));
            tries++;
        }
        

        float m;
        for (int i = 0; i < vertices.Length; i++)
        {
            m = vertices[i].magnitude;

            float distance = float.MaxValue;
            int nearestPeak = -1;
            foreach (int iii in mountainPeaks)
            {
                float newDistance = Vector3.Distance(vertices[i], vertices[iii]);
                if (newDistance < distance && newDistance < 50f)
                {
                    nearestPeak = iii;
                    distance = newDistance;
                }
            }

            if (distance > 0f)
            {
                float power = (1 / distance) * strength;
                vertices[i] += planetMesh.normals[i] * power;

                float diff = vertices[i].magnitude - m;

                colors[i] = Color.Lerp(strongBaseColor, baseColor, power); //Color.green;
            }
            else if (distance == 0f)
            {
                vertices[i] += planetMesh.normals[i] * strength * 2f;
                colors[i] = strongBaseColor;; //Color.green;
                //colors[i] = Color.red;
            }
        }

        planetMesh.colors = colors;
        planetMesh.vertices = vertices;
        planetMesh.RecalculateNormals();
        planetMesh.RecalculateBounds();
    }

    IEnumerator BongoGrow(float time)
    {
        float t = 0f;
        while (t < time)
        {
            for (int i = 0; i < planetMesh.vertices.Length; i++)
            {
                planetMesh.vertices[i] = Vector3.Lerp(planetMesh.vertices[i], bongoVertices[i], t / time);
            }            
            planetMesh.RecalculateNormals();
            planetMesh.RecalculateBounds();
            time += .25f;
            yield return new WaitForSeconds(.25f);
        }
    }
}
