﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using scMessage;
>>>>>>> pr/6

public class UserSnake : Snake
{
	// The speed of the snake in ms
	// intially 0.1
	private float speed;

	// Rotation direction
	private Quaternion dir;

	// Helps simulate responsiveness
	private Quaternion lastRotation;

	// Bools for food eating
<<<<<<< HEAD
	private bool apple;
	private bool onion;
	private bool mouse;
=======
	private bool apple = false;
	private bool onion = false;
	private bool rat = false;
	private bool moveable = true;
>>>>>>> pr/6

	// Use this for initialization
	void Start ()
	{
		dir = Quaternion.identity;

		// Sets the snake to start in the center
		// TODO: Once multiplayer, the player number will matter

		int userID = GameObject.Find ("PlayerList").GetComponent<PlayerList> ().startId;


		transform.position = new Vector3 (0, userID, 0);
		transform.rotation = dir;

		calcSpeed ();

		move ();
		
<<<<<<< HEAD
		InvokeRepeating ("grow", 0.0f, 1.0f);
=======
		//InvokeRepeating ("grow", 0.0f, 1.0f);
>>>>>>> pr/6
	}
	
	// Update is called once per frame
	void Update ()
	{
		input ();
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
<<<<<<< HEAD
		Application.LoadLevel ("GameScene");
=======
		float fx = coll.gameObject.transform.position.x;
		float fy = coll.gameObject.transform.position.y;
		message m = new message ("foodDestroy");
		scObject foodInfo = new scObject ("foodInfo");
		foodInfo.addFloat ("xPos", fx);
		foodInfo.addFloat ("yPos", fy);
		m.addSCObject (foodInfo);
		//food
		if (coll.name.StartsWith ("apple")) {
			apple = true;
			Client.Instance.SendServerMessage (m);
			Destroy (coll.gameObject);
		} else if (coll.name.StartsWith ("onion")) {
			onion = true;
			Client.Instance.SendServerMessage (m);
			Destroy (coll.gameObject);
		} else if (coll.name.StartsWith ("rat")) {
			rat = true;
			Client.Instance.SendServerMessage (m);
			Destroy (coll.gameObject);
		} else {
			//Application.LoadLevel ("GameScene");
			moveable = false;
		}
		    
>>>>>>> pr/6
	}
	
	private void move ()
	{
<<<<<<< HEAD
		if (segments.Count > 0) {
			for (int i = segments.Count - 1; i > 0; i--) {
				segments [i].transform.position = segments [i - 1].transform.position;
				segments [i].transform.rotation = segments [i - 1].transform.rotation;
			}

			segments [0].transform.position = transform.position;
			segments [0].transform.rotation = transform.rotation;
		}

		lastRotation = transform.rotation;
		transform.Translate (Vector2.right);
	
		Invoke ("move", speed);
=======
		if (moveable) {
			if (segments.Count > 0) {
				for (int i = segments.Count - 1; i > 0; i--) {
					segments [i].transform.position = segments [i - 1].transform.position;
					segments [i].transform.rotation = segments [i - 1].transform.rotation;
				}

				segments [0].transform.position = transform.position;
				segments [0].transform.rotation = transform.rotation;
			}

			lastRotation = transform.rotation;
			transform.Translate (Vector2.right);
	
			if (apple) {
				grow ();
				apple = false;
			}

			Invoke ("move", speed);
		}
>>>>>>> pr/6
	}

	public void grow ()
	{
		Vector3 pos = transform.position;
		Quaternion rot = transform.rotation;
		if (segments.Count > 0) {
			pos = segments [segments.Count - 1].transform.position;
			rot = segments [segments.Count - 1].transform.rotation;
		}
				
		GameObject newSegment = (GameObject)Instantiate (body, pos - rot * new Vector3 (1, 0, 0), rot);

		segments.Add (newSegment);

		calcSpeed ();
	}

	private void calcSpeed ()
	{
		speed = 0.1f + 0.005f * segments.Count;
		
		if (speed > 0.20f) {
			speed = 0.20f;
		}
	}

	private void input ()
	{
		// Use the given input for compatability and easier customization
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		dir = transform.rotation;
		// Snake can not move in the oppisite direction that it is moving
		if (horizontal == -1 && !Mathf.Approximately (lastRotation.eulerAngles.z, 0)) {			// LEFT
			dir.eulerAngles = new Vector3 (0, 0, 180);
		} else if (horizontal == 1 && !Mathf.Approximately (lastRotation.eulerAngles.z, 180)) {	// RIGHT
			dir.eulerAngles = new Vector3 (0, 0, 0);	
		} else if (vertical == -1 && !Mathf.Approximately (lastRotation.eulerAngles.z, 90)) {		// DOWN
			dir.eulerAngles = new Vector3 (0, 0, 270);	
		} else if (vertical == 1 && !Mathf.Approximately (lastRotation.eulerAngles.z, 270)) {		// UP
			dir.eulerAngles = new Vector3 (0, 0, 90);
		}

		transform.rotation = dir;
	}
}
