using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bezier
{
    public class BezierCurve : MonoBehaviour
    {
        [Range(0f, 1f)] public float t;
        public Transform[] poses;
        public Transform target;

        void Update()
        {
            Vector3 pos = new Vector3(
                cubicBezier(poses[0].position.x, poses[1].position.x, poses[2].position.x, poses[3].position.x, t),
                cubicBezier(poses[0].position.y, poses[1].position.y, poses[2].position.y, poses[3].position.y, t)
                );

            Vector3 vecPos = cubicBezierVec(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t);
            Vector3 nextVec = cubicBezierVec(poses[0].position, poses[1].position, poses[2].position, poses[3].position, t + Time.deltaTime);

            var dir = nextVec - vecPos;
            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            target.rotation = Quaternion.Euler(0, 0, z);

            target.position = vecPos;
        }

        float cubicBezier(float a, float b, float c, float d, float t)
        {
            float ab = Mathf.Lerp(a, b, t);
            float bc = Mathf.Lerp(b, c, t);
            float cd = Mathf.Lerp(c, d, t);

            float abbc = Mathf.Lerp(ab, bc, t);
            float bccd = Mathf.Lerp(bc, cd, t);

            return Mathf.Lerp(abbc, bccd, t);
        }

        Vector3 cubicBezierVec(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            var ab = Vector3.Lerp(a, b, t);
            var bc = Vector3.Lerp(b, c, t);
            var cd = Vector3.Lerp(c, d, t);

            var abbc = Vector3.Lerp(ab, bc, t);
            var bccd = Vector3.Lerp(bc, cd, t);

            return Vector3.Lerp(abbc, bccd, t);
        }
    }
}