using Assets.Scripts.Interfaces.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static statics;
using System;

public class MaterialManager : MonoBehaviour, Observer
{
    [SerializeField] bool observeMain = true, changer = true, self = false;
    [SerializeField] MATERIALLEVELS level = MATERIALLEVELS.REGULAR;
    [SerializeField] List<Renderer> parts;
    [SerializeField] List<FixParticleGradient> particleGradients;
    [SerializeField] List<ParticleSystem> particles;
    [SerializeField] List<LineRenderer> lines;
    [SerializeField] List<Image> images;
    [SerializeField] List<TextMeshProUGUI> textboxes;
    [SerializeField] TrailRenderer trail;
    string subjectName = "Material";
    private void Start()
    {
        if (changer) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            (player.transform.Find(subjectName).GetComponent(typeof(Subject)) as Subject).AddObserver(OBSERVERTYPES.MATERIAL, this);
        }
    }
    public List<Renderer> getParts()
    {
        return parts;
    }

    public void AddPart(Renderer r)
    {
        parts.Add(r);
    }

    public void addMaterial(Material m, bool all, bool overrider)
    {
        if ((all || self ) && !overrider) {
            switch (level)
            {
                case (MATERIALLEVELS.REGULAR):
                    m = PlayerMaterialManager.getHDRMaterial();
                    break;
                case (MATERIALLEVELS.MID):
                    m = PlayerMaterialManager.getHDRMidMaterial();
                    break;
                case (MATERIALLEVELS.DIM):
                    m = PlayerMaterialManager.getHDRDimMaterial();
                    break;
                case (MATERIALLEVELS.INTENSITY2):
                    m = PlayerMaterialManager.getHDRIntensity2Material();
                    break;
                case (MATERIALLEVELS.BLOCKHDR4):
                    m = PlayerMaterialManager.getBlockHDR4();
                    break;
                case (MATERIALLEVELS.PULSEHDR2To15):
                    m = PlayerMaterialManager.getPulseHDR2To15();
                    break;
            }

        }
        foreach (Renderer r in parts)
        {
            if (r != null) {
                r.material = m;
                MaterialManager mm;
                if (r.TryGetComponent<MaterialManager>(out mm) && r.name != name)
                {
                    mm.addMaterial(m, all, overrider);
                }
            }
        } 
        foreach (FixParticleGradient pg in particleGradients)
        {
            if (pg != null)
            {
                pg.fix(m);
            }
        }
        if (m.HasProperty("_EmissionColor"))
        {
            foreach (ParticleSystem ps in particles)
            {
                fixParticleMaterial(ps.transform,m);
            }

        }

        if (trail != null && m.HasProperty("_EmissionColor"))
        {
            trail.sharedMaterial.SetColor("_EmissionColor", m.GetColor("_EmissionColor"));
        }

        foreach (Image i in images )
        {
            i.material.SetColor("_Color", PlayerMaterialManager.getHDRMaterial().GetColor("_EmissionColor"));
            i.material.SetColor("_EmissionColor", PlayerMaterialManager.getHDRMaterial().GetColor("_EmissionColor"));
        }
        foreach (TextMeshProUGUI t in textboxes )
        {
            t.color = m.GetColor("_EmissionColor");
            t.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, PlayerMaterialManager.getHDRIntensity2Material().GetColor("_EmissionColor"));
        }


        if (m.HasProperty("_EmissionColor"))
        {
            foreach (LineRenderer lr in lines)
            {
                lr.startColor = m.GetColor("_EmissionColor");
                lr.endColor = m.GetColor("_EmissionColor");

                if (lr.sharedMaterial.HasProperty("_Color"))
                {
                    lr.sharedMaterial.SetColor("_EmissionColor", PlayerMaterialManager.getHDRDimMaterial().GetColor("_EmissionColor"));
                }
                if (lr.sharedMaterial.HasProperty("_EmissionColor"))
                {
                    lr.sharedMaterial.color = PlayerMaterialManager.getHDRDimMaterial().GetColor("_EmissionColor");
                }
            }
        }
    }

    public void UpdateObserver<T>(T info)
    {
        addMaterial(info as Material, (true && observeMain), false);
    }

    private void OnDestroy()
    {
        try
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            (player.transform.Find(subjectName).GetComponent(typeof(Subject)) as Subject).RemoveObserver(OBSERVERTYPES.MATERIAL, this);
        }
        catch (Exception e)
        {

        }
    }

    private void fixParticleMaterial(Transform t, Material m)
    {
        foreach (Transform child in t)
        {
            fixParticleMaterial(child,m);
        }
        ParticleSystem ps = t.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = m.GetColor("_EmissionColor");
        if (ps.GetComponent<ParticleSystemRenderer>().sharedMaterial.HasProperty("_EmissionColor"))
        {
            ps.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = PlayerMaterialManager.getHDRIntensity2Material().GetColor("_EmissionColor");
        }

        if (ps.GetComponent<ParticleSystemRenderer>().sharedMaterial.HasProperty("_Color"))
        {
            ps.GetComponent<ParticleSystemRenderer>().sharedMaterial.SetColor("_EmissionColor", PlayerMaterialManager.getHDRIntensity2Material().GetColor("_EmissionColor"));
        }
    }

    public void addParticleGradient(FixParticleGradient fixParticleGradient)
    {
        if (!particleGradients.Contains(fixParticleGradient))
        {
            particleGradients.Add(fixParticleGradient);
        }
    }
}
