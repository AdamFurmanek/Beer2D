using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DraggingObject : MonoBehaviour
{
    public float maxFoamingVolume;

    DraggingBorder border;
    AudioSource putAudio;
    AudioSource foamAudio;

    public void Awake()
    {
        border = transform.parent.GetComponentInChildren<DraggingBorder>();
        putAudio = GetComponents<AudioSource>()[1];
        foamAudio = GetComponents<AudioSource>()[2];
    }

    private void Update()
    {
        int count = 0;
        float intensity = 0;
        foreach(var foam in GetComponentsInChildren<Particle>())
        {
            if (foam.isFoam)
            {
                count++;
                intensity += foam.parameters.foamVolume;
            }
        }
        float foamParticles = intensity / 100 * maxFoamingVolume;
        foamAudio.volume = Mathf.Min(maxFoamingVolume, foamParticles);
    }

    public void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            border.offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            border.dragging = true;
        }
    }

    private void OnMouseUp()
    {
        border.transform.position = transform.position;
        border.transform.rotation = transform.rotation;
        border.transform.position += new Vector3(0, 0, 5);
        border.dragging = false;
        border.trigger = null;
        putAudio.Play();
    }
}
