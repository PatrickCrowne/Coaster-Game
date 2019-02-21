using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
 
	public GameObject path;
	public GameObject node;
	private GameObject line;
 
	public Node(GameObject o)
	{
		
		node = o;
		line = node.transform.Find("Line").gameObject;
		
	}
	
	public void setTarget(Node n)
	{
		
		line.transform.localScale = new Vector3(1, 1, distance(n) * 2);
		line.transform.LookAt(n.node.transform);
		
	}
	
	public float distance(Node n)
	{
		
		Vector3 a = n.getPosition();
		Vector3 b = getPosition();
		
		return Mathf.Sqrt(((a.x-b.x)*(a.x-b.x)) + ((a.y-b.y)*(a.y-b.y)) + ((a.z-b.z)*(a.z-b.z)));
		
	}
	
	public Vector3 getPosition()
	{
		
		return node.transform.position;
		
	}
	
}
