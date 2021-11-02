using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenting : MonoBehaviour
{
    private GameObject defaultParent;
    private GameObject specialParent;

    public void Awake()
    {
        defaultParent = GameObject.Find("ParticlesParent");
        specialParent = transform.Find("Particles").gameObject;

        var inverseScale = specialParent.transform.localScale;
        inverseScale.x = 1f / transform.localScale.x;
        inverseScale.y = 1f / transform.localScale.y;
        specialParent.transform.localScale = inverseScale;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
            collision.transform.SetParent(specialParent.transform);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
            collision.transform.SetParent(defaultParent.transform);
    }
}
