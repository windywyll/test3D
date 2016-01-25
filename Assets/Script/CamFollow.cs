using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 nPos = player.transform.position;
        nPos.y = this.transform.position.y;
	    this.transform.position = nPos;
	}
}
