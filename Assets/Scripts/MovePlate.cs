using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chesscake.Utilitaries;
using Chesscake.Enumerations;
using Chesscake.Providers;
/*Comando GetComponent:acessamos um objeto do jogo e
utilizamos alguma característica sua,se acessarmos
o script,podemos então modificar algum atributo ou 
realizar algum método da classe,logo o getcomponent é uma 
maneira de modificar o jogo ao vivo pelo script.
*/
public class MovePlate : MonoBehaviour {
    //O Script que Controla o Moveplate

    public GameObject Controller;
    public MovePlateUtilitaries movePlateUtilitaries;
    public SpecialMoves specialMoves;
    //A peça que tem ligação com essa moveplate
    GameObject Reference;
    //localização no tabuleiro
    int MatrixX;
    int MatrixY;
    public bool Castling;
    public bool Attack;
    public bool DoubleMovePawn;
    public bool EnPassant;
    public bool Normal;
    //Quando o Codigo começa a rodar
    public void Start() {
        movePlateUtilitaries = FindObjectOfType<MovePlateUtilitaries>();
        specialMoves = FindObjectOfType<SpecialMoves>();
        if (Attack) {
            //Muda a cor MovePlate para Vermelho
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }else if (Castling) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }else if (EnPassant) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
    }
    //Quando apertamos no moveplate
    public void OnMouseUp() {
        Controller = GameObject.FindGameObjectWithTag("GameController");
        Controller.GetComponent<Game>().SetPositionEmpty(Reference.GetComponent<Chessman>().GetXBoard(),
            Reference.GetComponent<Chessman>().GetYBoard());
        //Se o Moveplate tiver Attack = true
        if (Attack) {
            //pegamos a posição onde o moveplate está
            GameObject cp = Controller.GetComponent<Game>().GetPosition(MatrixX, MatrixY);

            //se a peça onde o moveplate está for o rei ou rainha preto ou branco,o jogo acaba
            int len = cp.name.Length;
            string realName = cp.name.Substring(0, len - 1);

            print(realName);
            if (realName == "white_king") Controller.GetComponent<Game>().Winner("black");
            if (realName == "black_king") Controller.GetComponent<Game>().Winner("white");
            //destrói a peça

            Destroy(cp);
            MovePiece();
        }
        //Deixa a posição da peça atual em branco -- SetpositionEmpty(peça atual.x().y())
        if (Castling) {
                //MatrixX == -2.3 && MatrixY == -2.3
                if (MatrixX == 0) {
                    CastlingMove(2, 3);
                }
                else {
                    CastlingMove(-2,4);
                }              
        }
        if (EnPassant) {
            int len = Reference.name.Length;
            string realName = Reference.name.Substring(0, len - 1);
            if (realName == "white_pawn") {
                GameObject cp = Controller.GetComponent<Game>().GetPosition(MatrixX, MatrixY - 1);
                Destroy(cp);
            }
            else {
                GameObject cp = Controller.GetComponent<Game>().GetPosition(MatrixX, MatrixY +1);
                Destroy(cp);
            }
            MovePiece();

        }
        if(Normal || DoubleMovePawn){
            //Move a peça de acordo com o moveplate clicado
            //Atribui novos valores a peça de acordo com o x e y do moveplate atual.
            if (DoubleMovePawn) {
                Reference.GetComponent<Chessman>().PawnDoubleMove = true;
            }
            MovePiece();
        }
        print(Reference);
        
        //Coloca a peça na posição real do tabuleiro de acordo com o novo X e Y
        Controller.GetComponent<Game>().SetPosition(Reference);
        specialMoves.VerifyPromotion(Reference.name);
        Chessman peca = Reference.GetComponent<Chessman>();
        peca.Moves += 1;
        Controller.GetComponent<Game>().SetMoves(MatrixX, MatrixY);
        //Troca de jogador
        Controller.GetComponent<Game>().NextTurn();
        //Destroi as moveplates existentes,incluindo a atual
        movePlateUtilitaries.DestroyMovePlates();
    }
    
    public void SetCoords(int x, int y) {
        MatrixX = x;
        MatrixY = y;
    }
    //A peça que tem relação com essa moveplate
    public void SetReference(GameObject obj) {
        Reference = obj;
    }

    public GameObject GetReference() {
        return Reference;
    }
    public void MovePiece() {
        Reference.GetComponent<Chessman>().SetXBoard(MatrixX);
        Reference.GetComponent<Chessman>().SetYBoard(MatrixY);
        Reference.GetComponent<Chessman>().SetCoords();
    }
    public void MovePiece(GameObject peca,int MatrixX,int MatrixY) {
        peca.GetComponent<Chessman>().SetXBoard(MatrixX);
        print(MatrixX);
        peca.GetComponent<Chessman>().SetYBoard(MatrixY);
        print(MatrixY);
        peca.GetComponent<Chessman>().SetCoords();
    }
    void CastlingMove(int offset,int newPosition) {
        GameObject torre = Controller.GetComponent<Game>().GetPosition(MatrixX, MatrixY);
        Controller.GetComponent<Game>().SetPositionEmpty(MatrixX, MatrixY);
        MovePiece(Reference, MatrixX + offset, MatrixY);
        MovePiece(torre, newPosition, MatrixY);
        torre.GetComponent<Chessman>().Moves += 1;
    }
}