using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class RandomMaterialManager : MonoBehaviour
{
    [SerializeField] Object[] materials;
    [SerializeField] string materialFolder;
    // Start is called before the first frame update
    void Start()
    {

        materials = Resources.LoadAll(materialFolder, typeof(Material));
        Material m = materials[Random.Range(0, materials.Length)] as Material;
        GetComponent<MaterialManager>().addMaterial(m, false, false);
    }
}
