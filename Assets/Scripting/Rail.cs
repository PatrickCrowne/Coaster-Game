using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
	
	Mesh mesh;
	GameObject gameObject;
	
	public Vector3 rotateX(Vector3 v, float rot)
	{
		
		float x = (v.x * 1) + (v.y * 0) + (v.z * 0);
		float y = (v.x * 0) + (v.y * Mathf.Cos(rot)) + (v.z * -Mathf.Sin(rot));
		float z = (v.x * 0) + (v.y * Mathf.Sin(rot)) + (v.z * Mathf.Cos(rot));
		
		return new Vector3(x, y, z);
		
	}
	
	public Vector3 rotateY(Vector3 v, float rot)
	{
		
		float x = (v.x * Mathf.Cos(rot)) + (v.y * 0) + (v.z * Mathf.Sin(rot));
		float y = (v.x * 0) + (v.y * 1) + (v.z * 0);
		float z = (v.x * -Mathf.Sin(rot)) + (v.y * 0) + (v.z * Mathf.Cos(rot));
		
		return new Vector3(x, y, z);
		
	}
	
	public Vector3 rotateZ(Vector3 v, float rot)
	{
		
		float x = (v.x * Mathf.Cos(rot)) + (v.y * -Mathf.Sin(rot)) + (v.z * 0);
		float y = (v.x * Mathf.Sin(rot)) + (v.y * Mathf.Cos(rot)) + (v.z * 0);
		float z = (v.x * 0) + (v.y * 0) + (v.z * 1);
		
		return new Vector3(x, y, z);
		
	}
	
	public Rail(List<GameObject> ties, int lod)
	{
		
		Destroy(GameObject.Find("Spine"));
		
		mesh = new Mesh();
		
		Vector3[] vertices = new Vector3[2 * ties.Count];
		Vector2[] uv = new Vector2[0];
		int[] triangles = new int[6 * ties.Count];
		
		for(int i = 0; i < ties.Count-1; i++)
		{
		
			Vector3 pos = ties[i].transform.position;
		
			Vector3 v;
		
			v = new Vector3(0.25f,-0.7f,0);
			v = rotateZ(v, -ties[i].transform.eulerAngles.x / 180.0f * Mathf.PI);
			//v = rotateY(v, -ties[i].transform.eulerAngles.x / 180.0f * Mathf.PI);
			//v = rotateX(v, -ties[i].transform.eulerAngles.x / 180.0f * Mathf.PI);
			
			vertices[(i*2)] = new Vector3(pos.x + v.x,pos.y + v.y,pos.z + v.z);
			
			v = new Vector3(0.25f,-0.2f,0);
			v = rotateZ(v, -ties[i].transform.eulerAngles.x / 180.0f * Mathf.PI);
			//v = rotateY(v, -ties[i].transform.eulerAngles.x / 180.0f * Mathf.PI);
			//v = rotateX(v, -ties[i].transform.eulerAngles.x / 180.0f * Mathf.PI);
			
			vertices[(i*2)+1] = new Vector3(pos.x + v.x,pos.y + v.y,pos.z + v.z);
			
			triangles[(i*6)] = 0 + (i*2);
			triangles[(i*6)+1] = 1 + (i*2);
			triangles[(i*6)+2] = 2 + (i*2);
			triangles[(i*6)+3] = 2 + (i*2);
			triangles[(i*6)+4] = 3 + (i*2);
			triangles[(i*6)+5] = 2 + (i*2);
		
		}
		
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		
		gameObject = new GameObject("Spine", typeof(MeshFilter), typeof(MeshRenderer));
		gameObject.transform.localScale = new Vector3(1,1,1);
		
		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		
	}
	
}
