using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 2.0f;

    private CameraFollow cameraFollow;
    private Rigidbody2D rig;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public bool isGrounded;
    public bool ac = false;
    private bool canStrafe = false;
    private int bestScore = 0;
    private int aScore = 0;

    public Sprite[] skins; // Массив спрайтов для скинов
    private SpriteRenderer spriteRenderer;
    private int currentSkinIndex = 0; // Индекс текущего скина
    private int groundSkinIndex = 0; // Индекс скина, когда на земле

    /*//
    public float maxApproachDistance = 1.0f; // Максимальное допустимое приближение
    //*/

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSkin(currentSkinIndex); // Установка начального скина
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true && isGrounded == true)
        {
            canStrafe = false;
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }
        if (Input.GetMouseButtonDown(0) == true && isGrounded == false && ac == false && canStrafe == false)
        {
            canStrafe = true;
            Vector2 newVelocity = rig.velocity;
            newVelocity.y = 0;
            newVelocity.x = speed;  // Установка горизонтальной скорости
            rig.velocity = newVelocity;
            newVelocity.y = 0;

            ac = true;
        }
        CheckKeyPress();
        CheckGrounded();
    }

    private void ChangeSkin(int skinIndex)
    {
        spriteRenderer.sprite = skins[skinIndex]; // Установка нового спрайта
    }

    void CheckKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.D) && !isGrounded)
        {
            Vector2 newVelocity = rig.velocity;
            newVelocity.y = 0;
            newVelocity.x = speed;  // Установка горизонтальной скорости
            newVelocity.y = 0;
            rig.velocity = newVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("WOW");
            Vector2 newVelocity = rig.velocity;
            newVelocity.y = 0;
            newVelocity.x = 0;

            rig.velocity = newVelocity;
            //transform.position = collision.gameObject.transform.position;
            
            transform.position = Vector3.Lerp(transform.position, collision.gameObject.transform.position, cameraFollow.smoothSpeed);

            /*//

            Transform target = collision.gameObject.transform;

            // Вычисляем направление к целевому объекту
            Vector3 directionToTarget = target.position - transform.position;

            // Вычисляем расстояние до целевого объекта
            float distanceToTarget = directionToTarget.magnitude;

            // Если расстояние меньше максимального приближения, прекращаем движение
            if (distanceToTarget <= maxApproachDistance)
            {
                return;
            }

            // Вычисляем новую позицию объекта с учетом скорости движения
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, 10f * Time.deltaTime);

            // Перемещаем объект к новой позиции
            transform.position = newPosition;

            //*/

            cameraFollow.variable += 1;
            aScore = cameraFollow.variable;
            PlayerPrefs.SetInt("AScore", aScore);

            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }

        if (collision.gameObject.CompareTag("LooseLine"))
        {
            SaveScore();
            if (cameraFollow.variable > bestScore)
            {
                PlayerPrefs.SetInt("AScore", 0);
                bestScore = cameraFollow.variable;
                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save();
            }
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {   
    currentSkinIndex = (currentSkinIndex + 1) % skins.Length; // Циклическая смена скинов
    ChangeSkin(currentSkinIndex);
        }*/
        if (other.gameObject.CompareTag("JumpSkin"))
        {
            currentSkinIndex = (3) % skins.Length; // Циклическая смена скинов
            ChangeSkin(currentSkinIndex);
        }
        if (other.gameObject.CompareTag("FallSkin"))
        {
            currentSkinIndex = (1) % skins.Length; // Циклическая смена скинов
            ChangeSkin(currentSkinIndex);
        }
        if (other.gameObject.CompareTag("FlySkin"))
        {
            currentSkinIndex = (2) % skins.Length; // Циклическая смена скинов
            ChangeSkin(currentSkinIndex);
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", cameraFollow.variable);
    }

    void CheckGrounded()
    {
        ac = false;
        // Проверяем, есть ли объекты с тегом "Floor" под игроком
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        isGrounded = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Floor"))
            {
                isGrounded = true;

                currentSkinIndex = groundSkinIndex % skins.Length; // Циклическая смена скинов
                ChangeSkin(currentSkinIndex);

                break;
            }
        }
    }
}
