using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShootingRange
{
    public class ShootingRange : MonoBehaviour
    {
        public GameObject bullet;

        [Header("Objects")]
        public Transform[] targets;

        [Header("UI")]
        public Transform shoot_parent;
        public Button shoot_buttonPrefab;
        public Transform target_parent;
        public Button target_buttonPrefab;

        bool isDone = true;

        Transform curTarget = null;

        private void Start()
        {
            curTarget = targets[0];

            for (int i = 0; i < 9; i++)
            {
                Button temp = Instantiate(shoot_buttonPrefab, shoot_parent);

                int index = i;
                temp.onClick.AddListener(() => Shoot(index));
                temp.GetComponentInChildren<Text>().text = $"shootingRange_{i}";
            }

            for (int i = 0; i < targets.Length; i++)
            {
                Button temp = Instantiate(target_buttonPrefab, target_parent);

                int index = i;
                temp.onClick.AddListener(() => SetTarget(index));
                temp.GetComponentInChildren<Text>().text = $"target_{i}";
            }

            shoot_buttonPrefab.gameObject.SetActive(false);
            target_buttonPrefab.gameObject.SetActive(false);
        }

        void SetTarget(int index)
        {
            curTarget = targets[index];
        }


        void Shoot(int index)
        {
            if (!isDone) return;

            switch (index)
            {
                case 0:
                    CircleShot(10);
                    break;
                case 1:
                    CircleDelayShot(10);
                    break;
                case 2:
                    TargetingShot();
                    break;
                case 3:
                    RoundTargetShot(10);
                    break;
                case 4:
                    RoundTargetDelayShot(10);
                    break;
                case 5:
                    SectorFormShot(5, 45);
                    break;
                case 6:
                    SectorFormTargetShot(5, 45);
                    break;
                case 7:
                    SectorFormDelayShot(5, 45, 30, 5, 10);
                    break;
                case 8:
                    MovingCircleShot(10);
                    break;
                default:
                    break;
            }
        }

        void CircleShot(int count)
        {
            for (int i = 0; i < 360; i += 360 / count)
            {
                Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, i));
            }
        }

        void CircleDelayShot(int count)
        {
            StartCoroutine(shotCoroutine());

            IEnumerator shotCoroutine()
            {
                isDone = false;

                for (int i = 0; i < 360; i += 360 / count)
                {
                    Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, i));
                    yield return new WaitForSeconds(0.1f);
                }

                isDone = true;
                yield break;
            }
        }

        void TargetingShot()
        {
            float z = Mathf.Atan2(curTarget.position.y, curTarget.position.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, z);
            Instantiate(bullet, new Vector3(0, 0, 0), rot);
        }

        void RoundTargetShot(int count)
        { // Vector3.zero는 시작지점으로 치환
            float radius = 1f;
            for (int i = 0; i < 360; i += 360 / count)
            {
                Vector3 pos = Vector3.zero + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);

                Vector2 nor = ((curTarget.position - pos) - Vector3.zero).normalized;
                float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, 0, z);

                Instantiate(bullet, pos, rot);

            }
        }

        void RoundTargetDelayShot(int count)
        {
            StartCoroutine(shotBullet());

            IEnumerator shotBullet()
            {
                isDone = false;
                List<Bullet> bullets = new List<Bullet>();

                float radius = 1f;
                for (int i = 0; i < 360; i += 360 / count)
                {
                    Vector3 pos = Vector3.zero + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);

                    Vector2 nor = ((curTarget.position - pos) - Vector3.zero).normalized;
                    float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
                    Quaternion rot = Quaternion.Euler(0, 0, z);

                    var temp = Instantiate(bullet, pos, rot);
                    temp.GetComponent<Bullet>().isRun = false;
                    bullets.Add(temp.GetComponent<Bullet>());
                }
                yield return new WaitForSeconds(1f);

                foreach (var item in bullets)
                {
                    item.isRun = true;
                    yield return new WaitForSeconds(0.1f);
                }

                isDone = true;
                yield break;
            }
        }

        void SectorFormShot(int count, float central) // 부채꼴
        {
            int i = count % 2 == 0 ? -count / 2 : -(int)(count / 2f - 0.5f);
            int max = i * -1;

            float amount = central / count;
            float z = i * amount;

            for (; i <= max; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, z);
                Instantiate(bullet, Vector3.zero, rot);
                z += amount;
            }
        }

        void SectorFormTargetShot(int count, float central) // 방향 바라보는 부채꼴 샷
        {
            int i = count % 2 == 0 ? -count / 2 : -(int)(count / 2f - 0.5f);
            int max = i * -1;

            Vector2 nor = (curTarget.position - Vector3.zero).normalized;
            float tarZ = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;

            float amount = central / count;
            float z = i * amount + (int)tarZ;

            for (; i <= max; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, z);
                Instantiate(bullet, Vector3.zero, rot);
                z += amount;
            }
        }

        void SectorFormDelayShot(int bulletCount, int central, int rotate, int rotateCount, int entireCount) // 부채꼴 왔다갔다 여러발
        {
            StartCoroutine(shotRoutine());

            IEnumerator shotRoutine()
            {
                isDone = false;

                float z = -rotate;
                float amount = (rotate * 2f) / rotateCount;

                for (int i = 0; i < entireCount; i++)
                {
                    Vector2 nor = (curTarget.position - Vector3.zero).normalized;
                    float tarZ = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;

                    z += amount;
                    if (z == rotate || z == -rotate)
                    {
                        amount *= -1;
                    }

                    shotBullet(central, z + tarZ);

                    yield return new WaitForSeconds(0.1f);
                }

                isDone = true;
                yield break;
            }

            void shotBullet(float central, float startRot)
            {
                int i = bulletCount % 2 == 0 ? -bulletCount / 2 : -(int)(bulletCount / 2f - 0.5f);
                int max = i * -1;

                float amount = central / bulletCount;
                float z = i * amount;

                for (; i <= max; i++)
                {
                    Quaternion rot = Quaternion.Euler(0, 0, z + startRot);
                    Instantiate(bullet, Vector3.zero, rot);
                    z += amount;
                }
            }
        }

        void MovingCircleShot(int count) // 원형이 움직이면서
        {
            float radius = 1f;
            for (int i = 0; i < 360; i += 360 / count)
            {
                Vector3 pos = Vector3.zero + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);

                Vector2 nor = (curTarget.position - Vector3.zero).normalized;
                float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, 0, z);

                Instantiate(bullet, pos, rot);
            }
        }
    }
}
