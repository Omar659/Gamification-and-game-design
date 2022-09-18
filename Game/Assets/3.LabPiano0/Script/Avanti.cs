using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avanti : MonoBehaviour
{
    public bool premutow = false;
    public bool premutoa = false;
    public bool premutos = false;
    public bool premutod = false;
    private Quaternion rotazione;
    private float altezza;
    public Animator animator;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        //rotazione = transform.rotation;
        altezza = transform.position.y +0.4f;
        rigidBody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = rotazione;
        if (Mathf.Abs(transform.position.y - altezza) > 0.1f)
        {
            transform.position = new Vector3(transform.position.x, altezza, transform.position.z);

        }

        if (Input.GetKeyDown("w"))
        {
            premutow = true;
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKeyUp("w"))
        {
            premutow = false;
            animator.SetBool("isRunning", false);
        }
        if (premutow)
        {
            rigidBody.transform.position += Vector3.forward / 10f;
        }


        if (Input.GetKeyDown("a"))
        {
            premutoa = true;
        }

        if (Input.GetKeyUp("a"))
        {
            premutoa = false;
        }
        if (premutoa)
        {
            transform.position += Vector3.left / 10f;
        }


        if (Input.GetKeyDown("s"))
        {
            premutos = true;
        }

        if (Input.GetKeyUp("s"))
        {
            premutos = false;
        }
        if (premutos)
        {
            transform.position += Vector3.back / 10f;
        }


        if (Input.GetKeyDown("d"))
        {
            premutod = true;
        }

        if (Input.GetKeyUp("d"))
        {
            premutod = false;
        }
        if (premutod)
        {
            transform.position += Vector3.right / 10f;
        }

    }
}
