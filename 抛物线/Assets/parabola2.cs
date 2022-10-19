using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola2 : MonoBehaviour
{
    float yspeed=0;
    int g=1;
    int xspeed=5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yspeed=g*Time.time;
        this.transform.Translate(Vector3.left*xspeed*Time.deltaTime);
        this.transform.Translate(-Vector3.up*(yspeed*Time.deltaTime+g/2*Time.deltaTime*Time.deltaTime));
    }
}
