using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Windows.Speech;

public class PartidaTerminada : MonoBehaviour
{
    public KeywordRecognizer m_Recognizer;
    Dictionary<string, Action> wtaction;

    void Start()
    {
        wtaction = new Dictionary<string, Action>();

        //Reiniciar
        wtaction.Add("reiniciar", reiniciar);
        wtaction.Add("restart", reiniciar);
        wtaction.Add("reintentar", reiniciar);

        //Salir del juego
        wtaction.Add("salir", salirMenu);
        wtaction.Add("quit", salirMenu);
        wtaction.Add("exit", salirMenu);
        wtaction.Add("menu", salirMenu);

        m_Recognizer = new KeywordRecognizer(wtaction.Keys.ToArray());
        m_Recognizer.OnPhraseRecognized += wordrecognizer;
        m_Recognizer.Start();
    }

    private void wordrecognizer(PhraseRecognizedEventArgs word)
    {
        Debug.Log(word.text);
        wtaction[word.text].Invoke();
    }

    public void reiniciar()
    {
        if (MenuManager.escena == 1)
        {
            SceneManager.LoadScene(1);
        }
        if (MenuManager.escena == 2)
        {
            SceneManager.LoadScene(2);
        }
        if (MenuManager.escena == 3)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void salirMenu()
    {
        SceneManager.LoadScene(0);
    }
}