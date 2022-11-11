using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solar : MonoBehaviour
{
    public GameObject earth;
    public GameObject moon;
    public GameObject mars;
    public GameObject mercury;
    public GameObject venus;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;
    // Start is called before the first frame update
    void Start()
    {
        earth=GameObject.Find("earth");
        moon=GameObject.Find("moon");
        mars=GameObject.Find("mars");
        mercury=GameObject.Find("mercury");
        venus=GameObject.Find("venus");
        jupiter=GameObject.Find("jupiter");
        saturn=GameObject.Find("saturn");
        uranus=GameObject.Find("uranus");
        neptune=GameObject.Find("neptune");
    }

    // Update is called once per frame
    void Update()
    {
        earth.transform.RotateAround(Vector3.zero,new Vector3(0.1f, 1, 0), 60 * Time.deltaTime);
        moon.transform.RotateAround(earth.transform.position,new Vector3(0.1f, 3, 0),120 * Time.deltaTime);
        mercury.transform.RotateAround(Vector3.zero, new Vector3(0.1f, 1, 0), 60 * Time.deltaTime);
        venus.transform.RotateAround(Vector3.zero, new Vector3(0, 1, -0.1f), 55 * Time.deltaTime);
        mars.transform.RotateAround(Vector3.zero, new Vector3(0.2f, 1, 0), 45 * Time.deltaTime);
        jupiter.transform.RotateAround(Vector3.zero, new Vector3(-0.1f, 2, 0), 35 * Time.deltaTime);
        saturn.transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0.2f), 20 * Time.deltaTime);
        neptune.transform.RotateAround(Vector3.zero, new Vector3(-0.1f, 1, -0.1f), 10 * Time.deltaTime);
        uranus.transform.RotateAround(Vector3.zero, new Vector3(0, 2, 0.1f), 15 * Time.deltaTime);
    }
}
