using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Comando GetComponent:acessamos um objeto do jogo e
utilizamos alguma característica sua,se acessarmos
o script,podemos então modificar algum atributo ou 
realizar algum método da classe,logo o getcomponent é uma 
maneira de modificar o jogo ao vivo pelo script.
*/
public class MovePlate : MonoBehaviour {
    //O Script que Controla o Moveplate

    ////Referencias ao Controller
    public GameObject Controller;

    //A peça que tem ligação com essa moveplate
    GameObject Reference = null;

    //localização no tabuleiro
    int MatrixX;
    int MatrixY;

    public bool Castling = false;
    //falso:Movimento;True:Attack
    public bool Attack = false;
    public bool DoubleMovePawn = false;
    public bool EnPassant = false;

    

    //Quando o Codigo começa a rodar
    public void Start() {
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

        //Se o Moveplate tiver Attack = true
        if (Attack) {
            //pegamos a posição onde o moveplate está
            GameObject cp = Controller.GetComponent<Game>().GetPosition(MatrixX, MatrixY);
            //se a peça onde o moveplate está for o rei ou rainha preto ou branco,o jogo acaba
            if (cp.name == "white_king") Controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") Controller.GetComponent<Game>().Winner("white");
            //destrói a peça
            Destroy(cp);
        }

        //Deixa a posição da peça atual em branco -- SetpositionEmpty(peça atual.x().y())
        Controller.GetComponent<Game>().SetPositionEmpty(Reference.GetComponent<Chessman>().GetXBoard(),
            Reference.GetComponent<Chessman>().GetYBoard());

        if (Castling) {
            string player = Controller.GetComponent<Game>().GetCurrentPlayer();
            if(player == "white") {
                //MatrixX == -2.3 && MatrixY == -2.3
                if (MatrixX == 0 && MatrixY == 0) {
                    CastlingMove(2, 3);
                }
                else {
                    CastlingMove(-2,4);
                }
            }
            else {
                if (MatrixX == 0 && MatrixY == 7) {
                    CastlingMove(2, 3);
                }
                else {
                    CastlingMove(-2, 4);
                }
            }            
        }
        if (EnPassant) {
            GameObject cp = Controller.GetComponent<Game>().GetPosition(MatrixX, MatrixY-1);
            Destroy(cp);
            Reference.GetComponent<Chessman>().SetXBoard(MatrixX);
            Reference.GetComponent<Chessman>().SetYBoard(MatrixY);
            Reference.GetComponent<Chessman>().SetCoords();
        }
        else {
            //Move a peça de acordo com o moveplate clicado
            //Atribui novos valores a peça de acordo com o x e y do moveplate atual.
            if (DoubleMovePawn) {
                Reference.GetComponent<Chessman>().PawnDoubleMove = true;
            }
            Reference.GetComponent<Chessman>().SetXBoard(MatrixX);
            Reference.GetComponent<Chessman>().SetYBoard(MatrixY);
            Reference.GetComponent<Chessman>().SetCoords();
        }
        
        //Coloca a peça na posição real do tabuleiro de acordo com o novo X e Y
        Controller.GetComponent<Game>().SetPosition(Reference);
        VerifyPromotion();
        Chessman peca = Reference.GetComponent<Chessman>();
        peca.Moves += 1;
        //Controller.GetComponent<Game>().SetUltimaPecaNome(Reference.name);
        Controller.GetComponent<Game>().SetMoves(MatrixX, MatrixY);
        //Troca de jogador
        Controller.GetComponent<Game>().NextTurn();

        //Destroi as moveplates existentes,incluindo a atual
        Reference.GetComponent<Chessman>().DestroyMovePlates();
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
    void VerifyPromotion() {
        Sprite sprite= Reference.GetComponent<Chessman>().white_queen;
        if (Reference.name == "white_pawn") {
            int x = Reference.GetComponent<Chessman>().GetXBoard();
            int y = Reference.GetComponent<Chessman>().GetYBoard();
            if (y == 7) {
                Reference.GetComponent<SpriteRenderer>().sprite = sprite;
                Reference.GetComponent<Chessman>().name = "white_queen";
                

            }
        }
        //this.GetComponent<SpriteRenderer>().sprite = black_queen


    }
    void CastlingMove(int offset,int newPosition) {
        GameObject torre1 = Controller.GetComponent<Game>().GetPosition(MatrixX, MatrixY);
        Controller.GetComponent<Game>().SetPositionEmpty(MatrixX, MatrixY);
        Reference.GetComponent<Chessman>().SetXBoard(MatrixX + offset);
        Reference.GetComponent<Chessman>().SetYBoard(MatrixY);
        Reference.GetComponent<Chessman>().SetCoords();
        torre1.GetComponent<Chessman>().SetXBoard(newPosition);
        torre1.GetComponent<Chessman>().SetYBoard(MatrixY);
        torre1.GetComponent<Chessman>().SetCoords();
        torre1.GetComponent<Chessman>().Moves += 1;
    }
}