using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingObject : MonoBehaviour
{
    DraggingBorder border;

    public void Awake()
    {
        border = transform.parent.GetComponentInChildren<DraggingBorder>();
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
    }
}
