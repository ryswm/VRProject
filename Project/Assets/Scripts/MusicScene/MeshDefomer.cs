using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDefomer : MonoBehaviour {

	public int band;
	public float startScale = 0;
	public float scaleMulti = 10;

	private Vector3[] meshpoints;

	private Vector3[] Originalpoints;
	private Color[] colors;

	private Mesh mesh;
	// Use this for initialization
	void Start () {
		mesh = this.GetComponent<MeshFilter>().mesh;
		meshpoints = mesh.vertices;
		Originalpoints = mesh.vertices;
		colors = new Color[meshpoints.Length];
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Vertex Count: "+ mesh.vertexCount);
		Debug.Log("meshPoint Len: "+ meshpoints.Length);
	}
}
