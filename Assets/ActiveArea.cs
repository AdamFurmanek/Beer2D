using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveArea : MonoBehaviour
{
    public float intensity;

    AudioSource[] pourOnFloorSounds;
    int lastSound;

    public void Awake()
    {
        pourOnFloorSounds = GetComponents<AudioSource>();
    }

    public void Update()
    {
        intensity -= Time.deltaTime * 2;
        intensity = Mathf.Max(0, intensity);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Liquid"))
        {
            Destroy(collision.gameObject);
            CalculateSounds();
        }
    }

    void CalculateSounds()
    {
        intensity += 1;
        intensity = Mathf.Min(100, intensity);

        if (intensity > 5)
        {
            if (!pourOnFloorSounds[lastSound].isPlaying)
            {
                pourOnFloorSounds[lastSound].PlayDelayed(Random.value);
                lastSound++;
                if (lastSound >= pourOnFloorSounds.Length)
                {
                    lastSound = 0;
                    System.Random random = new System.Random();
                    pourOnFloorSounds = pourOnFloorSounds.OrderBy(x => random.Next()).ToArray();
                }
                intensity -= 5;
            }
        }
    }
}
