using UnityEngine;
using System.Collections;

public class PlayerGrab : MonoBehaviour {

    public int maxStamina = 2000;
    private int stamina;
    private bool hanging = false;
    private bool canMove = true;
    public float gravity = 20.0F;
    private PlayerControler pc;
    private PlayerAttack pa;
    private Animator anim;
    private Collider colHang = null;
    private Quaternion initialRot;

    void Awake()
    {
        initialRot = this.transform.rotation;
    }

	// Use this for initialization
	void Start () {
        stamina = maxStamina;

        pc = this.gameObject.GetComponent<PlayerControler>();
        pa = this.gameObject.GetComponent<PlayerAttack>();
        anim = this.gameObject.GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "HangingPoint")
        {
            colHang = col;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "HangingPoint")
        {
            colHang = null;
        }
    }

    public void cantMove()
    {
        canMove = false;
    }

    public void freeMove()
    {
        canMove = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    
        if(colHang != null)
        {
            if(Input.GetButton("Hang"))
            {
                pc.cantMove();
                pa.cantMove();

                anim.SetBool("hanging", true);

                hanging = true;
                stamina--;

                this.gameObject.transform.parent = colHang.gameObject.transform;

            }

            if(Input.GetButtonUp("Hang") || stamina == 0 )
            {
                hanging = false;

                anim.SetBool("hanging", false);

                pc.freeMove();
                pa.freeMove();

                this.gameObject.transform.parent = null;
                this.transform.rotation = initialRot;
            }
        }
        
        if(!hanging && stamina < maxStamina)
        {
            stamina++;
        }

	}
}
