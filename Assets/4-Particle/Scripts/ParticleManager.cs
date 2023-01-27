using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Particle
{
    public class ParticleManager : MonoBehaviour
    {

        public ParticleEffectClass[] particles;

        [Header("UI")]
        public Transform parent;
        public Button buttonPrefab;

        private void Start()
        {

            for (int i = 0; i < particles.Length; i++)
            {
                Button temp = Instantiate(buttonPrefab, parent);

                int index = i;
                temp.onClick.AddListener(() => PlayParticle(index));
                temp.GetComponentInChildren<Text>().text = particles[index].name;

                particles[index].Stop();
            }
            buttonPrefab.gameObject.SetActive(false);
        }

        void PlayParticle(int index)
        {
            foreach (var item in particles)
            {
                item.Stop();
            }

            particles[index].Play();
        }

    }
}
