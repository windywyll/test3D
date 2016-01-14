using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidManager : MonoBehaviour {

    public GameObject boidPrefab;
    List<Boid> boidList;
    float MINIMUM_DIST_TO_FOLLOW = 10f;

	// Use this for initialization
	void Start () {
        boidList = new List<Boid>();

        float range = 10f;
        for(int i = 0; i < 32; i++)
        {
            float size = (Random.Range(1f, range)) / 4;
            Vector3 position = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
            AddBoid(position,size);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    for(int current = 0; current < boidList.Count; current++)
        {
            Boid boid = boidList[current];
            int countAlignement = 0;
            Vector3 vectorCohesion = Vector3.zero;
            Vector3 vectorAlignement = Vector3.zero;
            Vector3 vectorSeparation = Vector3.zero;

            Vector3 vectorTarget = boid.target - boid.position;
            for (int other = 0; other < boidList.Count; other++)
            {
                Boid boidOther = boidList[other];
                if(current != other)
                {
                    float dist = Vector3.Distance(boid.position, boidOther.position);
                    dist -= (boid.size + boidOther.size) / 2;

                    if(dist <= 0f)
                    {
                        vectorSeparation += boid.position - boidOther.position;
                    }

                    if(dist <= MINIMUM_DIST_TO_FOLLOW)
                    {
                        vectorAlignement += boidOther.velocity;
                        countAlignement++;
                    }

                    vectorCohesion += boidOther.position - boid.position;
                }
            }

            vectorCohesion /= (float)(boidList.Count - 1);

            if (countAlignement > 0)
            {
                vectorAlignement /= (float)countAlignement;
            }

            boid.ApplyVelocity(vectorCohesion * boid.scaleCohesion + vectorSeparation * boid.scaleSeparation + vectorAlignement * boid.scaleAlignement + vectorTarget * boid.scaleTarget);
        }
	}

    void AddBoid(Vector3 position , float size)
    {
        GameObject boidGameObject = GameObject.Instantiate(boidPrefab) as GameObject;
        Boid boid = boidGameObject.AddComponent<Boid>();
        boid.SetSize(size);
        boid.SetPosition(position);
        boid.scaleAlignement = 0.00078f;
        boid.scaleCohesion = 0.0005f;
        boid.scaleSeparation = 0.00004f;
        boid.scaleTarget = 0.0009f;
        boid.friction = 1f;
        boidList.Add(boid);
    }
}
