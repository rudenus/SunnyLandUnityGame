using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IEnemies;

public class Eagle : MonoBehaviour
{
    public class EagleClass : IEnemie
    {
        private Rigidbody2D rb;
        private float speed;
        private Bounds region;
        private bool faceRight = true;
        public EagleClass(Rigidbody2D rb, float speed, float arealDiameter)
        {
            this.rb = rb;
            this.speed = speed;
            region = GetRegion(arealDiameter);
        }
        private Bounds GetRegion(float arealDiameter)//���������� ���� ������ ��� ��������������
        {
            Vector2 pos = rb.GetComponent<BoxCollider2D>().bounds.center;
            return new Bounds(pos, new Vector2(arealDiameter, 0.1f));
        }
        public void KillThis()
        {
            Destroy(rb.gameObject);
            GameObject.Find("Enemies").GetComponent<ListEnemies>().enemies.Remove(this);//�������� �� ListEnemie
        }
        
        public void PatruleRegion()
        {
            float cheeckRad = 0.3f;
            Bounds bound = rb.gameObject.GetComponent<BoxCollider2D>().bounds;
            string[] arrLayer = new string[2] { "Platform", "Earth" };//����� ���� ������� ��������� �� �����������
            LayerMask lm = LayerMask.GetMask(arrLayer);

            if(Physics2D.OverlapCircle(new Vector2(bound.max.x, bound.center.y), cheeckRad, lm))//�������������� ������ ���� ���� ������ �����������
            {
                region.max = new Vector2(bound.max.x, region.max.y);
            }

            if(Physics2D.OverlapCircle(new Vector2(bound.min.x, bound.center.y), cheeckRad, lm))//����������, �� ����������� �����
            {
                region.min = new Vector2(bound.min.x, region.min.y);
            }

            if ((bound.max.x+Mathf.Abs(speed/2)>region.max.x && faceRight)//���� ���� � ������ ������� �������
                || IsNeedToFlipPlayer())//���� ���� ����� ������
            {
                Flip();
            }
            if ((bound.min.x - Mathf.Abs(speed/2)<region.min.x && !faceRight)//���� ���� � ����� ������� �������
                || IsNeedToFlipPlayer())//���� ���� ����� ������
            {
                Flip();
            }
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        private bool IsNeedToFlipPlayer()//��� �� ����
        {
            string[] maskArray = new string[3] { "Player", "Earth", "Platform" };
            LayerMask lm = LayerMask.GetMask(maskArray);
            RaycastHit2D result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.right), 150f, lm);
            if (result.collider.tag == "Player")//���� ������ ��� ������, � ���� ������ ������ �� ���� �����, ���������������
                if (!faceRight)
                    return true;
            result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.left), 150f, lm);
            if (result.collider.tag == "Player")//���������� ������ ��� �����
                if (faceRight)
                    return true;
            return false;

        }
        private void Flip()//������� ���� �� ��� x
        {
            speed = -speed;
            faceRight = !faceRight;
            Vector3 scaler = rb.transform.localScale;
            scaler.x *= -1;
            rb.transform.localScale = scaler;
        }
    }
}
