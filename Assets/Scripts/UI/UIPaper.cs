using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPaper : MonoBehaviour
{

    [SerializeField] private TMP_Text paperTexts;
    [SerializeField] private List<GameObject> allPapersInScene = new List<GameObject>();
    [SerializeField] private int totalPapers;

    private void Start()
    {
        GameObject[] papers = GameObject.FindGameObjectsWithTag("Paper");
        foreach (var variablPaper in papers)
        {
            allPapersInScene.Add(variablPaper);
        }

        paperTexts.text = totalPapers + " / " + allPapersInScene.Count;
    }

    public void AddPaper()
    {
        totalPapers += 1;
        paperTexts.text = totalPapers + " / " + allPapersInScene.Count;
    }

    public int GetTotalPapers()
    {
        return totalPapers;
    }
}
