using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    bool K = false;
    private void Update()
    {
        if (!K && this.GetComponent<Rigidbody>().velocity.y < Random.value * 2)
        {
            StartCoroutine(BreakOut());
            K = true;
        }
    }

    IEnumerator BreakOut()
    {
        this.transform.Find("Firework").gameObject.SetActive(true);
        this.transform.Find("Trail").GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(3F);
        DestroyImmediate(this.gameObject);
    }
}
