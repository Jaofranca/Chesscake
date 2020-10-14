using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chesscake.Enumerations;
namespace Chesscake.Providers {
    public class MovePlateSpawnTypes : MonoBehaviour {
        public MovePlateSpawner movePlateSpawner;
        public void LineMovePlate(int xIncrement, int yIncrement, string name) {
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
            /*A variavel do tipo game "sc" vai receber um componente
            do objeto Controller,sendo esse componente o script Game*/
            Game sc = Controller.GetComponent<Game>();
            /*recebemos o x e o y da peça no tabuleiro e icrementamos
             de acordo com os argumentos do método*/
            int x = chesspiece.GetXBoard() + xIncrement;
            int y = chesspiece.GetYBoard() + yIncrement;
            /*enquanto a posição no tabuleiro existir,e não ouver nada 
              nessa posição*/
            while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) {
                //Coloca um moveplate no tabuleiro na posição x e y
                movePlateSpawner.MovePlateSpawn(x, y, Tipos.Normal, chesspiece.name);
                x += xIncrement;
                y += yIncrement;
            }
            /*se a posição existir e a posição for de um jogador que 
            não é o jogador atual*/
            if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().GetPlayer() != chesspiece.GetPlayer()) {
                //Cria um moveplate de ataque
                movePlateSpawner.MovePlateSpawn(x, y, Tipos.Attack, chesspiece.name);
            }
        }
        public void PointMovePlate(int x, int y, string name) {
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
            Game sc = Controller.GetComponent<Game>();
            //se a posição existir
            if (sc.PositionOnBoard(x, y)) {
                //recebemos a posição
                GameObject cp = sc.GetPosition(x, y);
                //se a posição for nula
                if (cp == null) {
                    //colocamos uma moveplate
                    movePlateSpawner.MovePlateSpawn(x, y, Tipos.Normal, name);
                }
                //se a peça na posição for do jogador inimigo
                else if (cp.GetComponent<Chessman>().GetPlayer() != chesspiece.GetPlayer()) {
                    //Coloca uma moveplate de ataque
                    movePlateSpawner.MovePlateSpawn(x, y, Tipos.Attack, name);
                }
            }
        }
        public void PawnMovePlate(int x, int y, string name) {
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
            Game sc = Controller.GetComponent<Game>();
            //se a posição existe
            if (sc.PositionOnBoard(x, y)) {
                //se a posição for nula
                if (sc.GetPosition(x, y) == null) {
                    //cria um moveplate
                    movePlateSpawner.MovePlateSpawn(x, y, Tipos.Normal, name);
                    //movimento duplo inicial
                    if (chesspiece.Moves == 0 && sc.PositionOnBoard(x, y + 1) && sc.GetPosition(x, y + 1) == null && chesspiece.tipo == TiposPeca.white_pawn) {
                        movePlateSpawner.MovePlateSpawn(x, y + 1, Tipos.DoubleMovePawn, name);
                    }
                    else if (chesspiece.Moves == 0 && sc.PositionOnBoard(x, y - 1) && sc.GetPosition(x, y - 1) == null && chesspiece.tipo == TiposPeca.black_pawn) {
                        movePlateSpawner.MovePlateSpawn(x, y - 1, Tipos.DoubleMovePawn, name);
                    }
                }
                //se a posição x+1 existir no mapa, a posição não for nula e for uma peça do oponente
                //*Peão Branco pois é uma posição para frente
                if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().GetPlayer() != chesspiece.GetPlayer()) {
                    movePlateSpawner.MovePlateSpawn(x + 1, y, Tipos.Attack, name);
                }
                //se a posição x-1 existir no mapa, a posição não for nula e for uma peça do oponente
                //*Peão branco pois é uma posição para trás
                if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().GetPlayer() != chesspiece.GetPlayer()) {
                    movePlateSpawner.MovePlateSpawn(x - 1, y, Tipos.Attack, name);
                }
            }
        }
        public void LMovePlate(string name) {
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            int Xboard = chesspiece.GetXBoard();
            int Yboard = chesspiece.GetYBoard();
            PointMovePlate(Xboard + 1, Yboard + 2, name);
            PointMovePlate(Xboard - 1, Yboard + 2, name);
            PointMovePlate(Xboard + 2, Yboard + 1, name);
            PointMovePlate(Xboard + 2, Yboard - 1, name);
            PointMovePlate(Xboard + 1, Yboard - 2, name);
            PointMovePlate(Xboard - 1, Yboard - 2, name);
            PointMovePlate(Xboard - 2, Yboard + 1, name);
            PointMovePlate(Xboard - 2, Yboard - 1, name);
        }
        public void SurroundMovePlate(string name) {
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            int Xboard = chesspiece.GetXBoard();
            int Yboard = chesspiece.GetYBoard();
            PointMovePlate(Xboard, Yboard + 1, name);
            PointMovePlate(Xboard, Yboard - 1, name);
            PointMovePlate(Xboard - 1, Yboard + 0, name);
            PointMovePlate(Xboard - 1, Yboard - 1, name);
            PointMovePlate(Xboard - 1, Yboard + 1, name);
            PointMovePlate(Xboard + 1, Yboard + 0, name);
            PointMovePlate(Xboard + 1, Yboard - 1, name);
            PointMovePlate(Xboard + 1, Yboard + 1, name);
        }

    }
}
