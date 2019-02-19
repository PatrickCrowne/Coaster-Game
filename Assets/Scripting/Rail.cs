using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
	
	Mesh mesh;
	GameObject gameObject;
	Material mat;
	
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
	
	public Vector3 getVertexPosition(float x, float y, float z, Vector3 up, Vector3 right, Vector3 forward)
	{
	
		Vector3 u = up;
		u.x *= y;
		u.y *= y;
		u.z *= y;
		
		Vector3 r = right;
		r.x *= x;
		r.y *= x;
		r.z *= x;
		
		Vector3 f = forward;
		f.x *= z;
		f.y *= z;
		f.z *= z;
		
		Vector3 output = new Vector3(u.x + r.x + f.x, u.y + r.y + f.y, u.z + r.z + f.z);
		return output;
		
	}
	
	public void makeFace(List<GameObject> ties, string id, float x1, float y1, float x2, float y2)
	{
		
		Destroy(GameObject.Find("Spine" + id));
		
		mesh = new Mesh();
		
		Vector3[] vertices = new Vector3[2 * ties.Count];
		Vector2[] uv = new Vector2[vertices.Length];
		int[] triangles = new int[6 * ties.Count];
		
		for(int i = 0; i < ties.Count-1; i++)
		{
		
			Vector3 pos = ties[i].transform.position;
		
			Vector3 v;
		
			v = getVertexPosition(x1, y1, 0.0f, ties[i].transform.up, ties[i].transform.right, ties[i].transform.forward);
			
			vertices[(i*2)] = new Vector3(pos.x + v.x,pos.y + v.y,pos.z + v.z);
			
			v = getVertexPosition(x2, y2, 0.0f, ties[i].transform.up, ties[i].transform.right, ties[i].transform.forward);
			
			vertices[(i*2)+1] = new Vector3(pos.x + v.x,pos.y + v.y,pos.z + v.z);
			
			triangles[(i*6)] = 0 + (i*2);
			triangles[(i*6)+1] = 1 + (i*2);
			triangles[(i*6)+2] = 2 + (i*2);
			triangles[(i*6)+3] = 1 + (i*2);
			triangles[(i*6)+4] = 3 + (i*2);
			triangles[(i*6)+5] = 2 + (i*2);
		
		}
		
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		
		gameObject = new GameObject("Spine" + id, typeof(MeshFilter), typeof(MeshRenderer));
		gameObject.transform.localScale = new Vector3(1,1,1);
		
		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		gameObject.GetComponent<Renderer>().material = mat;
		
	}
	
	public Rail(List<GameObject> ties, int lod, Material m)
	{
		
		mat = m;
		
		makeFace(ties, "R", -0.2f, -0.2f, -0.2f, -0.7f);
		makeFace(ties, "D", -0.2f, -0.7f, 0.2f, -0.7f);
		makeFace(ties, "L", 0.2f, -0.7f, 0.2f, -0.2f);
		makeFace(ties, "U", 0.2f, -0.2f, -0.2f, -0.2f);
		
		makeFace(ties, "RailRR", -0.05f + 0.6f, 0.05f, -0.05f + 0.6f, -0.05f);
		makeFace(ties, "RailRD", -0.05f + 0.6f, -0.05f, 0.05f + 0.6f, -0.05f);
		makeFace(ties, "RailRL", 0.05f + 0.6f, -0.05f, 0.05f + 0.6f, 0.05f);
		makeFace(ties, "RailRU", 0.05f + 0.6f, 0.05f, -0.05f + 0.6f, 0.05f);
		
		makeFace(ties, "RailLR", -0.05f - 0.6f, 0.05f, -0.05f - 0.6f, -0.05f);
		makeFace(ties, "RailLD", -0.05f - 0.6f, -0.05f, 0.05f - 0.6f, -0.05f);
		makeFace(ties, "RailLL", 0.05f - 0.6f, -0.05f, 0.05f - 0.6f, 0.05f);
		makeFace(ties, "RailLU", 0.05f - 0.6f, 0.05f, -0.05f - 0.6f, 0.05f);
		
	}
	
}
