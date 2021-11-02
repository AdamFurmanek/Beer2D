using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingBorder : MonoBehaviour
{
    public float scrollingSpeed;

    DraggingObject mainObject;
    AudioSource hitAudio;
    SpriteRenderer spriteRenderer;
    [HideInInspector] public bool dragging;
    [HideInInspector] public Vector2 offset;
    Vector2 lastPosition;
    Quaternion lastRotation;
    public GameObject trigger;

    private void Awake()
    {
        mainObject = transform.parent.GetComponentInChildren<DraggingObject>();
        hitAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DishBorder"))
            trigger = collision.gameObject;
    }

    bool stoppedFlag;


    public void Update()
    {
        if (dragging)
        {
            if (Input.mouseScrollDelta.y > 0)
                transform.Rotate(0, 0, scrollingSpeed);
            if (Input.mouseScrollDelta.y < 0)
                transform.Rotate(0, 0, -scrollingSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (dragging)
        {
            if(trigger == null)
            {
                spriteRenderer.enabled = false;

                mainObject.transform.position = lastPosition;
                mainObject.transform.rotation = lastRotation;

                lastPosition = transform.position;
                lastRotation = transform.rotation;
                stoppedFlag = true;
            }
            else
            {
                if (stoppedFlag)
                {
                    hitAudio.Play();
                    var triggerHitAudio = trigger.GetComponent<AudioSource>();
                    if (triggerHitAudio != null)
                        triggerHitAudio.Play();
                    stoppedFlag = false;
                }
                spriteRenderer.enabled = true;

                lastPosition = mainObject.transform.position;
                lastRotation = mainObject.transform.rotation;
            }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition - offset;
            transform.position += new Vector3(0, 0, 5);

            trigger = null;
        }
    }
}
