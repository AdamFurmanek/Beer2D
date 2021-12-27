using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleAppearance liquid;
    public ParticleAppearance foam;
    public ParticleParameters parameters;

    SpriteRenderer particleRenderer;
    Rigidbody2D particleRigidbody;

    [HideInInspector] public bool isFoam = false;
    bool diffusing = false;

    static int lol = 0;

    private void Awake()
    {
        var mask = transform.GetChild(0).GetComponent<SpriteMask>();
        mask.frontSortingOrder = lol;
        particleRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        lol++;
        particleRenderer.sortingOrder = lol;
        particleRigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(ParticleCRT());
    }

    private void SetParameters(ParticleAppearance p)
    {
        particleRenderer.color = p.color;
        transform.localScale = p.scale;
        particleRigidbody.gravityScale = p.gravityScale;
        particleRigidbody.drag = p.linearDrag;
        particleRigidbody.angularDrag = p.angularDrag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.sqrMagnitude > 1000 && parameters.canFoam && !isFoam)
        {
            isFoam = true;
        }

        if (collision.gameObject.CompareTag(tag) && !diffusing)
        {
            StartCoroutine(Diffuse(collision.gameObject.GetComponent<Particle>()));
        }
    }

    private IEnumerator ParticleCRT()
    {
        while (true)
        {
            if (!isFoam)
            {
                yield return Liquid();
            }
            else
            {
                yield return Foam();
                isFoam = false;
            }
        }
    }

    private IEnumerator Liquid()
    {
        SetParameters(liquid);
        yield return null;
    }

    private IEnumerator Foam()
    {
        float t = 0;
        float appearingTime = parameters.foamAppearingTime;
        float stayTime = parameters.foamStayTime;
        float dissolveTime = parameters.foamDissolveTime;

        while (t < 1)
        {
            SetParameters(ParticleAppearance.Lerp(liquid, foam, t));

            t += Time.deltaTime / appearingTime;
            yield return null;
        }

        yield return new WaitForSeconds(stayTime);

        t = 0;
        while (t < 1)
        {
            SetParameters(ParticleAppearance.Lerp(foam, liquid, t));

            t += Time.deltaTime / dissolveTime;
            yield return null;
        }
    }

    private IEnumerator Diffuse(Particle other)
    {
        if (!parameters.canDiffuse || !other.parameters.canDiffuse)
            yield break;

        diffusing = true;

        ParticleAppearance originalLiquid = ParticleAppearance.Copy(liquid);
        ParticleAppearance destinationLiquid = ParticleAppearance.Lerp(liquid, other.liquid, parameters.diffuseIntensity);

        ParticleAppearance originalFoam = ParticleAppearance.Copy(foam);
        ParticleAppearance destinationFoam = ParticleAppearance.Lerp(foam, other.foam, parameters.diffuseIntensity);

        ParticleParameters originalParameters = ParticleParameters.Copy(parameters);
        ParticleParameters destinationParameters = ParticleParameters.Lerp(parameters, other.parameters, parameters.diffuseIntensity);

        float t = 0;
        float diffusingTime = parameters.diffusingTime;
        while(t < 1)
        {
            liquid = ParticleAppearance.Lerp(originalLiquid, destinationLiquid, t);

            foam = ParticleAppearance.Lerp(originalFoam, destinationFoam, t);

            parameters = ParticleParameters.Lerp(originalParameters, destinationParameters, t);

            t += Time.deltaTime / diffusingTime;
            yield return null;
        }

        diffusing = false;
    }

}
