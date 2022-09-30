using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seta: MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    private bool naArea = false;
    private bool destruido = false;

    private string tagDestruir = "";

    private Text textoAcerto;

    private void Start()
    {
        textoAcerto = GameObject.FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -speed * Time.deltaTime * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("barreira"))
        {
            naArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!destruido)
        {
            naArea = false;
        }
        Destruir();
    }

    public void Destruir()
    {
        if (tagDestruir == "" || !this.gameObject.CompareTag(tagDestruir))
        {
            textoAcerto.text = "ERROU";
        }
        else
        {
            if (naArea)
            {
                textoAcerto.text = "ACERTOU";
            }
            else
            {
                textoAcerto.text = "ERROU";
            }
        }

        destruido = true;
        Destroy(gameObject);
    }

    public void SetTagDestruir(string destruir)
    {
        tagDestruir = destruir;
    }
}
