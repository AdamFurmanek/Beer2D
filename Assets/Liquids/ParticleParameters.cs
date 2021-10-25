using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleParameters
{
    public bool canFoam;
    public float foamAppearingTime;
    public float foamStayTime;
    public float foamDissolveTime;

    public bool canDiffuse;
    public float diffusingTime;
    public float diffuseIntensity;

    public static ParticleParameters Lerp(ParticleParameters a, ParticleParameters b, float t)
    {
        return new ParticleParameters()
        {
            canFoam = a.canFoam,
            foamAppearingTime = Mathf.Lerp(a.foamAppearingTime, b.foamAppearingTime, t),
            foamStayTime = Mathf.Lerp(a.foamStayTime, b.foamStayTime, t),
            foamDissolveTime = Mathf.Lerp(a.foamDissolveTime, b.foamDissolveTime, t),

            canDiffuse = a.canDiffuse,
            diffusingTime = Mathf.Lerp(a.diffusingTime, b.diffusingTime, t),
            diffuseIntensity = a.diffuseIntensity + (b.diffuseIntensity - a.diffuseIntensity) / 4, //special diffusing of intensity
        };
    }

    public static ParticleParameters Combine(ParticleParameters a, ParticleParameters b)
    {
        return Lerp(a, b, 0.5f);
    }

    public static ParticleParameters Copy(ParticleParameters p)
    {
        return new ParticleParameters()
        {
            canFoam = p.canFoam,
            foamAppearingTime = p.foamAppearingTime,
            foamStayTime = p.foamStayTime,
            foamDissolveTime = p.foamDissolveTime,

            canDiffuse = p.canDiffuse,
            diffusingTime = p.diffusingTime,
            diffuseIntensity = p.diffuseIntensity,
        };
    }
}
