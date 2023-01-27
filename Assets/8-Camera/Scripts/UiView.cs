using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cam
{
    public class UiView : MonoBehaviour
    {
        public RectTransform mousePoint;
        public RectTransform imageRect;

        public void OnDrag()
        {
            mousePoint.anchoredPosition = Input.mousePosition - new Vector3(imageRect.sizeDelta.x / 2, imageRect.sizeDelta.y / 2);
        }
    }

}
