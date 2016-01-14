using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour {

    public Vector3 position = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public Vector3 target = Vector3.zero;

    public float size = 1f;
    public float speed = 1f;
    public float friction = 1f;

    public float scaleCohesion = 0.005f;
    public float scaleSeparation = 0.002f;
    public float scaleAlignement = 0.006f;
    public float scaleTarget = 0.001f;

    Material material;


	// Use this for initialization
	void Start () {
        float r = Random.value;
        float g = Random.value;
        float b = Random.value;
        this.GetComponent<Light>().color = new Color(r,g,b);
        material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ApplyVelocity(Vector3 newVelocity)
    {
        velocity += newVelocity;

        position += velocity * speed / size;
        transform.position = position;

        velocity *= friction;

        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        material.SetFloat("_Range", Mathf.Min(1f, 0.5f * velocity.magnitude));
    }

    public void SetSize(float newSize)
    {
        size = newSize;
        transform.localScale = Vector3.one * size;
    }

    public void SetPosition (Vector3 newPosition)
    {
        position = newPosition;
        transform.position = position;
    }
}
