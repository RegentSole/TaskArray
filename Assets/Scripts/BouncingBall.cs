using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float jumpForce = 20f; // Сила прыжка
    public float speedThreshold = 0.5f; // Порог скорости для смены цвета
    public Color normalColor = Color.white; // Нормальный цвет мяча
    public Color slowColor = Color.red; // Цвет при низкой скорости
    
    private Rigidbody rb;
    private Renderer rend;
    private float timeBelowThreshold = 0f; // Время, проведенное под порогом скорости
    private bool isSlow = false; // Флаг для отслеживания состояния скорости

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Начальное ускорение вверх
    }

    void Update()
    {
        CheckSpeed();
    }

    private void CheckSpeed()
    {
        float currentSpeed = rb.velocity.magnitude;

        if (currentSpeed < speedThreshold && !isSlow)
        {
            // Скорость упала ниже порога, меняем цвет и начинаем отсчет времени
            rend.material.color = slowColor;
            isSlow = true;
            timeBelowThreshold = Time.time;
        }
        else if (currentSpeed >= speedThreshold && isSlow)
        {
            // Скорость восстановилась, возвращаем нормальный цвет и выводим время в консоль
            rend.material.color = normalColor;
            Debug.Log("Время ниже порога: " + (Time.time - timeBelowThreshold));
            isSlow = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Приложение силы вверх при касании земли
        }
    }
}