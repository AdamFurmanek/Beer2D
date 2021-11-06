using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesSpawner : MonoBehaviour
{
    public GameObject[] glasses;
    public GameObject[] bottles;
    public GameObject[] filledBottles;
    public GameObject straw;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Instantiate(glasses[0]);
        if (Input.GetKeyDown(KeyCode.W))
            Instantiate(glasses[1]);
        if (Input.GetKeyDown(KeyCode.E))
            Instantiate(glasses[2]);
        if (Input.GetKeyDown(KeyCode.R))
            Instantiate(glasses[3]);
        if (Input.GetKeyDown(KeyCode.T))
            Instantiate(glasses[4]);
        if (Input.GetKeyDown(KeyCode.Y))
            Instantiate(glasses[5]);

        if (Input.GetKeyDown(KeyCode.A))
            Instantiate(bottles[0]);
        if (Input.GetKeyDown(KeyCode.S))
            Instantiate(bottles[1]);
        if (Input.GetKeyDown(KeyCode.D))
            Instantiate(bottles[2]);
        if (Input.GetKeyDown(KeyCode.F))
            Instantiate(bottles[3]);

        if (Input.GetKeyDown(KeyCode.Z))
            Instantiate(filledBottles[0]);
        if (Input.GetKeyDown(KeyCode.X))
            Instantiate(filledBottles[1]);
        if (Input.GetKeyDown(KeyCode.C))
            Instantiate(filledBottles[2]);
        if (Input.GetKeyDown(KeyCode.V))
            Instantiate(filledBottles[3]);
        if (Input.GetKeyDown(KeyCode.B))
            Instantiate(filledBottles[4]);

        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(straw);
    }

}
