using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	
	public float sensitivity = 5.0f;
	
	private static int VIEW = 0;
	private static int SELECT = 1;
	private static int PLACE = 2;
	
	private GameObject cameraObject;
	private GameObject currentNode;
	private bool nodeSelected = false;
	
	private int mode = VIEW;
	private int lastMode = PLACE;
	private Material normal;
	private Material highlighted;
	
	
	// OBJECTS
	public GameObject currentTrack;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
		normal = Resources.Load("Materials/TrackNode", typeof(Material)) as Material;
		highlighted = Resources.Load("Materials/TrackNodeHighlighted", typeof(Material)) as Material; 
		
		cameraObject = gameObject.transform.Find("Camera").gameObject;
		
    }

	void MoveCamera()
	{
		
		float forward = Input.GetAxis("Vertical");
		float right = Input.GetAxis("Horizontal");
		float up = Input.GetAxis("Upwards");
		float scroll = Input.GetAxis("Mouse ScrollWheel");
	 
		gameObject.transform.position += cameraObject.transform.forward * Time.deltaTime * forward * 100.0f;
		gameObject.transform.position += gameObject.transform.right * Time.deltaTime * right * 100.0f;
		gameObject.transform.position += gameObject.transform.up * Time.deltaTime * up * 100.0f;
		
		Camera.main.fieldOfView -= scroll * 30.0f;
		
	}
	
	void SetViewMode()
	{
		
		if (Input.GetMouseButtonDown(1))
		{
			if (mode == VIEW)
			{
				mode = lastMode;
			}
			else
			{
				lastMode = mode;
				mode = VIEW;
			}
		}
		if (Input.GetKeyDown("i"))
		{
			
			mode = SELECT;
			
		}
		if (Input.GetKeyDown("p"))
		{
			
			mode = PLACE;
			
		}
		
		if (mode == VIEW)
		{
			
			modeVIEW();
			
		}
		if (mode == SELECT)
		{
			
			modeSELECT();
			
		}
		if (mode == PLACE)
		{
			
			modePLACE();
			
		}
		
	}
	
	void modeVIEW()
	{
		float lookUp = Input.GetAxis("Mouse Y");
		float lookRight = Input.GetAxis("Mouse X");
			
		gameObject.transform.Rotate(0, lookRight * sensitivity, 0);
		cameraObject.transform.Rotate(lookUp * -sensitivity, 0, 0);
	}
	
	void modePLACE()
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
	
	void modeSELECT()
	{
		
		if (Input.GetMouseButtonDown(0))
		{
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			//	Find where it lands
			if (Physics.Raycast(ray, out hit, 10000))
			{
				
				GameObject objectHit = hit.transform.gameObject;
				
				if (objectHit.name.Equals("Node(Clone)"))
				{
					
					if(currentNode != null)
					{
						currentNode.GetComponent<MeshRenderer>().material = normal;
					}
					
					currentNode = objectHit;
					currentNode.GetComponent<MeshRenderer>().material = highlighted;
					nodeSelected = true;
				}
				else
				{
					currentNode = null;
					nodeSelected = false;
				}
				
			}
			
		}
		
	}
	
	void MoveNodes()
	{
		
		if (currentNode != null)
		{
			
			Track trackScript = (Track) currentTrack.GetComponent(typeof(Track));
			
			if (Input.GetKey("page up"))
			{
				currentNode.transform.Translate(0, 0.1f, 0, Space.World);
				trackScript.updateTrack();
			}
			if (Input.GetKey("page down"))
			{
				currentNode.transform.Translate(0, -0.1f, 0, Space.World);
				trackScript.updateTrack();
			}
			
			if (Input.GetKey("up"))
			{
				currentNode.transform.Translate(0, 0, 0.1f, Space.World);
				trackScript.updateTrack();
			}
			if (Input.GetKey("down"))
			{
				currentNode.transform.Translate(0, 0, -0.1f, Space.World);
				trackScript.updateTrack();
			}
			
			if (Input.GetKey("right"))
			{
				currentNode.transform.Translate(0.1f, 0, 0, Space.World);
				trackScript.updateTrack();
			}
			if (Input.GetKey("left"))
			{
				currentNode.transform.Translate(-0.1f, 0, 0, Space.World);
				trackScript.updateTrack();
			}
			
		}
		
	}
	
    // Update is called once per frame
    void Update()
    {
     
		MoveCamera();
		SetViewMode();
		MoveNodes();
		
    }
}
