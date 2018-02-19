﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour {

	public GameObject ropeShooter;

	private SpringJoint2D rope;
	public int maxRopeFrameCount;
	private int ropeFrameCount;
	public LayerMask mask;

	public LineRenderer lineRenderer;

	void Update () {
		if (Input.GetMouseButtonDown (0) && GameObject.Find("Ball").GetComponent<Ball>().stop == true) 
		{
			Fire ();
		}
	}

	void LateUpdate()
	{
		if (rope != null)
		{
			lineRenderer.enabled = true;
			lineRenderer.positionCount = 2;
			lineRenderer.SetPosition (0, ropeShooter.transform.position);
			lineRenderer.SetPosition (1, rope.connectedAnchor);
		} 
		else 
		{
			lineRenderer.enabled = false;
		}
	}

	void FixedUpdate()
	{
		if (rope != null) 
		{
			ropeFrameCount++;
			if (ropeFrameCount > maxRopeFrameCount)
			{
				GameObject.DestroyImmediate (rope);
				ropeFrameCount = 0;
			}
		}
	}

	void Fire()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 position = ropeShooter.transform.position;
		Vector3 direction = mousePosition - position;

		RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);

		if (hit.collider != null) 
		{
			SpringJoint2D newRope = ropeShooter.AddComponent<SpringJoint2D> ();
			newRope.enableCollision = false;
			newRope.frequency = 0.2f;
			newRope.connectedAnchor = hit.point;
			newRope.enabled = true;

			GameObject.DestroyImmediate (rope);
			rope = newRope;
			ropeFrameCount = 0;
		}
	}
}
