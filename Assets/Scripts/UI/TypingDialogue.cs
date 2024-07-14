using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingDialogue : MonoBehaviour
{

    public TMP_Text textComp;
    public string[] lines;
    public float txtSpeed;

    private int index;

    private void Start()
    {
        textComp.text = String.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSeconds(txtSpeed);
        }

        if (textComp.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComp.text = lines[index];
        }
    }

    private void NextLine()
    {
        if (index < lines.Length -1)
        {
            index++;
            textComp.text = String.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
}
