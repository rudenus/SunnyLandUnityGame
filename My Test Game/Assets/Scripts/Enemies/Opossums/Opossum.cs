using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IEnemies;

public class Opossum : MonoBehaviour
{
    public class OpossumClass : IEnemie
    {
        private Rigidbody2D rb;
        private float speed;
        private bool faceRight = true;
        public OpossumClass(Rigidbody2D rb, float speed)
        {
            this.rb = rb;
            this.speed = speed;
        }
        public void PatruleRegion()
        {
            float cheeckRad = 0.3f;
            Bounds bound = rb.gameObject.GetComponent<BoxCollider2D>().bounds;
            string[] arrLayer = new string[2] { "Platform", "Earth" };//какие слои опоссум принимает за препятствие
            LayerMask lm = LayerMask.GetMask(arrLayer);
            Vector2 pointAfterOpos = new Vector2(bound.max.x + 0.5f, bound.min.y);
            Vector2 pointBeforeOpos = new Vector2(bound.min.x - 0.5f, bound.min.y);//точки для детекта земли

            if (!Physics2D.OverlapCircle(pointAfterOpos, cheeckRad, lm) //если перед опоссумом нет земли
                || Physics2D.OverlapCircle(new Vector2(bound.max.x, bound.center.y), cheeckRad, lm)//если опоссум видит препятствие справа
                || IsNeedToFlipPlayer())//если опоссум видит игрока
            {
                Flip();
            }

            if (!Physics2D.OverlapCircle(pointBeforeOpos, cheeckRad, lm)//если перед опоссумом нет земли
                || Physics2D.OverlapCircle(new Vector2(bound.min.x, bound.center.y), cheeckRad, lm)//если опоссум слева видит препятствие
                || IsNeedToFlipPlayer())//если опоссум видит игрока
            {
                Flip();
            }
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        private bool IsNeedToFlipPlayer()//для ии опоссума
        {
            string[] maskArray = new string[3] { "Player", "Earth", "Platform" };
            LayerMask lm = LayerMask.GetMask(maskArray);

            RaycastHit2D result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.right), 150f, lm);
            if (result.collider.tag == "Player")//опоссум кидает луч вперед, и если первый объект на пути игрок, разворачивается
                if (!faceRight)
                    return true;

            result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.left), 150f, lm);
            if (result.collider.tag == "Player")//аналогично только луч назад
                if (faceRight)
                    return true;
            return false;

        }
        private void Flip()//поворот опоссума по оси x
        {
            speed = -speed;
            faceRight = !faceRight;
            Vector3 scaler = rb.transform.localScale;
            scaler.x *= -1;
            rb.transform.localScale = scaler;
        }
        public void KillThis()//вызывается через специальный boxCollider триггер
        {
            Destroy(rb.gameObject);
            GameObject.Find("Enemies").GetComponent<ListEnemies>().enemies.Remove(this);//удаление из списка всех врагов
        }
    }
  
}
