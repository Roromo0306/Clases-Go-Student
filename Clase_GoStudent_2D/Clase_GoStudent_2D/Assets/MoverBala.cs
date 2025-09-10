using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBala : MonoBehaviour
{
    public float velocidad = 5f;

    public float daño = 20;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up *  velocidad * Time.deltaTime, Space.Self);
       
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            player.Danio(daño);
            Debug.Log("Estoy colisionando con el player");
            Destroy(gameObject);
        }
    }

   
}
