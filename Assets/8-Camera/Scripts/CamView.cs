using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cam
{
    public class CamView : MonoBehaviour
    {

        public Transform mousePoint;

        void Update()
        {
            FollowTouchPos();
        }

        void FollowTouchPos()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePoint.position = touchPos + new Vector3(0, 0, 10);
            }
        }
    }
}
