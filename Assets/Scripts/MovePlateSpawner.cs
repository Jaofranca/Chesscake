using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xadrez.Enumerations;

namespace Xadrez{
    public class MovePlateSpawner : MonoBehaviour {
        public void MovePlateSpawn(int matrixX, int matrixY, Tipos tipo) {
            
            /*Recebe os valores do tabuleiro para converter para
            coordenadas x e y*/
            float x = matrixX;
            float y = matrixY;

            //Adjust by variable offset
            x *= 0.66f;
            y *= 0.66f;

            //Adiciona constantes (posição 0,0)
            x += -2.3f;
            y += -2.3f;
            //Objeto mp recebe o "objeto" moveplate no xyz
            //(ainda não está no tabuleiro,e sim possui um x y e z teorico )
            GameObject mp = Instantiate(MovePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
            //Mpscript recebe o Component do moveplate
            MovePlate mpScript = mp.GetComponent<MovePlate>();
            switch (tipo) {
                case Tipos.Attack:
                    mpScript.Attack = true;
                    break;
                case Tipos.Castling:
                    mpScript.Castling = true;
                    break;
                case Tipos.DoubleMovePawn:
                    mpScript.DoubleMovePawn = true;
                    break;
                case Tipos.EnPassant:
                    mpScript.EnPassant = true;
                    break;


            }
            //Coloca a peça atual como peça ligada ao Moveplate
           
            mpScript.SetReference(gameObject);
            /*setamos as coordenadas no tabuleiro,logo 
            agora o moveplate existe*/
            mpScript.SetCoords(matrixX, matrixY);
        }
    }
}
