using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bezier
{
    public class Player : MonoBehaviour
    {
        public Transform[] targets;
        public BezierBullet bullet;

        bool isActive = false;

        private void Update()
        {
            if(!isActive && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        IEnumerator AttackCoroutine()
        {
            isActive = true;
            var wait = new WaitForSeconds(0.05f);

            int idx = 0;
            while (idx < 40)
            {
                var temp = Instantiate(bullet);
                temp.InitBullet(transform.position, targets[Random.Range(0, targets.Length)].position, Random.Range(0.5f, 1.2f));

                idx++;
                yield return wait;
            }

            isActive = false;
            yield break;
        }
    }
}
