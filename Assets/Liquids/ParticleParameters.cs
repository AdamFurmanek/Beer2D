using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleParameters
{
    public Color color;
    public Vector2 scale;
    public float gravityScale;
    public float linearDrag;
    public float angularDrag;

    public static ParticleParameters Lerp(ParticleParameters a, ParticleParameters b, float t)
    {
        return new ParticleParameters()
        {
            color = Color.Lerp(a.color, b.color, t),
            scale = Vector2.Lerp(a.scale, b.scale, t),
            gravityScale = Mathf.Lerp(a.gravityScale, b.gravityScale, t),
            linearDrag = Mathf.Lerp(a.linearDrag, b.linearDrag, t),
            angularDrag = Mathf.Lerp(a.angularDrag, b.angularDrag, t),
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
            color = new Color(p.color.r, p.color.g, p.color.b, p.color.a ),
            scale = new Vector2(p.scale.x, p.scale.y),
            gravityScale = p.gravityScale,
            linearDrag = p.linearDrag,
            angularDrag = p.angularDrag
        };
    }
}
