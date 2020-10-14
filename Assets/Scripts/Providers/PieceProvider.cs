using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chesscake.Enumerations;
namespace Chesscake.Providers {
    public class PieceProvider : MonoBehaviour {
        public GameObject ChessPiece;
        public GameObject Create(string name, int x, int y,TiposPeca tipo) {
            Game game = FindObjectOfType<Game>();
            ChessPiece = game.GetComponent<Game>().ChessPiece;
            GameObject obj = Instantiate(ChessPiece, new Vector3(0, 0, -1), Quaternion.identity);
            Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
            cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
            cm.SetXBoard(x);
            cm.SetYBoard(y);
            cm.SetTipoPeca(tipo);
            cm.Activate(name); //It has everything set up so it can now Activate()
            
            return obj;
        }
    }
}
