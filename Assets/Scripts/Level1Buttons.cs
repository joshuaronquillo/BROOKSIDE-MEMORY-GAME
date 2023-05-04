using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Buttons : MonoBehaviour
{
    [SerializeField]
    private Transform PuzzleField;
    
    [SerializeField]
    private GameObject btn;

    void Awake()
    {
        for( int i = 0; i <8; i++) 
        {
            GameObject button= Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(PuzzleField, false);

        }
    }
}
