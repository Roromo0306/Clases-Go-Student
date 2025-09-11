using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculadorParticulas : MonoBehaviour
{
    [Header("Curva de emisión")]
    public AnimationCurve emission = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float minEmission = 0f;
    public float maxEmission = 5f;
    public float cicloEmission = 5f;

    [Header("Curva de tiempo de vida")]
    public AnimationCurve tiempoVida = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float minTiempoVida = 0f;
    public float maxTiempoVida = 5f;


    private float cronometro = 0f;
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        AumentarCronometro(); //Aumenta el tiempo del cronómetro

        CambioEmision(); //Controla la emisión de los rayos
        CambioTiempoVida(); //Controla el tiempo de vida de los rayos


    }

    void AumentarCronometro()
    {
        cronometro += Time.deltaTime;
        if(cronometro >= cicloEmission)
        {
            cronometro = 0;
        }
    }

    void CambioEmision()
    {
        float totalEmission = minEmission + emission.Evaluate(cronometro / cicloEmission) * (maxEmission - minEmission);
        var canalEmision = particles.emission;
        canalEmision.rateOverTime = new ParticleSystem.MinMaxCurve(totalEmission);
    }

    void CambioTiempoVida()
    {
        float totalVida = minTiempoVida + tiempoVida.Evaluate(cronometro / maxTiempoVida) * (maxTiempoVida - minTiempoVida);
        var canalMain = particles.main;
        canalMain.startLifetime = new ParticleSystem.MinMaxCurve(totalVida);
    }
}
