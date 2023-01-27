using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lerp
{
    public class FollowingCam : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public Transform target;

        void Update()
        {
            if (target == null) return;

            transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 0, -10f), Time.deltaTime * moveSpeed);
        }
    }

}
