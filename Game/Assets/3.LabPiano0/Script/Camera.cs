using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //public static int livello0 = 0;
    public static Vector3 posizione = new Vector3(0f, 0.489f, -186f);
    private Quaternion tuamamma;
    public GameObject lutrario;
    // Start is called before the first frame update
    void Start()
    {
        lutrario.transform.position = posizione;
        //tuamamma = transform.rotation;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //transform.rotation = tuamamma;
    //}
}
