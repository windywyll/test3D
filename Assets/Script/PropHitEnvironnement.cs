using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropHitEnvironnement : MonoBehaviour {

    private Transform propTransform;
    private List<Pair<GameObject, float>> listSound;
    private float soundLife = 1.0f;
    public float radiusSound = 5;

    void OnCollisionEnter(Collision col)
    {
        GameObject sound = new GameObject();
        sound.transform.position = propTransform.position;
        sound.AddComponent<SphereCollider>();
        sound.GetComponent<SphereCollider>().radius = radiusSound;
        sound.GetComponent<SphereCollider>().isTrigger = true;

        listSound.Add(new Pair<GameObject, float>(sound, Time.time));
    }

	// Use this for initialization
	void Start () {
        propTransform = this.transform;
        listSound = new List<Pair<GameObject, float>>();
	}
	
	// Update is called once per frame
	void Update () {
        if (listSound.Count > 0)
        {
            for (int i = 0; i < listSound.Count; i++)
            {
                if (listSound[i].getSecond() + soundLife < Time.time)
                {
                    Destroy(listSound[i].getFirst());
                    listSound.RemoveAt(i);
                    i--;
                }
            }
        }
	}
}
