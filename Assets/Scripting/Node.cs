using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
 
	public GameObject path;
	private GameObject node;
 
	public Node(GameObject o)
	{
		
		node = o;
		
	}
	
	public Vector3 getPosition()
	{
		
		return node.transform.position;
		
	}
	
}
