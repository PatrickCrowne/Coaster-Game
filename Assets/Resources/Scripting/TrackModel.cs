using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackModel : MonoBehaviour
{
 
	public GameObject crosstie;
	
	private List<GameObject> ties = new List<GameObject>();
	private Rail spine;
	private Material material;
	
	public TrackModel(Material m)
	{
		
		material = m;
		
		crosstie = GameObject.Find("Crosstie");
		spine = new Rail(ties, 4, m);
		
	}
	
	public void generateTrackModel(Spline spline, float tieDensity)
	{
		
		for (int i = 0; i < ties.Count; i++)
		{
			
			Destroy(ties[i]);
			
		}
		
		ties = new List<GameObject>();
		
		for (float i = 0; i < spline.nodeCount(); i += 1.0f / tieDensity)
		{
			
			ties.Add(Instantiate(crosstie, spline.bezier(i), new Quaternion(0,0,0,0)));
			
			
		}
		
		// Tilt
		Vector3 last = new Vector3(0,1,0);
		for (int i = 0; i < ties.Count-1; i++)
		{
			
			Vector3 a = ties[i].transform.position;
			Vector3 b = ties[i+1].transform.position;
			
			Vector3 forward = new Vector3(b.x-a.x, b.y-a.y, b.z-a.z);
			forward.Normalize();
			
			Vector3 up = last;
			
			Quaternion rot = Quaternion.LookRotation(forward, up);
			
			ties[i].transform.rotation = rot;
			
			last = ties[i].transform.up;
			
		}
		
		spine = new Rail(ties, 4, material);
		
	}
	
}
