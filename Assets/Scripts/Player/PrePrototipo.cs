using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Windows.Speech;

public class PrePrototipo : MonoBehaviour
{
    public bool moverAdelante, moverDerecha, moverIzquierda, moverAtras, quieto, PararCoche, Camara1_Act;
    public bool gameOver = false;
    public float speed;

    public GameObject GameOver, SalirMenu, MenuFinal;

    //micro
    public KeywordRecognizer m_Recognizer;
    Dictionary<string, Action> wtaction;

    public AudioSource sonidoCoche, palabraCorrecta;

    public GameObject Camara1, Camara2;
    public ParticleSystem particulasDerecha, particulasIzquierda;

    void Start()
    {
        PararCoche = false;

        //Listado de palabras
        wtaction = new Dictionary<string, Action>();

        //Arriba
        wtaction.Add("arriba", delante);
        wtaction.Add("delante", delante);
        wtaction.Add("norte", delante);

        //Derecha
        wtaction.Add("derecha", derecha);
        wtaction.Add("girar derecha", derecha);
        wtaction.Add("este", derecha);
       
        //Izquierda
        wtaction.Add("izquierda", izquierda);
        wtaction.Add("rotar izquierda", izquierda);
        wtaction.Add("girar izquierda", izquierda);
        wtaction.Add("doblar izquierda", izquierda);
        wtaction.Add("oeste", izquierda);
       
        //Debajo
        wtaction.Add("detras", detras);
        wtaction.Add("atras", detras);
        wtaction.Add("sur", detras);
        wtaction.Add("sud", detras);
        wtaction.Add("debajo", detras);
        wtaction.Add("abajo", detras);

        //Parar
        wtaction.Add("para", freno);
        wtaction.Add("stop", freno);

        //Cambio de camara
        wtaction.Add("camara", cambioCamara);
        wtaction.Add("cambio camara", cambioCamara);

        //Micro
        m_Recognizer = new KeywordRecognizer(wtaction.Keys.ToArray());
        m_Recognizer.OnPhraseRecognized += wordrecognizer;
        m_Recognizer.Start();

        //Movimiento
        moverAdelante = false;
        moverDerecha = false;
        moverIzquierda = false;
        moverAtras = false;
        quieto = false;
        Camara1_Act = true;
    }

    private void wordrecognizer(PhraseRecognizedEventArgs word)
    {
        Debug.Log(word.text);
        wtaction[word.text].Invoke();
    }

    //Movimiento
    public void delante()
    {
        moverAdelante = true;
        moverDerecha = false;
        moverIzquierda = false;
        moverAtras = false;
        quieto = false;
        palabraCorrecta.Play();
    }

    public void detras()
    {
        moverAdelante = false;
        moverDerecha = false;
        moverIzquierda = false;
        moverAtras = true;
        quieto = false;
        palabraCorrecta.Play();
    }

    public void izquierda()
    {
        moverAdelante = false;
        moverDerecha = false;
        moverIzquierda = true;
        moverAtras = false;
        quieto = false;
        palabraCorrecta.Play();
    }

    public void derecha()
    {
        moverAdelante = false;
        moverDerecha = true;
        moverIzquierda = false;
        moverAtras = false;
        quieto = false;
        palabraCorrecta.Play();
    }

    public void freno()
    {
        moverAdelante = false;
        moverDerecha = false;
        moverIzquierda = false;
        moverAtras = false;
        quieto = true;
        palabraCorrecta.Play();
    }

    public void cambioCamara()
    {
        if (Camara1_Act)
        {
            Camara1_Act = false;
            Debug.Log("DesactivarCamara1");
        }
        else if (!Camara1_Act)
        {
            Camara1_Act = true;
            Debug.Log("DesactivarCamara2");
        }
    }

    private void Update()
    {
        if (PararCoche == false)
        {
            sonidoCoche.Play();
            particulasDerecha.Play();
            particulasIzquierda.Play();

            if (moverAdelante == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
            }
         
            if (moverDerecha == true)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
            }
         
            if (moverIzquierda == true)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
            }
         
            if (moverAtras == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
            }
         
            if (quieto == true)
            {
                particulasDerecha.Stop();
                particulasIzquierda.Stop();

                if (transform.rotation.y == 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
         
                if (transform.rotation.y == 90)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
                }
         
                if (transform.rotation.y == -90)
                {
                    transform.eulerAngles = new Vector3(0, -90, 0);
                }
         
                if (transform.rotation.y == 180)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
         
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }

        if (Camara1_Act)
        {
            Camara1.SetActive(true);
            Camara2.SetActive(false);
        }
        else if (!Camara1_Act)
        {
            Camara1.SetActive(false);
            Camara2.SetActive(true);
        }
    }

   
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Pared")
        {
            PararCoche = true;
            GameOver.SetActive(true);
            SalirMenu.SetActive(false);
            moverAdelante = false;
            moverDerecha = false;
            moverIzquierda = false;
            moverAtras = false;
            quieto = true;
            gameOver = true;
            particulasDerecha.Stop();
            particulasIzquierda.Stop();
            sonidoCoche.Stop();
        }

        if (col.tag == "Final")
        {
            PararCoche = true;
            MenuFinal.SetActive(true);
            SalirMenu.SetActive(false);
            moverAdelante = false;
            moverDerecha = false;
            moverIzquierda = false;
            moverAtras = false;
            quieto = true;
            gameOver = true;
            particulasDerecha.Stop();
            particulasIzquierda.Stop();
            sonidoCoche.Stop();
        }
    }
}