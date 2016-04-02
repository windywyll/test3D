using UnityEngine;
using System.Collections;

public class Hall : MonoBehaviour {

    public HallEditor editor;

	// Use this for initialization
	void Start () {
        editor = GameObject.Find("Editor").GetComponent<HallEditor>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void createHall()
    {
        editor.createHall(gameObject);
    }
}
