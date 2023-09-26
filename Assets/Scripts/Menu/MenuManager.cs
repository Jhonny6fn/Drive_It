using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Windows.Speech;

public class MenuManager : MonoBehaviour
{
    public KeywordRecognizer m_Recognizer;
    Dictionary<string, Action> wtaction;

    public GameObject MenuInicial, MenuAyuda;

    public static int escena;

    void Start()
    {
        wtaction = new Dictionary<string, Action>();

        //Nivel 1
        wtaction.Add("uno", uno);
        wtaction.Add("nivel uno", uno);

        //Nivel 2
        wtaction.Add("dos", dos);
        wtaction.Add("nivel dos", dos);

        //Nivel 3
        wtaction.Add("tres", tres);
        wtaction.Add("nivel tres", tres);

        //Ayuda
        wtaction.Add("ayuda", ayuda);
        wtaction.Add("help", ayuda);
        wtaction.Add("gelp", ayuda);

        //Atras
        wtaction.Add("atras", atras);
        wtaction.Add("return", atras);

        //Salir del juego
        wtaction.Add("salir", SalirDelJuego);
        wtaction.Add("quit", SalirDelJuego);
        wtaction.Add("exit", SalirDelJuego);

        m_Recognizer = new KeywordRecognizer(wtaction.Keys.ToArray());
        m_Recognizer.OnPhraseRecognized += wordrecognizer;
        m_Recognizer.Start();

        escena = 0;
    }

    private void wordrecognizer(PhraseRecognizedEventArgs word)
    {
        Debug.Log(word.text);
        wtaction[word.text].Invoke();
    }

    public void uno()
    {
        escena = 1;
        SceneManager.LoadScene("Laberinto");
    }

    public void dos()
    {
        escena = 2;
        SceneManager.LoadScene("Laberinto1");
    }

    public void tres()
    {
        escena = 3;
        SceneManager.LoadScene("Laberinto2");
    }

    void ayuda()
    {
        MenuInicial.SetActive(false);
        MenuAyuda.SetActive(true);
    }

    void atras()
    {
        MenuInicial.SetActive(true);
        MenuAyuda.SetActive(false);
    }

    void SalirDelJuego()
    {
        Application.Quit();
    }
}