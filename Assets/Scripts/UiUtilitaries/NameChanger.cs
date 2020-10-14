using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NameChanger : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    
    void Start()

    {
        string texto1 = AskPlayerName.playername1;
        string texto2 = AskPlayerName.playername2;
        print(texto1);
        Player1.GetComponent<TextMeshProUGUI>().text = texto1;

        Player2.GetComponent<TextMeshProUGUI>().text = texto2;

    }

}
