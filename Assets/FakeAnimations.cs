using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAnimations : MonoBehaviour
{
    public Animator anim;


    private bool isRunnning;
    private bool isJumping;
    void Start()
    {
        GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("Nlight");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("Slight");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("Dlight");
        }

        
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("Nheavy");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetTrigger("Sheavy");
        }
        








        if (Input.GetKeyDown(KeyCode.R))
        {
            isRunnning = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRunnning = false;
        }

        if (isRunnning == true)
        {
            anim.SetBool("Run", true);
        }
        else if (isRunnning == false)
        {
            anim.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isJumping = true;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isJumping = false;
        }

        if (isJumping == true)
        {
            anim.SetBool("Jump", true);
        }
        else if (isJumping == false)
        {
            anim.SetBool("Jump", false);
        }


    }
}
