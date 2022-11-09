using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola3 : MonoBehaviour
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
        this.transform.position+=Vector3.left*Time.deltaTime*xspeed;
        this.transform.position-=Vector3.up*(yspeed*Time.deltaTime+g*Time.deltaTime*Time.deltaTime);
    }
}
