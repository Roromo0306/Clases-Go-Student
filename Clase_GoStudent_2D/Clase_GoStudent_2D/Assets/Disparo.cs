using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
    public float cooldown = 1f;
    public Animator anim;

    private DeteccionJugador deteccionjugador;
    private Coroutine coroutine;
    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        deteccionjugador = GetComponent<DeteccionJugador>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Invoke("Funcion", cooldown); //Forma de llamar a una función cada X tiempo

        if (deteccionjugador.detectado && coroutine == null)
        {
            coroutine = StartCoroutine(CorrutinaDisparo());
            
        }
    }

    private IEnumerator CorrutinaDisparo()
    {
        while (deteccionjugador.detectado)
        {
            Instantiate(prefabProyectil,puntoDisparo.position, transform.rotation);
            anim.SetTrigger("Disparo");
            if(particles != null)
            {
                particles.Play();
            }
            
            yield return new WaitForSeconds(cooldown);
        }

    }
}
