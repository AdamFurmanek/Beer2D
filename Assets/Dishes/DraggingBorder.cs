using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingBorder : MonoBehaviour
{
    public float scrollingSpeed;

    DraggingObject mainObject;
    SpriteRenderer spriteRenderer;
    [HideInInspector] public bool dragging;
    [HideInInspector] public Vector2 offset;
    Vector2 lastPosition;
    Quaternion lastRotation;
    int triggers;

    private void Awake()
    {
        mainObject = transform.parent.GetComponentInChildren<DraggingObject>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DishBorder"))
            triggers++;
    }

    private void FixedUpdate()
    {
        if (dragging)
        {
            if(triggers == 0)
            {
                spriteRenderer.enabled = false;

                mainObject.transform.position = lastPosition;
                mainObject.transform.rotation = lastRotation;

                lastPosition = transform.position;
                lastRotation = transform.rotation;
            }
            else
            {
                spriteRenderer.enabled = true;

                lastPosition = mainObject.transform.position;
                lastRotation = mainObject.transform.rotation;
            }

            if (Input.mouseScrollDelta.y > 0)
                transform.Rotate(0, 0, scrollingSpeed);
            if (Input.mouseScrollDelta.y < 0)
                transform.Rotate(0, 0, -scrollingSpeed);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition - offset;
            transform.position += new Vector3(0, 0, 5);

            triggers = 0;
        }
    }
}
