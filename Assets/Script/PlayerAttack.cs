using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    private bool attacking = false;
    private bool canMove = true;
    private PlayerControler pc;
    private PlayerGrab pg;
    private Animator anim;

	// Use this for initialization
	void Start () {
        pc = this.gameObject.GetComponent<PlayerControler>();
        pg = this.gameObject.GetComponent<PlayerGrab>();
        anim = this.gameObject.GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider col)
    {
    }

    void OnTriggerExit(Collider col)
    {
    }

    public void cantMove()
    {
        canMove = false;
    }

    public void freeMove()
    {
        canMove = true;
    }

    public void attackEnd(string msg)
    {
        if (msg == "endAttack")
            attacking = false;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    
        if(canMove)
        {
            if(Input.GetButtonDown("Attack"))
            {
                pc.stopMovement();
                pc.cantDoAnAction();
                pg.cantMove();

                anim.SetBool("attacking", true);

                attacking = true;
            }

            if(!attacking)
            {

                anim.SetBool("attacking", false);

                pc.freeAction();
                pg.freeMove();
            }
        }

	}
}
