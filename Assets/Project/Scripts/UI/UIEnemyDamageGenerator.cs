using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class UIEnemyDamageGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject damagePrefab;
        public void ShowDamage(Vector3 enemyPos, int damage)
        {
            var damageUI = Instantiate(damagePrefab, transform);
            var rect = new Rect(0, 0, 1920, 1080);
            var rectTransform = damageUI.GetComponent<RectTransform>();
            var scrrenPos = Camera.main.WorldToScreenPoint(enemyPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                scrrenPos,
                null, // オーバーレイモードの場合はnull
                out var uiLocalPos);
            uiLocalPos = new Vector2(
                uiLocalPos.x > 900 ? Random.Range(700, 900)
                    : uiLocalPos.x < -900 ? -Random.Range(700, 900)
                    : uiLocalPos.x,
                uiLocalPos.y > 500 ? Random.Range(300, 500)
                    : uiLocalPos.y < -500 ? -Random.Range(300, 500)
                    : uiLocalPos.y
            );
            rectTransform.localPosition = uiLocalPos;
            /*if (!rect.Contains(rectTransform.position))
                rectTransform.position = Vector2.zero;*/
            damageUI.GetComponent<UIEnemyDamage>().SetDamage(damage);
        }
    }
}