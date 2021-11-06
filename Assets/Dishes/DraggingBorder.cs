using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingBorder : MonoBehaviour
{
    public float scrollingSpeed;
    public float movingSpeedAudioTrigger;

    DraggingObject mainObject;
    AudioSource hitAudio;
    AudioSource moveAudio;
    SpriteRenderer spriteRenderer;
    [HideInInspector] public bool dragging;
    [HideInInspector] public Vector2 offset;
    Vector2 lastPosition;
    Quaternion lastRotation;
    [HideInInspector] public GameObject trigger;
    bool stoppedFlag;
    float toScroll;

    private void Awake()
    {
        mainObject = transform.parent.GetComponentInChildren<DraggingObject>();
        hitAudio = GetComponents<AudioSource>()[0];
        moveAudio = GetComponents<AudioSource>()[1];
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DishBorder"))
            trigger = collision.gameObject;
    }

    public void Update()
    {
        if (dragging)
        {
            if (Input.mouseScrollDelta.y > 0)
                toScroll = scrollingSpeed;
            if (Input.mouseScrollDelta.y < 0)
                toScroll = -scrollingSpeed;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, toScroll);
        toScroll = 0;

        if (dragging)
        {
            if(trigger == null)
            {
                var distance = Vector2.Distance(lastPosition, transform.position);
                if (distance > movingSpeedAudioTrigger && !moveAudio.isPlaying)
                    moveAudio.Play();

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
                    moveAudio.Stop();
                    hitAudio.Play();
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
