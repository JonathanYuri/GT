using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diretor : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 direction;
    
    [SerializeField]
    private Text m_Text;

    private GeradorDeSetas gerador;

    // olhar se o began position Y é menor do que o ended position Y, significa que foi pra cima
    // ver se o x difere em um intervalo?

    private void Start()
    {
        this.gerador = this.GetComponent<GeradorDeSetas>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update the Text on the screen depending on current TouchPhase, and the current direction vector

        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                this.startPos = Camera.main.ScreenToWorldPoint(touch.position);
            }
            /*
            else if (touch.phase == TouchPhase.Moved)
            {
                this.direction = touch.position - this.startPos;
            }*/
            else if (touch.phase == TouchPhase.Ended)
            {
                this.direction = Camera.main.ScreenToWorldPoint(touch.position) - this.startPos;
                this.PegarDirecao();
            }
        }

        Debug.DrawLine(Vector3.zero, this.direction, Color.red);
    }

    private void PegarDirecao()
    {
        if (this.direction.x < this.startPos.x) // 2 ou 3 quadrante
        {
            // o Valor absoluto pois posso ter indo pra baixo, ou indo pra esquerda e talz
            if (Mathf.Abs(this.direction.y) < Mathf.Abs(this.direction.x)) // o y nao cresceu tanto quanto o x, ele tentou ir pro lado apenas
            {
                Debug.Log("left" + this.direction + " start : " + this.startPos);
                this.m_Text.text = "left";
                // left

                this.DestruirSeta("SetaEsquerda");
            }
            else
            {
                if (this.direction.y > 0)
                {
                    Debug.Log("up" + this.direction + " start : " + this.startPos);
                    this.m_Text.text = "up";
                    // up

                    this.DestruirSeta("SetaCima");
                }
                else
                {
                    Debug.Log("down" + this.direction + " start : " + this.startPos);
                    this.m_Text.text = "down";
                    // down

                    this.DestruirSeta("SetaBaixo");
                }
            }
        }
        else // 1 ou 4 quadrante
        {
            // this.direction.x >= this.startPos.x

            if (Mathf.Abs(this.direction.y) < Mathf.Abs(this.direction.x))
            {
                Debug.Log("right" + this.direction + " start : " + this.startPos);
                this.m_Text.text = "right";
                // right

                this.DestruirSeta("SetaDireita");
            }
            else
            {
                if (this.direction.y > 0)
                {
                    Debug.Log("up" + this.direction + " start : " + this.startPos);
                    this.m_Text.text = "up";
                    // up

                    this.DestruirSeta("SetaCima");
                }
                else
                {
                    Debug.Log("down" + this.direction + " start : " + this.startPos);
                    this.m_Text.text = "down";
                    // down

                    this.DestruirSeta("SetaBaixo");
                }
            }
        }
    }

    private void DestruirSeta(string destruir)
    {
        Seta[] setas = GameObject.FindObjectsOfType<Seta>();

        int posicaoMenorYSeta = -1;
        Seta setaMenorY = null;

        for (int i = 0; i < setas.Length; i++)
        {
            if (setaMenorY == null)
            {
                posicaoMenorYSeta = i;
                setaMenorY = setas[i];
            }
            else
            {
                if (setas[i].transform.position.y < setaMenorY.transform.position.y)
                {
                    posicaoMenorYSeta = i;
                    setaMenorY = setas[i];
                }
            }
        }

        if (posicaoMenorYSeta != -1)
        {
            setas[posicaoMenorYSeta].SetTagDestruir(destruir);
            setas[posicaoMenorYSeta].Destruir();
        }
    }
}
