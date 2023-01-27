using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cam
{
    public class CamToRay : MonoBehaviour
    {
        public Text text;

        void Update()
        {
            CheckRay();
        }

        void CheckRay()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                if (hit)
                {
                    text.text = hit.transform.gameObject.name;
                }
            }
        }
    }
}
