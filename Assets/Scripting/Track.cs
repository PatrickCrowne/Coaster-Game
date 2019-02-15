using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
	
	public GameObject node;
	public GameObject path;
	
    // Start is called before the first frame update
    void Start()
    {
        
		
		
    }

    // Update is called once per frame
    void Update()
    {
        
		if (Input.GetButtonDown("Fire1"))
		{
		
			
		
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 10000))
			{
				
				GameObject node2 = Instantiate(node, hit.point, hit.transform.rotation);
				
			}
			
		}
		
    }
}
