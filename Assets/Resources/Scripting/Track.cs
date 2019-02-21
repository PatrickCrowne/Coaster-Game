using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
	
	// Constants
	private int editDensity = 5;
	
	//	Default Object
	public GameObject node;
	public GameObject path;
	public GameObject design;
	public Material mat;
	
	//	Coaster Objects
	private GameObject clipPlane;
	private List<GameObject> track = new List<GameObject>();
	private Spline spline = new Spline();
	private TrackModel trackModel;
	
    // Start is called before the first frame update
    void Start()
    {
        
		clipPlane = GameObject.Find("ClipPlane");
		trackModel = new TrackModel(mat);
		
    }

	float distance = 0;
	float bDistance = 0;
    // Update is called once per frame
    void Update()
    {
		
		distance += 0.01f;
		bDistance = spline.getBezierTime(distance);
		if (bDistance >= spline.nodeCount()) 
		{
			distance = 0;
		}
		
		if (spline.nodeCount() > 0)
		{
			path.transform.position = spline.bezier(spline.getBezierTime(distance));
		}
		
    }
	
	public void addNode(RaycastHit hit)
	{
		spline.addNode(Instantiate(node, hit.point, hit.transform.rotation));
	}
	
	public void updateTrack()
	{
		
		if (spline.nodeCount() > 1)
		{
		
			spline.updateNodeTargets();
		
			for (int i = spline.nodeCount()*editDensity; i < track.Count; i++)
			{
				
				Destroy(track[i]);
				track.Remove(track[i]);
				
			}	
			
			for (int i = track.Count-1; i < spline.nodeCount()*editDensity; i++)
			{
				track.Add(Instantiate(design, spline.bezier((float) i / (float) editDensity), new Quaternion(0,0,0,0)));
			}
			
			for (int i = 0; i < track.Count; i++)
			{
				track[i].transform.position = spline.bezier((float) i / (float) editDensity);
			}
		
		}
		
	}
	
	public void buildTrackModel()
	{
		trackModel.generateTrackModel(spline, 8);
	}
}
