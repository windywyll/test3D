using UnityEngine;
using System.Collections;

public class HallEditor : MonoBehaviour {

    private Camera mainCam;
    public GameObject prefabsHall;
    public GuizmoGrid grid;
    private GameObject newObj;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
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

                    detectForward();
                    detectBack();
                    detectLeft();
                    detectRight();
                }
            }
        }
    }

    void detectForward()
    {
        RaycastHit hit;

        Ray forward = new Ray((newObj.transform.position + (0.5f * newObj.transform.up)), newObj.transform.forward);

        if (Physics.Raycast(forward, out hit, 1.0f))
        {
            Debug.Log("found something");
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallF")
            {
                Debug.Log("yep it's a wall!");
                newObj.transform.FindChild("WallF").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    void detectBack()
    {
        RaycastHit hit;

        Ray back = new Ray((newObj.transform.position + (0.5f * newObj.transform.up)), -newObj.transform.forward);

        if (Physics.Raycast(back, out hit, 1.0f))
        {
            Debug.Log("found something");
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallB")
            {
                Debug.Log("yep it's a wall!");
                newObj.transform.FindChild("WallB").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    void detectRight()
    {
        RaycastHit hit;

        Ray right = new Ray((newObj.transform.position + (0.5f * newObj.transform.up)), newObj.transform.right);

        if (Physics.Raycast(right, out hit, 1.0f))
        {
            Debug.Log("found something");
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallR")
            {
                Debug.Log("yep it's a wall!");
                newObj.transform.FindChild("WallR").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    void detectLeft()
    {
        RaycastHit hit;

        Ray left = new Ray((newObj.transform.position + (0.5f * newObj.transform.up)), -newObj.transform.right);

        if (Physics.Raycast(left, out hit, 1.0f))
        {
            Debug.Log("found something");
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallL")
            {
                Debug.Log("yep it's a wall!");
                newObj.transform.FindChild("WallL").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
}
