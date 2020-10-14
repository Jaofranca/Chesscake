using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chesscake.Enumerations;
namespace Chesscake.Providers {
    public class SpecialMoves : MonoBehaviour {
        MovePlateSpawner movePlateSpawner;
        GameObject promotionPanel;

        /*int len = name.Length;
            string realName = name.Substring(0, len - 1);
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
            Game sc = Controller.GetComponent<Game>();*/
        private void Start() {
            movePlateSpawner = FindObjectOfType<MovePlateSpawner>();
            promotionPanel = GameObject.Find("PromotionPanel");
            promotionPanel.SetActive(false);

        }
        public void VerifyEnPassant(string name) {
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            int Xboard = chesspiece.GetXBoard();
            int Yboard = chesspiece.GetYBoard();
            if (Xboard == 0) {
                if (chesspiece.tipo == TiposPeca.white_pawn && PieceExist(Xboard + 1, Yboard, TiposPeca.black_pawn)) {
                    EnPassantMovePlate(1, 1, name);
                }
                else if (chesspiece.tipo == TiposPeca.black_pawn && PieceExist(Xboard + 1, Yboard, TiposPeca.white_pawn)) {
                    EnPassantMovePlate(1, -1, name);
                }
            }
            else if (Xboard == 7) {
                if (chesspiece.tipo == TiposPeca.white_pawn && PieceExist(Xboard - 1, Yboard, TiposPeca.black_pawn)) {
                    EnPassantMovePlate(-1, 1, name);
                }
                else if (chesspiece.tipo == TiposPeca.black_pawn && PieceExist(Xboard - 1, Yboard, TiposPeca.white_pawn)) {
                    EnPassantMovePlate(-1, -1, name);
                }
            }
            else {
                if (chesspiece.tipo == TiposPeca.white_pawn) {
                    if(PieceExist(Xboard + 1, Yboard, TiposPeca.black_pawn)) {
                        EnPassantMovePlate(1, 1, name);
                    }
                    if(PieceExist(Xboard - 1, Yboard, TiposPeca.black_pawn)) {
                        EnPassantMovePlate(-1, 1, name);
                    } 
                }          
                if (chesspiece.tipo == TiposPeca.black_pawn) {
                    if(PieceExist(Xboard + 1, Yboard, TiposPeca.white_pawn)) {
                        EnPassantMovePlate(1, -1, name);
                    }
                    if(PieceExist(Xboard - 1, Yboard, TiposPeca.white_pawn)) {
                        EnPassantMovePlate(-1, -1, name);
                    }
                }
            }
        }
        void EnPassantMovePlate(int x, int y, string name) {
            GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
            Game game = Controller.GetComponent<Game>();
            GameObject objeto = GameObject.Find(name);
            Chessman chesspiece = objeto.GetComponent<Chessman>();
            int Xboard = chesspiece.GetXBoard();
            int Yboard = chesspiece.GetYBoard();
            GameObject Posicao = Controller.GetComponent<Game>().GetPosition(Xboard + x, Yboard);
            if (Posicao.GetComponent<Chessman>().GetXBoard() == game.LastMoves[0]
                && Posicao.GetComponent<Chessman>().GetYBoard() == game.LastMoves[1]
                && Posicao.GetComponent<Chessman>().Moves == 1 &&
                Posicao.GetComponent<Chessman>().PawnDoubleMove == true) {
                movePlateSpawner.MovePlateSpawn(Xboard + x, Yboard + y, Tipos.EnPassant, name);
            }
        }
        //Auxiliares
        bool PieceExist(int x, int y, TiposPeca tipo) {
            GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
            Game jogo = Controller.GetComponent<Game>();
            GameObject posicao = jogo.GetPosition(x, y);
            if(posicao == null) {
                return false;
            }
            TiposPeca tipoPecaInimiga = posicao.GetComponent<Chessman>().GetTiposPeca();
            if (jogo.PositionOnBoard(x, y) && !(jogo.GetPosition(x, y) == null)
                && tipoPecaInimiga == tipo) {
                return true;
            }
            else {
                return false;
            }

        }
       
        public void CheckCastling(string name) {
            GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
            Game jogo = Controller.GetComponent<Game>();
            string player = Controller.GetComponent<Game>().GetCurrentPlayer();
            if (player == "white") {
                Chessman rei = GameObject.Find("white_king1").GetComponent<Chessman>();          
                Chessman torre1 = GameObject.Find("white_rook1").GetComponent<Chessman>();
                Chessman torre2 = GameObject.Find("white_rook2").GetComponent<Chessman>();
                //jogo.GetPosition();
                if (rei.GetXBoard() == 4 && rei.GetYBoard() == 0 && rei.Moves == 0) {
                    if (torre1.GetXBoard() == 0 && torre1.GetYBoard() == 0 && torre1.Moves == 0 && jogo.GetPosition(1, 0) == null
                        && jogo.GetPosition(2, 0) == null && jogo.GetPosition(3, 0) == null) {
                        movePlateSpawner.MovePlateSpawn(0, 0, Tipos.Castling, name);

                    }
                    if (torre2.GetXBoard() == 7 && torre2.GetYBoard() == 0 && jogo.GetPosition(5, 0) == null
                        && jogo.GetPosition(6, 0) == null && torre2.Moves == 0) {
                        movePlateSpawner.MovePlateSpawn(7, 0, Tipos.Castling, name);
                    }
                }
            }
            else {
                Chessman rei = GameObject.Find("black_king1").GetComponent<Chessman>();
                Chessman torre1 = GameObject.Find("black_rook1").GetComponent<Chessman>();
                Chessman torre2 = GameObject.Find("black_rook2").GetComponent<Chessman>();
                if (rei.GetXBoard() == 4 && rei.GetYBoard() == 7 && rei.Moves == 0) {
                    if (torre1.GetXBoard() == 0 && torre1.GetYBoard() == 7 && torre1.Moves == 0 && jogo.GetPosition(1, 7) == null
                        && jogo.GetPosition(2, 7) == null && jogo.GetPosition(3, 7) == null) {
                        movePlateSpawner.MovePlateSpawn(0, 7, Tipos.Castling, name);

                    }
                    if (torre2.GetXBoard() == 7 && torre2.GetYBoard() == 7 && torre2.Moves == 0 && jogo.GetPosition(5, 7) == null
                        && jogo.GetPosition(6, 7) == null) {
                        movePlateSpawner.MovePlateSpawn(7, 7, Tipos.Castling, name);
                    }
                }
            }


        }
        public bool VerifyPromotion(string name) {
            GameObject peca = GameObject.Find(name);
            TiposPeca tipoPeca = peca.GetComponent<Chessman>().tipo;
            if(peca.GetComponent<Chessman>().promovida == true) {
                return false;
            }
            else if (tipoPeca == TiposPeca.white_pawn && peca.GetComponent<Chessman>().GetYBoard() == 7) {
                promotionPanel.GetComponent<PromotionButtons>().SetChesspiece(peca);
                promotionPanel.SetActive(true);
                return true;
            }
            else if(tipoPeca == TiposPeca.black_pawn && peca.GetComponent<Chessman>().GetYBoard() == 0) {
                promotionPanel.GetComponent<PromotionButtons>().SetChesspiece(peca);
                promotionPanel.SetActive(true);
                return true;
            }
            return false;
        }
    }
}
