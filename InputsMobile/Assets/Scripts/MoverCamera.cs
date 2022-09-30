using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform inimigo;

    private float cronometro = 0f;
    [SerializeField] private float tempoMudanca = 10.0f;
    [SerializeField] [Range(0.01f, 1)] private float velocidade = 1f;
    private bool tempoPlayer = true;
    private bool chegou = true;

    private float tempoInicialZoom;
    private float tempoFinalZoom;
    private float tempoZoom;
    private float tempoMAXZoom = 1;

    private bool zoom = false;
    private bool zoomAlt = false;

    [SerializeField] private CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tempoZoom >= tempoFinalZoom)
        {
            zoom = false;
            zoomAlt = false;
        }

        if (zoom)
        {
            // VVVV isso para deixar um zero, senao seria um virada mt brusca se fosse que nem o codigo de cima
            // no primeiro instante isso nao é zero tempoZoom / tempoFinalZoom

            cam.m_Lens.OrthographicSize = Mathf.Lerp(6, 4, (tempoZoom - tempoInicialZoom) / (tempoFinalZoom - tempoInicialZoom));
            tempoZoom += Time.deltaTime;
        }
        else if (zoomAlt)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(4, 6, (tempoZoom - tempoInicialZoom) / (tempoFinalZoom - tempoInicialZoom));
            tempoZoom += Time.deltaTime;
        }

        if (tempoPlayer && !chegou)
        {
            if (this.transform.position != player.position)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, velocidade);
            }
            else
            {
                chegou = true;
                DarZoom();
            }
        }
        else if (!tempoPlayer && !chegou)
        {
            if (this.transform.position != inimigo.position)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, inimigo.transform.position, velocidade);
            }
            else
            {
                chegou = true;
                DarZoom();
            }
        }

        if (cronometro >= tempoMudanca)
        {
            tempoPlayer = !tempoPlayer;
            cronometro = 0f;
            chegou = false;
            TirarZoom();
        }
        cronometro += Time.deltaTime;
    }

    private void DarZoom()
    {
        tempoInicialZoom = Time.time;
        tempoZoom = tempoInicialZoom;
        tempoFinalZoom = tempoMAXZoom + tempoZoom;
        //camera.m_Lens.OrthographicSize = 4f;

        zoom = true;
    }

    private void TirarZoom()
    {
        tempoInicialZoom = Time.time;
        tempoZoom = tempoInicialZoom;
        tempoFinalZoom = tempoMAXZoom + tempoZoom;
        //camera.m_Lens.OrthographicSize = 6f;

        zoomAlt = true;
    }
}
