using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bezier
{
    public class BezierBullet : MonoBehaviour
    {

        Vector3[] poses;

        public void InitBullet(Vector3 start, Vector3 end, float duration)
        {
            poses = new Vector3[3];

            poses[0] = start;
            poses[1] = start + (Vector3)Random.insideUnitCircle * Random.Range(1f, 4.5f);
            poses[2] = end;


            transform.position = poses[0];
            StartCoroutine(moveRoutine());

            IEnumerator moveRoutine()
            {
                float timer = 0f;

                while (timer < duration)
                {
                    transform.position = cubicBezier(poses[0], poses[1], poses[2], timer / duration);
                    timer += Time.deltaTime;
                    yield return null;
                }

                Destroy(gameObject);
            }
        }


        Vector3 cubicBezier(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            var ab = Vector3.Lerp(a, b, t);
            var bc = Vector3.Lerp(b, c, t);

            var abbc = Vector3.Lerp(ab, bc, t);
            return abbc;
        }
    }
}
