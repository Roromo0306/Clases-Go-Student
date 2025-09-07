using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionJugador : MonoBehaviour
{
    public enum tipoDeteccion { Distancia, Raycast, DistanciaAngulo}
    public tipoDeteccion tipo = tipoDeteccion.Distancia;

    [Header("Detección por Distancia")]
    [Tooltip("Umbral de detección entre Jugador y Torreta")]
    public float distanciaDeteccion = 5f;

    [Header("Detección por Angulo")]
    [Tooltip("Umbral de detección entre Jugador y Torreta")]
    public float anguloDeteccion = 40f;


    [Header("Detección por Raycast")]
    public Transform puntoOrigen;
    public LayerMask mascaraRaycast;
    public float radioOlvidarJugador = 5f;

    private Vector3 puntoImpacto;
    private Vector3 puntoFinal;


    public bool detectado = false;

    private Transform jugador;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ModoDeteccion();
    }

    void ModoDeteccion()
    {
        switch (tipo)
        {
            case tipoDeteccion.Distancia:
                TipoDistancia();
                break;
            case tipoDeteccion.Raycast:
                TipoRaycast();
                break;

            case tipoDeteccion.DistanciaAngulo:
                TipoDistanciaAngulo();
                break;
        }
    }

    void TipoDistancia()
    {

        float distancia = Vector3.Distance(jugador.position, transform.position); //float distancia = (jugador.position - transform.position).magnitude;

        detectado = (distancia <= distanciaDeteccion);

        
    }
    void TipoDistanciaAngulo()
    {
        Vector3 direccion = (jugador.position - transform.position);
        float distancia = direccion.magnitude;

        float angulo = Vector3.Angle(transform.up, direccion);

        detectado = (distancia <= distanciaDeteccion) && (angulo <= anguloDeteccion);


    }

    void TipoRaycast()
    {
        bool golpeaJugador = false;

        RaycastHit2D hit = Physics2D.Raycast(puntoOrigen.position, puntoOrigen.up, distanciaDeteccion, mascaraRaycast); //CircleCast??
        puntoImpacto = hit.point;

        if (hit.collider != null)
        {
            golpeaJugador = (hit.transform == jugador); 

        }

        if (golpeaJugador)
        {
            detectado = true;
        }
        else if (detectado)
        {
            Vector3 direccion = (jugador.position - transform.position);
            float distancia = direccion.magnitude;

            if(distancia >= radioOlvidarJugador)
            {
                detectado = false;
            }
        }

        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radioOlvidarJugador);

        if(detectado == true)
        {
            if (tipo == tipoDeteccion.Raycast)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(puntoOrigen.position, puntoImpacto - puntoOrigen.position);
            }
        }
        else
        {
           
            puntoFinal = puntoOrigen.position + transform.up * distanciaDeteccion;
            Gizmos.color = Color.gray;
            Gizmos.DrawRay(puntoOrigen.position, puntoFinal - puntoOrigen.position );
            
            
        }
       
    }    
#endif
}
