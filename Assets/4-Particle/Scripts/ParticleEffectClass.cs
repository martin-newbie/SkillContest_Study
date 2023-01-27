using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Particle
{
    public class ParticleEffectClass : MonoBehaviour
    {
        public ParticleSystem particle;

        public virtual void Stop()
        {
            gameObject.SetActive(false);
            particle.Stop();
        }

        public virtual void Play()
        {
            gameObject.SetActive(true);
            particle.Play();
        }
    }
}
