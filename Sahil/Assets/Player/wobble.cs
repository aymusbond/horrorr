using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wobble : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isWalking)
        {
            anim.SetBool("isWobble", true);
        }
        else
        {
            anim.SetBool("isWobble", false);
        }
    }
}
