using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chesscake.Enumerations;
public class PromotionButtons : MonoBehaviour
{
    public int escolha;
    public GameObject chesspiece;
    GameObject Panel;
    private void Start() {
       Panel = GameObject.Find("PromotionPanel");
    }
    public void Dama() {
        PromotePiece(1);
    }
    public void Torre() {
        PromotePiece(2);
    }
    public void Bispo() {
        PromotePiece(3);
    }
    public void Cavalo() {
        PromotePiece(4);
    }
    public void SetChesspiece(GameObject chesspiece) {
        this.chesspiece = chesspiece;
    }
    public void PromotePiece(int x) {
        if (chesspiece.GetComponent<Chessman>().GetTiposPeca() == TiposPeca.white_pawn) {
            switch (x) {
                
                case 1:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().white_queen;
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.queen);
                    break;
                case 2:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().white_rook;
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.rook);
                    break;
                case 3:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().white_bishop; 
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.bishop);
                    break;
                case 4:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().white_knight; 
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.knight);
                    break;
            }
            chesspiece.GetComponent<Chessman>().promovida = true;
            Panel.SetActive(false);
        }

        else {
            switch (x) {
                case 1:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().black_queen;
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.queen);
                    break;
                case 2:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().black_rook;
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.rook);
                    break;
                case 3:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().black_bishop; 
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.bishop);
                    break;
                case 4:
                    chesspiece.GetComponent<SpriteRenderer>().sprite = chesspiece.GetComponent<Chessman>().black_knight; 
                    chesspiece.GetComponent<Chessman>().SetTipoPeca(TiposPeca.knight);
                    break;
        }
            chesspiece.GetComponent<Chessman>().promovida = true;
            Panel.SetActive(false);
        }
    }
}
