using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chesscake.Utilitaries {
    public class MovePlateUtilitaries : MonoBehaviour {
        public void DestroyMovePlates() {
            /*Procura os objetos com tags(ou nome) de "Moveplate"
            e adiciona em um array do tipo GameObject*/
            GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
            //For passando por todos os valores do array
            for (int i = 0; i < movePlates.Length; i++) {
                //Função que destrói os objetos em cena
                Destroy(movePlates[i]);
            }
        }
    }
}
