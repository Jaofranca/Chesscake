using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chesscake.Enumerations;
namespace Chesscake.Providers {
    public class MovePlateSpawner : MonoBehaviour {
        GameObject movePlate;
        public MovePlateSpawnTypes movePlateSpawnTypes;
        public SpecialMoves specialMoves;

        public void MovePlateSpawn(int matrixX, int matrixY, Tipos tipo, string name) {
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
            movePlate = FindObjectOfType<Chessman>().MovePlate;
            GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
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
                case Tipos.Normal:
                    mpScript.Normal = true;
                    break;
            }
            GameObject objeto = GameObject.Find(name);
            //Coloca a peça atual como peça ligada ao Moveplate
            mpScript.SetReference(objeto);
            /*setamos as coordenadas no tabuleiro,logo 
            agora o moveplate existe*/
            mpScript.SetCoords(matrixX, matrixY);
        }
        public void InitiateMovePlates(string name) {
            int len = name.Length;
            string realName = name.Substring(0, len - 1);
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            int Xboard = chesspiece.GetXBoard();
            int Yboard = chesspiece.GetYBoard();
            switch (chesspiece.tipo) {

                //se for uma rainha preta ou branca
                case TiposPeca.queen:
                    //Moveplates nas seguintes posições
                    movePlateSpawnTypes.LineMovePlate(1, 0, name);
                    movePlateSpawnTypes.LineMovePlate(0, 1, name);
                    movePlateSpawnTypes.LineMovePlate(1, 1, name);
                    movePlateSpawnTypes.LineMovePlate(-1, 0, name);
                    movePlateSpawnTypes.LineMovePlate(0, -1, name);
                    movePlateSpawnTypes.LineMovePlate(-1, -1, name);
                    movePlateSpawnTypes.LineMovePlate(-1, 1, name);
                    movePlateSpawnTypes.LineMovePlate(1, -1, name);
                    break;
                //se for um cavalo preto ou branco
                case TiposPeca.knight:
                    movePlateSpawnTypes.LMovePlate(name);
                    break;
                //se for um bispo preto ou branco
                case TiposPeca.bishop:
                    //Moveplates nas seguintes posições
                    movePlateSpawnTypes.LineMovePlate(1, 1, name);
                    movePlateSpawnTypes.LineMovePlate(1, -1, name);
                    movePlateSpawnTypes.LineMovePlate(-1, 1, name);
                    movePlateSpawnTypes.LineMovePlate(-1, -1, name);
                    break;
                //se for um Rei preto ou branco
                case TiposPeca.king:
                    //Moveplates nas seguintes posições
                    //Obs:Provavel erro pois ele se move nas diagonais
                    movePlateSpawnTypes.SurroundMovePlate(name);
                    specialMoves.CheckCastling(name);
                    break;
                //se for uma torre preta ou branca
                case TiposPeca.rook:
                    //Moveplates nas seguintes posições
                    movePlateSpawnTypes.LineMovePlate(1, 0, name);
                    movePlateSpawnTypes.LineMovePlate(0, 1, name);
                    movePlateSpawnTypes.LineMovePlate(-1, 0, name);
                    movePlateSpawnTypes.LineMovePlate(0, -1, name);
                    break;
                //se for um peão preto
                case TiposPeca.black_pawn:
                    //Moveplates nas seguintes posições
                    movePlateSpawnTypes.PawnMovePlate(Xboard, Yboard - 1, name);
                    //VerifyEnPassant("black");
                    if (Yboard == 3) {
                        specialMoves.VerifyEnPassant(name);
                    }
                    break;
                case TiposPeca.white_pawn:
                    // se for um peão branco
                    //Moveplates nas seguintes posições
                    movePlateSpawnTypes.PawnMovePlate(Xboard, Yboard + 1, name);
                    if(Yboard == 4) {
                        specialMoves.VerifyEnPassant(name);
                    }
                    
                    break;
            }
        }
    }
}
