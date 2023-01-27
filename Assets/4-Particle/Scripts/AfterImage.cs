using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Particle
{
    public class AfterImage : ParticleEffectClass
    {
        public Transform[] poses;
        Coroutine routine;

        public override void Stop()
        {
            if (routine != null)
                StopCoroutine(routine);
            base.Stop();
        }

        public override void Play()
        {
            base.Play();
            StartCoroutine(Move(1f));
        }

        IEnumerator Move(float duration)
        {
            float timer = 0f;

            while (timer < duration)
            {
                particle.transform.position = Vector3.Lerp(poses[0].position, poses[1].position, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            Stop();
            yield break;
        }
    }
}
