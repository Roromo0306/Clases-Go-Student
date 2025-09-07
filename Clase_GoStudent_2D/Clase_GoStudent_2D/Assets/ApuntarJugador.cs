using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarJugador : MonoBehaviour
{
    [Tooltip("Arrastra aquí el objeto que tenga el script de DeteccionJugador")]
    public DeteccionJugador deteccion;

    public enum modoRotacion{Inmediato,Gradual, Curva}
    
    [Tooltip("Selección entre los modos de giro")]
    public modoRotacion modo = modoRotacion.Inmediato;

    [Header("Giro Gradual")]
    [Tooltip("Velocidad con la gira la torre en el modo de giro gradual")]
    public float velocidadGiro = 1;

    [Header("Giro Curva")]
    public AnimationCurve curvaVelocidad = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);


    private Transform posicionJugador;

    // Start is called before the first frame update
    void Start()
    {
        
        BuscarComponentes();
    }

    // Update is called once per frame
    void Update()
    {
        if(deteccion.detectado == true)
        {
            CambiarModoGiro();
        }
       
        
    }

    void CambiarModoGiro()
    {
       if(posicionJugador == null)
        {
            return;
        }

        switch (modo)
        {
            case modoRotacion.Inmediato:
                GiroInmediato();
                break;
            case modoRotacion.Gradual:
                GiroGradual();
                break;
            case modoRotacion.Curva:
               RotacionConCurva();
                break;
        }
    }

    void BuscarComponentes()
    {
       GameObject jugador = GameObject.Find("Player");

        if(jugador != null)
        {
            posicionJugador = jugador.transform;
        }
        else
        {
            Debug.LogError("Jugador no encontrado, hay que llamarlo Player");
        }
    }

    void GiroInmediato()
    {
        transform.up = posicionJugador.position - transform.position;
    }

    void GiroGradual()
    {
        Vector3 direccionObjetivo = (posicionJugador.position - transform.position).normalized;
        Vector3 nuevaDireccion = Vector3.RotateTowards(transform.up, direccionObjetivo, velocidadGiro* Time.deltaTime, 0); //RotateTowards gira un vector a otro al ritmo que le pidamos
        transform.up = nuevaDireccion;
    }

    void RotacionConCurva()
    {
        Vector3 direccionObjetivo = (posicionJugador.position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.up, direccionObjetivo); //La funcion de Vector3.Angle devuelve el ángulo entre vectores siempre entre 0 y 180 grados
        float p = Mathf.InverseLerp(0,180,angulo);
        Vector3 nuevaDireccion = Vector3.RotateTowards(transform.up, direccionObjetivo, velocidadGiro * curvaVelocidad.Evaluate(p) * Time.deltaTime, 0);
        transform.up = nuevaDireccion;
    }
}
