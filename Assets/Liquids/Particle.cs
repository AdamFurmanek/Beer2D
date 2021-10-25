using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    SpriteRenderer particleRenderer;
    Rigidbody2D particleRigidbody;
    public ParticleParameters liquidParameters;
    public ParticleParameters foamParameters;

    bool isFoam = false;
    bool diffusing = false;

    private void Awake()
    {
        
        particleRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        particleRigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(ParticleCRT());
    }

    private void SetParameters(ParticleParameters p)
    {
        particleRenderer.color = p.color;
        transform.localScale = p.scale;
        particleRigidbody.gravityScale = p.gravityScale;
        particleRigidbody.drag = p.linearDrag;
        particleRigidbody.angularDrag = p.angularDrag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.sqrMagnitude > 1000 && !isFoam)
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
        SetParameters(liquidParameters);
        yield return null;
    }

    private IEnumerator Foam()
    {
        float t = 0;
        while(t < 1)
        {
            SetParameters(ParticleParameters.Lerp(liquidParameters, foamParameters, t));

            t += Time.deltaTime / 1;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        t = 0;
        while (t < 1)
        {
            SetParameters(ParticleParameters.Lerp(foamParameters, liquidParameters, t));

            t += Time.deltaTime / 1;
            yield return null;
        }
    }

    private IEnumerator Diffuse(Particle other)
    {
        diffusing = true;

        ParticleParameters liquidOriginal = ParticleParameters.Copy(liquidParameters);
        ParticleParameters liquidDestination = ParticleParameters.Combine(liquidParameters, other.liquidParameters);

        ParticleParameters foamOriginal = ParticleParameters.Copy(foamParameters);
        ParticleParameters foamDestination = ParticleParameters.Combine(foamParameters, other.foamParameters);

        float t = 0;
        while(t < 1)
        {
            liquidParameters = ParticleParameters.Lerp(liquidOriginal, liquidDestination, t);

            foamParameters = ParticleParameters.Lerp(foamOriginal, foamDestination, t);

            t += Time.deltaTime / 0.1f;
            yield return null;
        }

        diffusing = false;
    }
}
