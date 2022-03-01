using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public class PlayerClass
    {
        private Rigidbody2D rb;
        private float speed;
        private float jumpForce;
        private static PlayerClass player;
        private int health = 3;
        private Animator animator;
        private bool faceRight = true;
        private GameObject bullet;
        private bool InDoobleJump = false;
        private int scores = 0;
        private long currentTime;
        private PlayerClass(Rigidbody2D rb, Animator animator, float speed, float jumpForce, GameObject bullet)
        {
            this.animator = animator;
            this.rb = rb;
            this.speed = speed;
            this.jumpForce = jumpForce;
            this.bullet = bullet;
            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
        public static PlayerClass GetPlayer(Rigidbody2D rb, Animator animator, float speed, float jumpForce, GameObject bullet)
        {
            if (player == null || player.Equals(null) || player.rb==null)
            {
                player = new PlayerClass(rb, animator, speed, jumpForce, bullet);
            }
            return player;
        }
        public static PlayerClass GetPlayer()
        {
            return player;
        }
        public void Move(float inputMove)
        {
            rb.velocity = new Vector2(speed * inputMove, rb.velocity.y);
            if (animator)
            {
                animator.SetBool("Run", Mathf.Abs(inputMove) > 0.001f);
            }
            InversFace(inputMove);
        }
        public void Jump()
        {
            string[] arrLayer = new string[3] { "Platform", "Enemies", "Earth" };//enemie для того чтобы при убийстве врага прыжком персонаж подпрыгивал

            LayerMask lm = LayerMask.GetMask(arrLayer);
            Bounds bound = rb.GetComponent<BoxCollider2D>().bounds;
            if (Physics2D.OverlapBox(new Vector2(bound.center.x,bound.min.y), new Vector2(bound.size.x/2,0.1f),0f, lm))//Если мы на платформе
            {
                InDoobleJump = false;
                rb.velocity = Vector2.up * jumpForce;
                animator.SetTrigger("Jump");
            }
            else
            {
                DoobleJump();
            }
        }
        private void DoobleJump()
        {
            if (InDoobleJump)
            {
                return;
            }
            rb.velocity = Vector2.up * jumpForce;
            animator.SetTrigger("Jump");
            InDoobleJump = true;
        }
        public void Shoot()
        {
            float x, y;
            if (faceRight)
                x = rb.transform.GetComponent<BoxCollider2D>().bounds.max.x;
            else
            {
                x = rb.transform.GetComponent<BoxCollider2D>().bounds.min.x + 0.1f;
            }
            y = rb.transform.GetComponent<BoxCollider2D>().bounds.center.y - 0.4f;
            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(bullet, spawnPos, Quaternion.identity);
        }
        public void BonusRegister()
        {
            scores += 1000;
        }
        public void HealthRemove()
        {
            health--;
            scores -= 500;
            if (health <= 0)
            {
                Destroy(GameObject.Find("Player"));
                player = null;
                SceneManager.LoadScene(2);
            }
            GameObject.Find("Healths").GetComponent<Healths>().ImageChange();
        }
        private void InversFace(float inputMove)
        {
            if (!faceRight && inputMove > 0)
            {
                Flip();
            }
            if (faceRight && inputMove < 0)
            {
                Flip();
            }
        }
        private void Flip()
        {
            faceRight = !faceRight;
            Vector3 scaler = rb.transform.localScale;
            scaler.x *= -1;
            rb.transform.localScale = scaler;
        }
        public Transform GetTransform()
        {
            return rb.transform;
        }
        public void SaveScore()
        {
            long deltaTime = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - currentTime)/1000;
            scores -= (int)deltaTime * 10;//каждая секунда : -10 очков
            if (scores < 0) scores = 0;
            PlayerPrefs.SetInt("scores", scores);
        }
        public void KillEnemie()
        {
            scores += 1000;
        }
        public bool isFaceRight()
        {
            return faceRight;
        }
    }
}
