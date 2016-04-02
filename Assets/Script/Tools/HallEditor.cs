using UnityEngine;
using System.Collections;

public class HallEditor : MonoBehaviour {

    public GameObject editorInstance;
    private Camera mainCam;
    public GameObject prefabsHall;
    public GuizmoGrid grid;

    // Use this for initialization
    void Awake()
    {
        if (editorInstance == null)
        {
            editorInstance = gameObject;
        }
        else
        {
            if (editorInstance != gameObject)
                Destroy(gameObject);
        }
    }

    void Start () {
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.tag != "Hall")
                {
                    GameObject obj = Instantiate(prefabsHall);
                    Vector3 aligned = new Vector3(Mathf.Floor(hit.point.x / grid.xWidth) * grid.xWidth + grid.xWidth / 2.0f, 0.0f, Mathf.Floor(hit.point.z / grid.zWidth) * grid.zWidth + grid.zWidth / 2.0f);
                    obj.transform.position = aligned;
                }
            }
        }
    }

    public void createHall(GameObject obj)
    {
        Vector3 objPos = obj.transform.position;
        obj.transform.FindChild("Floor").gameObject.SetActive(false);

        Ray ray = new Ray(objPos + (0.5f * obj.transform.up), -obj.transform.up);
        RaycastHit hit;

        bool repeated = false;

        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (hit.transform.tag == "Hall")
            {
                Destroy(obj);
                repeated = true;
            }
        }

        if (!repeated)
        {
            obj.transform.FindChild("Floor").gameObject.SetActive(true);
            detectForward(obj);
            detectBack(obj);
            detectLeft(obj);
            detectRight(obj);
        }
    }

    void detectForward(GameObject obj)
    {
        RaycastHit hit;

        Ray forward = new Ray((obj.transform.position + (0.5f * obj.transform.up)), obj.transform.forward);

        if (Physics.Raycast(forward, out hit, 1.0f))
        {
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallF")
            {
                obj.transform.FindChild("WallF").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    void detectBack(GameObject obj)
    {
        RaycastHit hit;

        Ray back = new Ray((obj.transform.position + (0.5f * obj.transform.up)), -obj.transform.forward);

        if (Physics.Raycast(back, out hit, 1.0f))
        {
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallB")
            {
                obj.transform.FindChild("WallB").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    void detectRight(GameObject obj)
    {
        RaycastHit hit;

        Ray right = new Ray((obj.transform.position + (0.5f * obj.transform.up)), obj.transform.right);

        if (Physics.Raycast(right, out hit, 1.0f))
        {
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallR")
            {
                obj.transform.FindChild("WallR").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    void detectLeft(GameObject obj)
    {
        RaycastHit hit;

        Ray left = new Ray((obj.transform.position + (0.5f * obj.transform.up)), -obj.transform.right);

        if (Physics.Raycast(left, out hit, 1.0f))
        {
            if (hit.transform.tag == "Hall" && hit.transform.name != "WallL")
            {
                obj.transform.FindChild("WallL").gameObject.SetActive(false);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
}
