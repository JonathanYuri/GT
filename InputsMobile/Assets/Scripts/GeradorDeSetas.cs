using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeSetas : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [Header("Prefabs")]

    [SerializeField]
    private GameObject prefabSetaCima;
    [SerializeField]
    private GameObject prefabSetaBaixo;
    [SerializeField]
    private GameObject prefabSetaEsq;
    [SerializeField]
    private GameObject prefabSetaDir;

    [Header("Geradores")]

    [SerializeField]
    private GameObject geradorCima;
    [SerializeField]
    private GameObject geradorBaixo;
    [SerializeField]
    private GameObject geradorEsq;
    [SerializeField]
    private GameObject geradorDir;

    [Header("Roteiro de Teclas")]

    [SerializeField] private List<GameObject> teclas;
    [SerializeField] private List<float> tempoTeclas;

    private float cronometro;
    [SerializeField]
    private float tempoParaGerar = 4f;

    private void Start()
    {
        this.cronometro = this.tempoParaGerar;

        this.teclas = new List<GameObject>();
        this.tempoTeclas = new List<float>();
        
        // sequencia
        this.teclas.Add(this.prefabSetaCima);
        this.tempoTeclas.Add(0.4f);

        this.teclas.Add(this.prefabSetaCima);
        this.tempoTeclas.Add(0.9f);

        this.teclas.Add(this.prefabSetaBaixo);
        this.tempoTeclas.Add(0.9f);

        this.teclas.Add(this.prefabSetaBaixo);
        this.tempoTeclas.Add(0.8f);

        this.teclas.Add(this.prefabSetaEsq);
        this.tempoTeclas.Add(0.7f);

        this.teclas.Add(this.prefabSetaDir);
        this.tempoTeclas.Add(0.9f);

        this.teclas.Add(this.prefabSetaEsq);
        this.tempoTeclas.Add(0.8f);

        this.teclas.Add(this.prefabSetaDir);
        this.tempoTeclas.Add(1.2f);

        StartCoroutine(this.GerarSetas());
    }

    private void Update()
    {
        
    }

    public IEnumerator GerarSetas()
    {
        for (int i = 0; i < this.teclas.Count; i++)
        {
            yield return new WaitForSeconds(tempoTeclas[i]);
            GameObject gerador = geradorCima;
            if (teclas[i] == prefabSetaBaixo)
            {
                gerador = geradorBaixo;
            }
            else if (teclas[i] == prefabSetaDir)
            {
                gerador = geradorDir;
            }
            else if (teclas[i] == prefabSetaEsq)
            {
                gerador = geradorEsq;
            }
            GameObject tecla = Instantiate(this.teclas[i], gerador.transform.position, Quaternion.identity);
            tecla.transform.SetParent(cam);
        }
    }
}
