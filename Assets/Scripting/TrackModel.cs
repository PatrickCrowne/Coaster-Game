using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackModel : MonoBehaviour
{
 
	public GameObject crosstie;
	
	private List<GameObject> ties = new List<GameObject>();
	private Rail spine;
 
	public TrackModel()
	{
		
		crosstie = GameObject.Find("Crosstie");
		spine = new Rail(ties, 4);
		
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
		Vector3 lastRight = new Vector3();
		for (int i = 0; i < ties.Count-1; i++)
		{
			
			if (i == 0) {
				
				ties[i].transform.LookAt(ties[i+1].transform);	
				
			} else {
				
				ties[i].transform.LookAt(ties[i+1].transform);	
				
				ties[i].transform.up = Vector3.Cross(lastRight, ties[i].transform.forward);
				ties[i].transform.right = Vector3.Cross(ties[i].transform.right, ties[i].transform.forward);
				
			}
			
			lastRight = ties[i].transform.right;
			
		}
		
		spine = new Rail(ties, 4);
		
	}
	
}
