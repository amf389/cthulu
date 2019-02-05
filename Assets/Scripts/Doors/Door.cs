﻿using System.Collections;
using System.Collections.Generic;
using Cthulu;
using Cthulu.Events;
using UnityEngine;

public class Door : Interactable, IManageable {
	[SerializeField]
	private GameObject[] doorsInSet;

	private bool isLocked;

	[SerializeField]
	private bool isOpen;
	private AudioSource source;

	public GameObject obj => gameObject;
	Vector3 pos;
	Vector3 rot;

	// Use this for initialization
	void Start() {
		this.SetName();
		pos = transform.position;
		rot = transform.eulerAngles;
	}

	public void Open() { //TODO: Make Coroutine
		GameManager.When(name,"open");
		transform.Rotate(0, 90, 0);
	}

	public void Close() { //TODO: Make Coroutine
		transform.eulerAngles = rot;
	}

	public void Lock() {
		Close();
		isLocked = true;
	}

	public void Unlock() {
		isLocked = false;
	}

	public bool IsLocked() {
		return isLocked;
	}

	public bool IsOpen() {
		return isOpen;
	}

	public override void Interact(Player p) {
		throw new System.NotImplementedException();
	}

	public void Set() {
		throw new System.NotImplementedException();
	}

	public string[] ValidArgs() {
		throw new System.NotImplementedException();
	}

	public void Set(GameEvent ge) {
		string param = ge.args[0];
		if (param == "open") {
			Open();
		} else if (param == "close") {
			Close();
		} else if (param == "lock") {
			Lock();
		} else if (param == "unlock") {
			Unlock();
		}
	}

	

	void FixedUpdate() {
		transform.position = pos;
		//ClampRotation();

	}

	private void ClampRotation() {
		Vector3 e = transform.eulerAngles;
		float angleY = e.y;
		if (angleY > 90) {
			angleY = 90;
		} else if (angleY < 0) {
			angleY = 0;
		}
		e.y = angleY;
		transform.eulerAngles = e;
	}
}