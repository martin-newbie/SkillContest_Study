using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDebug : MonoBehaviour
{
    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Debug.Log($"min : {rect.offsetMin}, max : {rect.offsetMax}");

        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log($"anchoredPosition : {rect.anchoredPosition}");
    }
}
