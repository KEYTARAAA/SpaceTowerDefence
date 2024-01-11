using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FixParticleTrailGradient : FixParticleGradient
{
    [SerializeField] MaterialManager mm;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        colKeys = ps.colorOverLifetime.color.gradient.colorKeys;
        mm.addParticleGradient(this);

    }
    public override void fix(Material m)
    {
        List<GradientColorKey> colourKeys = new List<GradientColorKey>();
        bool added;
        for (int i = 0; i < colKeys.Length; i++)
        {
            GradientColorKey key = colKeys[i];
            added = false;
            if (all)
            {
                if (m.name.ToUpper().Contains("ORANGE"))
                {
                    Color c = new Color((225 / 225), (125 / 225), 0);
                    ColorUtility.TryParseHtmlString("#FF7D00", out c);
                    colourKeys.Add(new GradientColorKey(c, key.time));
                }
                else
                {
                    colourKeys.Add(new GradientColorKey(m.GetColor("_EmissionColor"), key.time));
                }
                added = true;
            }
            else {
                foreach (float f in sections)
                {
                    if (key.time.ToString("#.000") == f.ToString("#.000"))
                    {
                        if (m.name.ToUpper().Contains("ORANGE"))
                        {
                            Color c = new Color((225 / 225), (125 / 225), 0);
                            ColorUtility.TryParseHtmlString("#FF7D00", out c);
                            colourKeys.Add(new GradientColorKey(c, key.time));
                        }
                        else
                        {
                            colourKeys.Add(new GradientColorKey(m.GetColor("_EmissionColor"), key.time));
                        }
                        added = true;
                    }
                }
            }

            if (!added)
            {
                colourKeys.Add(key);
            }

        }

        Gradient grad = new Gradient();
        grad.SetKeys(colourKeys.ToArray(), ps.colorOverLifetime.color.gradient.alphaKeys);

        var col = ps.trails;
        col.colorOverLifetime = grad;
        col.colorOverTrail = grad;

    }

}
