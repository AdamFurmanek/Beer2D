using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenting : MonoBehaviour
{
    private GameObject defaultParent;
    private GameObject specialParent;

    public float maxAudioVolume;
    private AudioSource pouring;

    public void Awake()
    {
        defaultParent = GameObject.Find("ParticlesParent");
        specialParent = transform.Find("Particles").gameObject;

        var inverseScale = specialParent.transform.localScale;
        inverseScale.x = 1f / transform.localScale.x;
        inverseScale.y = 1f / transform.localScale.y;
        specialParent.transform.localScale = inverseScale;
        pouring = GetComponent<AudioSource>();
    }

    void Update()
    {
        pouring.volume -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
        {
            pouring.volume += Time.deltaTime * 2;
            pouring.volume = Mathf.Min(maxAudioVolume, pouring.volume);
            collision.transform.SetParent(specialParent.transform);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
            collision.transform.SetParent(defaultParent.transform);
    }
}
