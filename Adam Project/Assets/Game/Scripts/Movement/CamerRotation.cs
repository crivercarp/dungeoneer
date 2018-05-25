using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float step = 10 * Time.deltaTime;

        Quaternion wantedRotation = Quaternion.LookRotation (new Vector3(0,0,0) - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Lerp (gameObject.transform.rotation, wantedRotation, step);
    }
}
