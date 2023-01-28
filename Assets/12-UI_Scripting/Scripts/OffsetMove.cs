using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIMove
{
    public class OffsetMove : MonoBehaviour
    {
        public RectTransform window;

        int state = 0;
        bool isMove = true;

        public void MoveWindow()
        {
            if (!isMove) return;
            StartCoroutine(MoveCoroutine(0.5f));
        }

        IEnumerator MoveCoroutine(float duration)
        {
            isMove = false;

            float targetY = state == 0 ? -100 : -1420;
            state = state == 0 ? 1 : 0;
            
            Vector2 originPos = window.offsetMax;
            Vector2 targetPos = new Vector2(window.offsetMax.x, targetY);

            float timer = 0f;
            while (timer <= duration)
            {
                window.offsetMax = Vector2.Lerp(originPos, targetPos, ease(timer / duration));

                timer += Time.deltaTime;
                yield return null;
            }

            isMove = true;
            yield break;

            float ease(float x)
            {
                return 1f - Mathf.Pow(1f - x, 5);
            }
        }
    }
}

