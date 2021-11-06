using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserObject : MonoBehaviour
{
    public GameObject[] possibleLiquids;

    Dispenser dispenser;
    SpriteRenderer backSprite;
    int actualLiquid;

    private void Awake()
    {
        dispenser = GetComponentInChildren<Dispenser>();
        backSprite = GetComponentsInChildren<SpriteRenderer>()[1];

        SetParticle();

    }

    public void OnMouseEnter()
    {
        StartCoroutine(Flashing());
    }

    public void OnMouseExit()
    {
        flashing = false;
    }

    public void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            actualLiquid = 0;

            SetParticle();
        }
        else
        {
            actualLiquid++;
            if (actualLiquid >= possibleLiquids.Length)
                actualLiquid = 0;

            SetParticle();
        }
    }

    private void SetParticle()
    {
        if (possibleLiquids.Length > 0)
        {
            dispenser.liquid = possibleLiquids[actualLiquid];
            backSprite.color = possibleLiquids[actualLiquid].GetComponentInChildren<SpriteRenderer>().color;
        }
    }

    bool flashing;
    IEnumerator Flashing()
    {
        float t = 0;
        backSprite.color = possibleLiquids[actualLiquid].GetComponentInChildren<SpriteRenderer>().color;
        flashing = true;
        while (flashing)
        {
            t = Mathf.Repeat(t + Time.unscaledDeltaTime * 6f, Mathf.PI * 2f);
            float v = (Mathf.Sin(t) + 1f) / 3.5f;
            Color c = possibleLiquids[actualLiquid].GetComponentInChildren<SpriteRenderer>().color;
            c.a = c.a - v;
            backSprite.color = c;
            yield return null;
        }
        backSprite.color = possibleLiquids[actualLiquid].GetComponentInChildren<SpriteRenderer>().color;
    }

}
