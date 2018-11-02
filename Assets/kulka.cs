using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kulka : MonoBehaviour {
    public camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!cam.isholding)
        {
            this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
        }
        if(cam.isholding)
        {
            this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * cam.odleglosc;
        }

    }
}
