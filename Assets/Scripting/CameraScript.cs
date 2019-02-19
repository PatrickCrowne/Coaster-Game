using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	
	private static int VIEW = 0;
	private static int EDIT = 1;
	
	private GameObject cameraObject;
	private int mode = VIEW;
	
	public float sensitivity = 5.0f;
	
	// OBJECTS
	public GameObject currentTrack;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
		cameraObject = gameObject.transform.Find("Camera").gameObject;
		
    }

    // Update is called once per frame
    void Update()
    {
     
		float forward = Input.GetAxis("Vertical");
		float right = Input.GetAxis("Horizontal");
		float up = Input.GetAxis("Upwards");
	 
		gameObject.transform.position += cameraObject.transform.forward * Time.deltaTime * forward * 100.0f;
		gameObject.transform.position += gameObject.transform.right * Time.deltaTime * right * 100.0f;
		gameObject.transform.position += gameObject.transform.up * Time.deltaTime * up * 100.0f;
		
		if (Input.GetMouseButtonDown(1))
		{
			mode = -mode + 1;
		}
		
		if (mode == VIEW)
		{
			
			float lookUp = Input.GetAxis("Mouse Y");
			float lookRight = Input.GetAxis("Mouse X");
			
			gameObject.transform.Rotate(0, lookRight * sensitivity, 0);
			cameraObject.transform.Rotate(lookUp * -sensitivity, 0, 0);
		} else
		{
			
			if (Input.GetMouseButtonDown(0))
			{
				
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
			
				//	Find where it lands
				if (Physics.Raycast(ray, out hit, 10000))
				{
					// Get Script
					Track trackScript = (Track) currentTrack.GetComponent(typeof(Track));
					// Add Node to list of nodes
					trackScript.addNode(hit);
					// Update Track Model
					trackScript.updateTrack();
				
				}
				
			}
			
		}
		
    }
}
