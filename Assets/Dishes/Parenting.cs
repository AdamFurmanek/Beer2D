using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenting : MonoBehaviour
{
    public float maxPouringVolume;

    private ActiveArea activeArea;
    private GameObject specialParent;
    private AudioSource pouringAudio;

    public void Awake()
    {
        activeArea = FindObjectOfType<ActiveArea>();
        specialParent = transform.Find("Particles").gameObject;

        var inverseScale = specialParent.transform.localScale;
        inverseScale.x = 1f / transform.localScale.x;
        inverseScale.y = 1f / transform.localScale.y;
        specialParent.transform.localScale = inverseScale;
        pouringAudio = GetComponents<AudioSource>()[0];
    }

    void Update()
    {
        pouringAudio.volume -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
        {
            pouringAudio.volume += Time.deltaTime * 2;
            pouringAudio.volume = Mathf.Min(maxPouringVolume, pouringAudio.volume);
            collision.transform.SetParent(specialParent.transform);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
            collision.transform.SetParent(activeArea.transform);
    }
}
