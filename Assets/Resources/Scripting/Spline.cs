using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline
{
    
	public List<Node> nodes = new List<Node>();
	
	public void addNode(GameObject o)
	{
		
		nodes.Add(new Node(o));
		updateNodeTargets();
		
	}

	public void updateNodeTargets()
	{
		for (int i = 0; i < nodeCount()-1; i++)
		{
			
			nodes[i].setTarget(nodes[i+1]);
			
		}
	}
	
	public float getWeight(int point, float amount)
	{
		
		return Mathf.Exp( -Mathf.Pow(point - amount, 2));
		
	}
	
	public int nodeCount()
	{
		
		return nodes.Count;
		
	}
	
	public Vector3 getVectorAtPoint(float t)
	{
		
		Vector3 a = bezier(t);
		Vector3 b = bezier(t+0.01f);
		Vector3 o = new Vector3(b.x-a.x, b.y-a.y, b.z-a.z);
		return o;
		
	}
	
	public float squareDistance(Vector3 a, Vector3 b)
	{
		
		return ((a.x-b.x)*(a.x-b.x)) + ((a.y-b.y)*(a.y-b.y)) + ((a.z-b.z)*(a.z-b.z));
		
	}
	
	public float getBezierTime(float dist)
	{
		
		float interval = 0.01f;
		float intervalSquared = interval*interval;
		float time = 0.0f;
		float totalDistance = 0.0f;
		
		Vector3 current = bezier(0.0f);
		Vector3 last = bezier(0.0f);
		
		while (totalDistance < dist)
		{
		
			while (squareDistance(current, last) < intervalSquared)
			{
				
				time += 0.05f;
				current = bezier(time);
				
			}
		
			last = bezier(time);
			totalDistance += interval;
		
		
		}
		
		return time;
		
	}
	
	public Vector3 bezier(float t)
	{
		
		Vector3 output = new Vector3(0,0,0);
		
		float distance = t % 1;
		float x = 0;
		float y = 0;
		float z = 0;
		float baseVal = 0;
		
		for (int i = 0; i < nodes.Count; i++)
		{
			
			
			x += (1 * nodes[i].getPosition().x * getWeight((int) i - (int)(t - (distance)), distance));
			y += (1 * nodes[i].getPosition().y * getWeight((int) i - (int)(t - (distance)), distance));
			z += (1 * nodes[i].getPosition().z * getWeight((int) i - (int)(t - (distance)), distance));
			baseVal += getWeight((int) i - (int)(t - (distance)), distance);
			
			
		}
		
		output.x = x/baseVal;
		output.y = y/baseVal;
		output.z = z/baseVal;
		
		return output;
		
	}
	
}
