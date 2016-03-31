using UnityEngine;
using System.Collections;

public class HallEditor : MonoBehaviour {

    private Camera mainCam;
    public GameObject prefabsHall;
    public GuizmoGrid grid;
    private GameObject newObj;
    private Ray forward;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
        forward = new Ray(Vector3.zero, Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))

        {
            RaycastHit hit;

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))

            {
                if (hit.transform.tag != "Hall")
                {
                    newObj = Instantiate(prefabsHall);
                    Vector3 aligned = new Vector3(Mathf.Floor(hit.point.x / grid.xWidth) * grid.xWidth + grid.xWidth / 2.0f, 0.0f, Mathf.Floor(hit.point.z / grid.zWidth) * grid.zWidth + grid.zWidth / 2.0f);
                    newObj.transform.position = aligned;

                    Debug.Log("created");

                    forward = new Ray((newObj.transform.position + (0.55f * newObj.transform.forward)), newObj.transform.forward);

                    Debug.Log("orig : " + forward.origin);
                    Debug.Log("dir : " + forward.direction);
                    Debug.Log("pos cube : " + newObj.transform.position);

                    if (Physics.Raycast(ray, out hit, 0.5f))
                    {
                        Debug.Log("found something");
                        if (hit.transform.tag == "Hall")
                        {
                            Debug.Log("yep it's a wall!");
                            newObj.transform.FindChild("WallF").gameObject.SetActive(false);
                            hit.transform.gameObject.SetActive(false);
                        }
                    }

                    /*ray = new Ray((newObj.transform.position + (0.55f * -newObj.transform.forward)), -newObj.transform.forward);
                    ray = new Ray((newObj.transform.position + (0.55f * newObj.transform.right)), newObj.transform.right);
                    ray = new Ray((newObj.transform.position + (0.55f * -newObj.transform.right)), -newObj.transform.right);*/
                }
            }
        }

        Debug.DrawRay(forward.origin, forward.direction, Color.red);
    }
}
