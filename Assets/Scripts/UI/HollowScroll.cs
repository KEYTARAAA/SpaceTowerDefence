using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HollowScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 1;
    TextMeshProUGUI t;
    
    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        t.fontSharedMaterial.SetTextureOffset(ShaderUtilities.ID_FaceTex, new Vector2(0, offset));
        t.fontSharedMaterial.SetTextureOffset(ShaderUtilities.ID_OutlineTex, new Vector2(0, offset));
    }
}
