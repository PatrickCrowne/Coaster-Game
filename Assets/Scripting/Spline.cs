using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline
{
    
	List<Node> nodes = new List<Node>();
	
	public void addNode(GameObject o)
	{
		
		nodes.Add(new Node(o));
		
	}
	
}
