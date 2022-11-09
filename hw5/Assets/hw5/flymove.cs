using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class flymove : MonoBehaviour
{
    // Start is called before the first frame update
    private flydata data;
    void Start()
    {
        data=gameObject.GetComponent<flydata>();
        data.direction=new Vector3(UnityEngine.Random.Range(0,2)-1,UnityEngine.Random.Range(0,2)-1,UnityEngine.Random.Range(0,2)-1);
        data.x_speed=UnityEngine.Random.Range(1,3);
        data.y_speed=UnityEngine.Random.Range(1,3);
        data.z_speed=UnityEngine.Random.Range(1,3);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position+=   Vector3.left*data.x_speed*Time.deltaTime*data.direction.x+
                                    Vector3.up*data.y_speed*Time.deltaTime*data.direction.y+
                                    Vector3.forward*data.z_speed*Time.deltaTime*data.direction.z;
    }
}
