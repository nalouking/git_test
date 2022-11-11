using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.value * 0.6F + 0.2F);
            Transform T = Instantiate(GameObject.Find("Space").transform.Find("Firework"), GameObject.Find("Space").transform);
            T.GetComponent<Rigidbody>().velocity = new Vector3(Random.value * 5 - 2.5F, Random.value * 8 + 12, Random.value * 5 - 2.5F);
            T.gameObject.AddComponent<Firework>();

            Color C = new Color(Random.value * 0.7F + 0.4F, Random.value * 0.7F + 0.4F, Random.value * 0.7F + 0.4F);
            ParticleSystem.MainModule M = T.Find("Trail").GetComponent<ParticleSystem>().main;
            M.startColor = C;
            M = T.transform.Find("Firework").GetComponent<ParticleSystem>().main;
            M.startColor = C;

            T.gameObject.SetActive(true);
        }
    }
}
