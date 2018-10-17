﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    bool scare = false;
    public float MoveSpeed = 5;
    public GameObject door;
    Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Tentacle").Length; i++) {
            if (GameObject.FindGameObjectsWithTag("Tentacle")[i].GetComponent<Tentacle>().alive == true) {
                scare = true;
                break;
            }
        }
        transform.LookAt(door.transform);
        if (scare) {
            rb.AddForce((transform.forward) *  MoveSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "door") {
            Destroy(gameObject);
        }
    }


}
