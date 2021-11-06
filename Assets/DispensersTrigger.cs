using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispensersTrigger : MonoBehaviour
{
    Transform[] dispenserObjects;

    void Awake()
    {
        dispenserObjects = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            dispenserObjects[i] = transform.GetChild(i);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
            TriggerDispenser(0);
        if (Input.GetKey(KeyCode.Alpha2))
            TriggerDispenser(1);
        if (Input.GetKey(KeyCode.Alpha3))
            TriggerDispenser(2);
        if (Input.GetKey(KeyCode.Alpha4))
            TriggerDispenser(3);
        if (Input.GetKey(KeyCode.Alpha5))
            TriggerDispenser(4);
    }

    void TriggerDispenser(int id)
    {
        foreach (var dispenser in dispenserObjects[id].GetComponentsInChildren<Dispenser>())
            dispenser.Pour();
    }
}
