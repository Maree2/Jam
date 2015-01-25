using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        Mesh planetMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = planetMesh.vertices;

        Color[] colors = new Color[vertices.Length];
        float m;
        for (int i = 0; i < vertices.Length; i++)
        {
            m = vertices[i].magnitude;
            vertices[i] += planetMesh.normals[i] * Mathf.PerlinNoise(vertices[i].x, vertices[i].y) * 0.2f;
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
            colors[i] = Color.Lerp(Color.green, Color.blue, 1 - (0.2f - diff) / (0.2f)); //Color.green;

        }

        planetMesh.colors = colors;

        planetMesh.vertices = vertices;

        planetMesh.RecalculateNormals();
        planetMesh.RecalculateBounds();
    }
	
	// Update is called once per frame
	void Update () 
    {

	}
}
