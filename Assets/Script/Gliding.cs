using UnityEngine;
using System.Collections;

public class Gliding : MonoBehaviour {

    public float minDelayRotation = 5;
    public float maxDelayRotation = 15;
    public float minStopInAir = 1;
    public float maxStopInAir = 5;
    public bool secondStep = true;
    public float turnSpeed = 0.05f;
    private bool beginStop = true;
    private Quaternion maxAngle = Quaternion.Euler(new Vector3(90, 0, 0));
    private float delayRotation, stopInAir, startDelayRotation, startStopInAir;
    private enum rotationWay { none = 0, left = 1, right = 2, round = 3 };
    private rotationWay rotationSelected;
    private int maxRotSelec = 3, minRotSelec = 1;
    private GameObject objToGlide;
    //X

	// Use this for initialization
	void Start () {
        startStopInAir = Time.time;
        stopInAir = Random.Range(minStopInAir, maxStopInAir);
        startDelayRotation = Time.time;
        delayRotation = Random.Range(minDelayRotation, maxDelayRotation);
        rotationSelected = rotationWay.none;
	}
	
	// Update is called once per frame
	void Update () {

	    if(secondStep)
        {
            if( startDelayRotation + delayRotation < Time.time)
            {
                Quaternion rotation = this.gameObject.transform.rotation;
                if (rotationSelected == rotationWay.none)
                {
                    rotationSelected = (rotationWay)Random.Range(minRotSelec, maxRotSelec);
                    beginStop = true;
                    stopInAir = Random.Range(minStopInAir, maxStopInAir);
                }

                switch(rotationSelected)
                {
                    case rotationWay.left:
                        Debug.Log("left");
                        if (this.transform.rotation.x < maxAngle.x && this.transform.rotation.x > -maxAngle.x && beginStop)
                        {
                            rotation *= Quaternion.Euler(1, 0, 0);
                            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                        }
                        else
                        {
                            if(beginStop)
                            {
                                beginStop = false;
                                startStopInAir = Time.time;
                            }
                            else
                            {
                                if (startStopInAir + stopInAir < Time.time)
                                {
                                    rotation *= Quaternion.Euler(-1, 0, 0);
                                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                                }
                            }
                        }

                        if (this.transform.rotation.x <= 0.001 && this.transform.rotation.x >= -0.001 && !beginStop)
                        {
                            startDelayRotation = Time.time;
                            rotationSelected = rotationWay.none;
                            delayRotation = Random.Range(minDelayRotation, maxDelayRotation);
                        }
                        break;
                    case rotationWay.right:
                        Debug.Log("right");
                        if (this.transform.rotation.x < maxAngle.x && this.transform.rotation.x > -maxAngle.x && beginStop)
                        {
                            rotation *= Quaternion.Euler(-1, 0, 0);
                            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                        }
                        else
                        {
                            if (beginStop)
                            {
                                beginStop = false;
                                startStopInAir = Time.time;
                            }
                            else
                            {
                                if (startStopInAir + stopInAir < Time.time)
                                {
                                    rotation *= Quaternion.Euler(1, 0, 0);
                                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                                }
                            }
                        }

                        if (this.transform.rotation.x <= 0.001 && this.transform.rotation.x >= -0.001 && !beginStop)
                        {
                            startDelayRotation = Time.time;
                            rotationSelected = rotationWay.none;
                            delayRotation = Random.Range(minDelayRotation, maxDelayRotation);
                        }
                        break;
                    case rotationWay.round:
                        break;
                }
            }
        }
	}
}
