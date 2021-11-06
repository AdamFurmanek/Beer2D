using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public GameObject liquid;
    public bool enable;
    public float force;
    public float frequency;
    public float maxPouringVolume;

    bool busy;
    AudioSource pouring;
    ActiveArea activeArea;

    private void Awake()
    {
        pouring = GetComponent<AudioSource>();
        activeArea = FindObjectOfType<ActiveArea>();
    }

    void Update()
    {
        pouring.volume -= Time.deltaTime;
    }

    public void Pour()
    {
        if (enable)
        {
            pouring.volume += Time.deltaTime * 2;
            pouring.volume = Mathf.Min(maxPouringVolume, pouring.volume);
            if (!busy)
                StartCoroutine(PourCRT());
        }
    }

    IEnumerator PourCRT()
    {
        busy = true;

        var go = Instantiate(liquid);
        go.transform.SetParent(activeArea.transform);
        go.transform.position = transform.position;

        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
        go.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1.0f / frequency);

        busy = false;
    }
}
