using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingObject : MonoBehaviour
{
    DraggingBorder border;
    AudioSource hitAudio;

    public void Awake()
    {
        border = transform.parent.GetComponentInChildren<DraggingBorder>();
        //hitAudio = transform.parent.GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        border.offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
        border.dragging = true;
    }

    private void OnMouseUp()
    {
        border.transform.position = transform.position;
        border.transform.rotation = transform.rotation;
        border.transform.position += new Vector3(0, 0, 5);
        border.dragging = false;
        border.trigger = null;
        //hitAudio.Play();
    }
}
