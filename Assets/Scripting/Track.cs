using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
	
	//	Default Object
	public GameObject node;
	
	//	Coaster Objects
	private GameObject clipPlane;
	private List<GameObject> track = new List<GameObject>();
	private Spline spline = new Spline();
	
    // Start is called before the first frame update
    void Start()
    {
        
		clipPlane = GameObject.Find("ClipPlane");
		
    }

    // Update is called once per frame
    void Update()
    {
        
		if (Input.GetButtonDown("Fire1"))
		{
		
			// Make a ray
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			//	Find where it lands
			if (Physics.Raycast(ray, out hit, 10000))
			{
				// Add Node to list of nodes
				spline.addNode(Instantiate(node, hit.point, hit.transform.rotation));
				
				// Move clip plane, if needed
				clipPlane.transform.position = new Vector3(0,0,(int)hit.distance-99);
				
			}
			
		}
		
    }
}
