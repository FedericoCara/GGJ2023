using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBackground : MonoBehaviour
{
    Material Material;
    private void Awake()
    {
        Material = GetComponent<Image>().material;
    }
    private void Update()
    {
        Material.mainTextureOffset += new Vector2(0.0005f, 0.0005f);
    }

}
