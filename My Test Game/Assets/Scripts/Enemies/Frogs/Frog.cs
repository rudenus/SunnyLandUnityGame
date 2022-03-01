using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IEnemies;

public class Frog : MonoBehaviour
{
    public class FrogClass : IEnemie
    {
        private Rigidbody2D rb;
        private float jumpForce;
        private float speed;
        private Animator animator;
        private Transform feetPos;
        private bool isGrounded = true;
        private bool faceRight = true;
        public FrogClass(Rigidbody2D rb,Animator animator,float jumpForce, float speed)
        {
            this.animator = animator;
            this.jumpForce = jumpForce;
            this.rb = rb;
            this.speed = speed;
            feetPos = GameObject.Find("FeetPos").transform;
        }
        public void KillThis()
        {
            Destroy(rb.gameObject);
            GameObject.Find("Enemies").GetComponent<ListEnemies>().enemies.Remove(this);//удаление из массива ListEnemie
        }

        public void PatruleRegion()
        {
            
            if (!isGrounded)
            {
                return;
            }
            string[] arrLayer = new string[2] { "Platform", "Earth" };//какие слои л€гушка принимает за преп€тствие
            LayerMask lm = LayerMask.GetMask(arrLayer);
            Vector2 pointForDetect = new Vector2(feetPos.position.x, feetPos.position.y+0.5f);
            RaycastHit2D raycast = Physics2D.Raycast(pointForDetect, Vector2.right, Mathf.Abs(speed), lm);
            if (faceRight && raycast &&(raycast.collider.tag == "Earth" || raycast.collider.tag == "Platform")//если л€гушка видит преп€тствие справа
                || IsNeedToFlipPlayer())//если л€гушка видит игрока
            {
                Flip();
            }
            raycast = Physics2D.Raycast(pointForDetect, Vector2.left, Mathf.Abs(speed), lm);
            if (!faceRight && raycast && (raycast.collider.tag == "Earth" || raycast.collider.tag == "Platform")//если л€гушка слева видит преп€тствие
                || IsNeedToFlipPlayer())//если л€гушка видит игрока
            {
                Flip();
            }
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(speed, jumpForce);
            isGrounded = false;
            

        }
        private bool IsNeedToFlipPlayer()//дл€ ии л€гушки
        {
            string[] maskArray = new string[3] { "Player", "Earth", "Platform" };
            LayerMask lm = LayerMask.GetMask(maskArray);
            RaycastHit2D result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.right), 150f, lm);
            if (result.collider.tag == "Player")//л€гушка кидает луч вперед, и если первый объект на пути игрок, разворачиваетс€
                if (!faceRight)
                    return true;
            result = Physics2D.Raycast(rb.transform.position, rb.transform.TransformDirection(Vector3.left), 150f, lm);
            if (result.collider.tag == "Player")//аналогично только луч назад
                if (faceRight)
                    return true;
            return false;

        }

        private void Flip()//поворот л€гушки по оси x
        {
            speed = -speed;
            faceRight = !faceRight;
            Vector3 scaler = rb.transform.localScale;
            scaler.x *= -1;
            rb.transform.localScale = scaler;
        }
        public void SetIsGrounded()//вызываетс€ в спеиально триггере
        {
            isGrounded = true;
        }
    }
}
