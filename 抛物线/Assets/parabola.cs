using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola : MonoBehaviour
{
    int g=1;
    int speed=5;
    Vector3 beg;
    // Start is called before the first frame update
    void Start()
    {
        beg=this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.position=Vector3.left*speed*Time.time;
        this.transform.position=beg+Vector3.left*speed*Time.time-Vector3.up*Time.time*Time.time*g;
    }
}
