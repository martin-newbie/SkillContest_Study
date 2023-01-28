using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShootingRange
{
    public class ShootingRange : MonoBehaviour
    {
        public GameObject bullet;
        public GameObject rotateBullet;

        [Header("Objects")]
        public Transform startPosTR;
        public Transform[] targets;

        Vector3 startPos => startPosTR.position;

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

            for (int i = 0; i < 12; i++)
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
                    TargetingSingleShot();
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
                case 9:
                    MovingRotatingCircleShot(10, 4f, 100f);
                    break;
                case 10:
                    GatheringCircleShot(10);
                    break;
                case 11:
                    LinearTargetDelayShot(10);
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

        void TargetingSingleShot()
        {
            float z = Mathf.Atan2(curTarget.position.y, curTarget.position.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, z);
            Instantiate(bullet, new Vector3(0, 0, 0), rot);
        }

        void RoundTargetShot(int count)
        { // startPos는 시작지점으로 치환
            float radius = 1f;
            for (int i = 0; i < 360; i += 360 / count)
            {
                Vector3 pos = startPos + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);

                Vector2 nor = ((curTarget.position - pos) - startPos).normalized;
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
                    Vector3 pos = startPos + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);

                    Vector2 nor = ((curTarget.position - pos) - startPos).normalized;
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
                Instantiate(bullet, startPos, rot);
                z += amount;
            }
        }

        void SectorFormTargetShot(int count, float central) // 방향 바라보는 부채꼴 샷
        {
            int i = count % 2 == 0 ? -count / 2 : -(int)(count / 2f - 0.5f);
            int max = i * -1;

            Vector2 nor = (curTarget.position - startPos).normalized;
            float tarZ = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;

            float amount = central / count;
            float z = i * amount + (int)tarZ;

            for (; i <= max; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, z);
                Instantiate(bullet, startPos, rot);
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
                    Vector2 nor = (curTarget.position - startPos).normalized;
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
                    Instantiate(bullet, startPos, rot);
                    z += amount;
                }
            }
        }

        void MovingCircleShot(int count) // 원형이 움직이면서
        {
            float radius = 1f;
            for (int i = 0; i < 360; i += 360 / count)
            {
                Vector3 pos = startPos + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);

                Vector2 nor = (curTarget.position - startPos).normalized;
                float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, 0, z);

                Instantiate(bullet, pos, rot);
            }
        }

        void MovingRotatingCircleShot(int count, float moveSpd, float rotateSpd)
        {
            float radius = 1f;
            for (int i = 0; i < 360; i += 360 / count)
            {
                Vector3 pos = startPos + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius);
                Vector2 nor = (curTarget.position - startPos).normalized;

                GameObject temp = Instantiate(rotateBullet, pos, Quaternion.identity);
                temp.GetComponent<RotateBullet>().InitBullet(startPos, nor, moveSpd, rotateSpd, i, radius);
            }
        }

        void GatheringCircleShot(int count)
        {
            StartCoroutine(shotBullet());

            IEnumerator shotBullet()
            {
                isDone = false;
                float radius = 2f;
                List<Bullet> bullets = new List<Bullet>();
                for (int i = 0; i < 360; i += 360 / count)
                {
                    Vector3 pos = new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius);
                    pos += curTarget.position;

                    Vector2 nor = (curTarget.position - pos).normalized;
                    float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
                    Quaternion rot = Quaternion.Euler(0, 0, z);

                    var temp = Instantiate(bullet, pos, rot);
                    var bt = temp.GetComponent<Bullet>();

                    bt.isRun = false;
                    bullets.Add(bt);

                    yield return new WaitForSeconds(1f / count);
                }

                yield return new WaitForSeconds(1f);

                foreach (var item in bullets)
                {
                    item.isRun = true;
                }

                isDone = true;
                yield break;
            }
        }

        void LinearTargetDelayShot(int count)
        {
            StartCoroutine(shotBullet());

            IEnumerator shotBullet()
            {
                isDone = false;

                List<Bullet> bullets = new List<Bullet>();

                for (int i = 0; i < count; i++)
                {
                    Vector3 pos = startPos + new Vector3(-count / 2 + i, 0);

                    var temp = Instantiate(bullet, pos, Quaternion.identity).GetComponent<Bullet>();
                    temp.isRun = false;
                    bullets.Add(temp);
                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.5f);

                foreach (var item in bullets)
                {
                    var dir = (curTarget.position - item.transform.position);
                    float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion rot = Quaternion.Euler(0, 0, z);
                    
                    item.transform.rotation = rot;
                    item.isRun = true;

                    yield return new WaitForSeconds(0.1f);
                }

                isDone = true;
                yield break;
            }
        }
    }
}
