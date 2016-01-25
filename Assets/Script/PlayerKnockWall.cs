using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerKnockWall : MonoBehaviour {

    private Transform playerTransform;
    private float radiusSoundPlayer = 10;
    private int wall = 0;
    private List<Pair<GameObject, float>> listSound;
    private float soundLife = 1.0f;
    private bool disabled = false;

    public void disableKnock()
    {
        disabled = true;
    }

    public void enableKnock()
    {
        disabled = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Wall")
        {
            wall++;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "Wall")
        {
            wall--;
        }
    }

	// Use this for initialization
	void Start () {
        playerTransform = this.transform;
        listSound = new List<Pair<GameObject, float>>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Attack") && wall > 0 && !disabled)
        {
            GameObject sound = new GameObject();
            sound.transform.position = playerTransform.position;
            sound.AddComponent<SphereCollider>();
            sound.GetComponent<SphereCollider>().radius = radiusSoundPlayer;
            sound.GetComponent<SphereCollider>().isTrigger = true;

            listSound.Add(new Pair<GameObject, float>(sound, Time.time));
        }

        if(listSound.Count > 0)
        {
            for(int i = 0; i < listSound.Count; i++)
            {
                if(listSound[i].getSecond() + soundLife < Time.time)
                {
                    Destroy(listSound[i].getFirst());
                    listSound.RemoveAt(i);
                    i--;
                }
            }
        }
	}
}
