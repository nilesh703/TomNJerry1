using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animchar;
    void Start()
    {
        animchar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            animchar.SetBool("anim", true);
            animchar.Play("RumbaDance");
        }
    }
}
