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
            string[] arrLayer = new string[2] { "Platform", "Earth" };//����� ���� ������� ��������� �� �����������
            LayerMask lm = LayerMask.GetMask(arrLayer);
            Vector2 pointAfterOpos = new Vector2(bound.max.x + 0.5f, bound.min.y);
            Vector2 pointBeforeOpos = new Vector2(bound.min.x - 0.5f, bound.min.y);//����� ��� ������� �����

            if (!Physics2D.OverlapCircle(pointAfterOpos, cheeckRad, lm) //���� ����� ��������� ��� �����
                || Physics2D.OverlapCircle(new Vector2(bound.max.x, bound.center.y), cheeckRad, lm)//���� ������� ����� ����������� ������
                || IsNeedToFlipPlayer())//���� ������� ����� ������
            {
                Flip();
            }

            if (!Physics2D.OverlapCircle(pointBeforeOpos, cheeckRad, lm)//���� ����� ��������� ��� �����
                || Physics2D.OverlapCircle(new Vector2(bound.min.x, bound.center.y), cheeckRad, lm)//���� ������� ����� ����� �����������
                || IsNeedToFlipPlayer())//���� ������� ����� ������
            {
                Flip();
            }
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        private bool IsNeedToFlipPlayer()//��� �� ��������
        {
            string[] maskArray = new string[3] { "Player", "Earth", "Platform" };
            LayerMask lm = LayerMask.GetMask(maskArray);

            RaycastHit2D result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.right), 150f, lm);
            if (result.collider.tag == "Player")//������� ������ ��� ������, � ���� ������ ������ �� ���� �����, ���������������
                if (!faceRight)
                    return true;

            result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.left), 150f, lm);
            if (result.collider.tag == "Player")//���������� ������ ��� �����
                if (faceRight)
                    return true;
            return false;

        }
        private void Flip()//������� �������� �� ��� x
        {
            speed = -speed;
            faceRight = !faceRight;
            Vector3 scaler = rb.transform.localScale;
            scaler.x *= -1;
            rb.transform.localScale = scaler;
        }
        public void KillThis()//���������� ����� ����������� boxCollider �������
        {
            Destroy(rb.gameObject);
            GameObject.Find("Enemies").GetComponent<ListEnemies>().enemies.Remove(this);//�������� �� ������ ���� ������
        }
    }
  
}
