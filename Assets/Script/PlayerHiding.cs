using UnityEngine;
using System.Collections;

public class PlayerHiding : MonoBehaviour {
    private bool grounded = false;
    private bool isCrouched = false;
    private GameObject box;
    private CapsuleCollider capsuleCol;
    private MeshRenderer meshCapsule;
    private RigiBodyController scriptControl;
    private PlayerGrapProp grapControl;
    private PlayerKnockWall knockControl;

	// Use this for initialization
    void Awake()
    {
        meshCapsule = this.GetComponent<MeshRenderer>();
        capsuleCol = this.GetComponent<CapsuleCollider>();
        scriptControl = this.GetComponent<RigiBodyController>();
        grapControl = this.GetComponent<PlayerGrapProp>();
        knockControl = this.GetComponent<PlayerKnockWall>();
        box = this.transform.FindChild("BoxA").gameObject;
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded)
        {
            if (Input.GetButtonDown("Hide"))
            {
                isCrouched = !isCrouched;

                if (isCrouched)
                {
                    meshCapsule.enabled = false;
                    capsuleCol.enabled = false;
                    box.SetActive(true);
                    scriptControl.disableAllButMove();
                    scriptControl.setToCrouchSpeed();
                    knockControl.disableKnock();
                    grapControl.disableGrab();
                }
                else
                {
                    meshCapsule.enabled = true;
                    capsuleCol.enabled = true;
                    box.SetActive(false);
                    scriptControl.enableAll();
                    scriptControl.revertToInitSpeed();
                    knockControl.enableKnock();
                    grapControl.enableGrab();
                }
            }
        }
	}

    void OnCollisionStay()
    {
        grounded = true;
    }
}
