using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackModel : MonoBehaviour
{
 
	public GameObject crosstie;
	private List<GameObject> ties = new List<GameObject>();
 
	public TrackModel()
	{
		
		crosstie = GameObject.Find("Crosstie");
		
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
		for (int i = 0; i < ties.Count-1; i++)
		{
			
			ties[i].transform.LookAt(ties[i+1].transform);
			
		}
		
	}
	
}
