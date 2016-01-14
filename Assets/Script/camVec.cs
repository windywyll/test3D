using UnityEngine;
using System.Collections;

public class camVec : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("forward" + this.transform.forward);
        Debug.Log("up" + this.transform.up);
	}
}
