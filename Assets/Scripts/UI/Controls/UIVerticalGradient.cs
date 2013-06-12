using UnityEngine;
using System.Collections;

public class UIVerticalGradient : MonoBehaviour {
	
	public Color fromColor;
	public Color toColor;
	
	private void Start() 
	{
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        
		Vector3[] vertices = mesh.vertices;
        
		Color[] colors = new Color[vertices.Length];
        
		int i = 0;
        
		while (i < vertices.Length) 
		{
            colors[i] = Color.Lerp(toColor, fromColor, vertices[i].y);
            i++;
        }
        mesh.colors = colors;
    }
}
