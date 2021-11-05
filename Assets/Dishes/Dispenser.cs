using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public GameObject liquid;
    public GameObject parent;
    public bool enable;
    public float force;
    public float frequency;
    public float maxAudioVolume;

    private bool busy;
    private AudioSource pouring;

    private void Awake()
    {
        pouring = GetComponent<AudioSource>();
    }

    void Update()
    {
        pouring.volume -= Time.deltaTime;
        if(enable && Input.GetKey(KeyCode.Space))
        {
            pouring.volume += Time.deltaTime * 2;
            pouring.volume = Mathf.Min(maxAudioVolume, pouring.volume);
            if (!busy)
                StartCoroutine(Pour());
        }
    }

    IEnumerator Pour()
    {
        busy = true;

        var go = Instantiate(liquid);
        go.transform.SetParent(parent.transform);
        go.transform.position = transform.position;

        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
        go.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1.0f / frequency);

        busy = false;
    }
}
