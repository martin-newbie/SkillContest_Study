using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lerp
{
    public class CircularSaw : MonoBehaviour
    {
        public Transform[] poses;
        public Transform saw;

        public float duration = 0f, maxDuration = 3f;
        int dir = 1;
        private void Update()
        {
            duration += dir * Time.deltaTime;

            if(duration > maxDuration || duration < 0)
            {
                dir *= -1;
            }

            saw.position = Vector3.Lerp(poses[0].position, poses[1].position, duration / maxDuration);
            saw.Rotate(new Vector3(0, 0, dir) * Time.deltaTime * 500f);
        }
    }
}
