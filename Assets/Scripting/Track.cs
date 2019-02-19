using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
	
	//	Default Object
	public GameObject node;
	public GameObject path;
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

	float t;
	
    // Update is called once per frame
    void Update()
    {
        
		t += 0.01f;
		
		if (t >= spline.nodeCount()) {
			
			t = 0;
			
		}
		
		path.transform.position = spline.bezier(t);
		
    }
	
	public void addNode(RaycastHit hit)
	{
		spline.addNode(Instantiate(node, hit.point, hit.transform.rotation));
	}
	
	public void updateTrack()
	{
		trackModel.generateTrackModel(spline, 8);
	}
}
