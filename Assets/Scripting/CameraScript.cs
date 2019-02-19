using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
		float forward = Input.GetAxis("Vertical");
		float right = Input.GetAxis("Horizontal");
		
		float lookUp = Input.GetAxis("Mouse Y");
		float lookRight = Input.GetAxis("Mouse X");
	 
		gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * forward * 100.0f;
		gameObject.transform.position += gameObject.transform.right * Time.deltaTime * right * 100.0f;
		
    }
}
