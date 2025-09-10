using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Atributos Player")]
    [Tooltip("Velocidad a la que se mueve el jugador")]
    public float velocidad = 5f;

    private float MoveX, MoveY;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveX = Input.GetAxis("Horizontal");
        MoveY = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(MoveX , MoveY, 0f).normalized;
        rb.velocity = movimiento * velocidad;
    }
}
