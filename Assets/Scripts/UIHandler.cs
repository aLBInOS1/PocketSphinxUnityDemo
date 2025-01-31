﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text mic;
    [SerializeField] private Text phrase;
    public AudioSource audioSource;

    private SphinxExample sphinx;

    private IEnumerator Start()
    {
        sphinx = FindObjectOfType<SphinxExample>();
        sphinx.OnSpeechRecognized += UpdateUI;

        // Wait until the mic is initialized before we setup the UI.
        while(sphinx.mic == null)
        {
            yield return null;
        }
        SetupUI();
    }

    private void SetupUI()
    {
        mic.text = sphinx.mic.Name;
        phrase.text = $"Say {sphinx.keyphrase}";
    }

    private void UpdateUI(string str)
    {
        StartCoroutine(UpdateUIEnum(str));
    }

    private IEnumerator UpdateUIEnum(string str)
    {
        phrase.text = $"Keyphrase recognized! \nPhrase: {str}";
        audioSource.Play();
        yield return new WaitForSeconds(1.3f);
        phrase.text = $"Say {sphinx.keyphrase}";
    }
}
