using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using Xadrez;
using Xadrez.Enumerations;
/*Comando GetComponent:acessamos um objeto do jogo e
utilizamos alguma característica sua,se acessarmos
o script,podemos então modificar algum atributo ou 
realizar algum método da classe,logo o getcomponent é uma 
maneira de modificar o jogo ao vivo pelo script.
*/

/*basicamente o script que possui tudo sobre a peça e os 
Moveplates de cada uma*/
public class Chessman : MonoBehaviour {
    //Referencias aos objetos na cena
    public GameObject Controller;
    public GameObject MovePlate;
    public MovePlateSpawner movePlateSpawner;

    //Posição da peça no tabuleiro
    private int Xboard = -1;
    private int Yboard = -1;

    public int Moves = 0;

    //Qual jogador a peça pertence
    private string Player;

    public bool PawnDoubleMove = false;

    

    /*Sprites possíveis que cada peça pode receber
    Como esse código serve para todas as peças,
    quando o jogo começa a peça específica recebe
    um sprite.*/

    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    public void Activate() {
        //Recebe o controlador do jogo
        Controller = GameObject.FindGameObjectWithTag("GameController");

        //Take the instantiated location and adjust transform
        SetCoords();

        /*Função que faz cada peça receber seu sprite
         Cada peça vai automaticamente ter seu nome 
         por causa de uma função explicada mais tarde
         e o nome da peça é um atributo basico de cada
         objeto do unity,logo n é necessário colocar 
         como atributo no script*/

        switch (this.name) {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; Player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; Player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; Player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; Player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; Player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; Player = "black"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; Player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; Player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; Player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; Player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; Player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; Player = "white"; break;
        }
    }
    /* Receber as coordenadas da peça e transformar em 
    coordenadas aceitáveis pelo Unity */
    public void SetCoords() {

        /*recebe o valor da peça no tabuleiro e converte para
        coordenadas x e y*/
        float x = Xboard;
        float y = Yboard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Adiciona constantes (posição 0,0)
        x += -2.3f;
        y += -2.3f;

        //Transforma esses valores em valores aceitados pelo unity
        this.transform.position = new Vector3(x, y, -1.0f);
    }
    //Getters e Setters padrões
    public int GetXBoard() {
        return Xboard;
    }

    public int GetYBoard() {
        return Yboard;
    }

    public void SetXBoard(int x) {
        Xboard = x;
    }

    public void SetYBoard(int y) {
        Yboard = y;
    }
    /*Quando apertamos na Peça,Deletamos e geramos novos 
    moveplates*/
    private void OnMouseUp() {
        /*se o jogo não tiver acabado e o jogador atual
        for o jogado que está clicando*/
        if (!Controller.GetComponent<Game>().IsGameOver() && Controller.GetComponent<Game>().GetCurrentPlayer() == Player) {

            //Remove todas as moveplates
            DestroyMovePlates();

            //Cria novas moveplates
            InitiateMovePlates();
        }
    }
    //Destroi os moveplates atuais
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
    /*Função para iniciar os MovePlates,resumindo,
     quando apertamos em uma peça,recebemos o nome da peça
     então fazemos um switch case para cada nome*/
    public void InitiateMovePlates() {
        switch (this.name) {
            //se for uma rainha preta ou branca
            case "black_queen":
            case "white_queen":
                //Moveplates nas seguintes posições
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            //se for um cavalo preto ou branco
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            //se for um bispo preto ou branco
            case "black_bishop":
            case "white_bishop":
                //Moveplates nas seguintes posições
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            //se for um Rei preto ou branco
            case "black_king":
                CheckCastling();
                SurroundMovePlate();
                break;

            case "white_king":
                CheckCastling();
                //Moveplates nas seguintes posições
                //Obs:Provavel erro pois ele se move nas diagonais
                SurroundMovePlate();
                break;
            //se for uma torre preta ou branca
            case "black_rook":
            case "white_rook":
                //Moveplates nas seguintes posições
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            //se for um peão preto
            case "black_pawn":
                //Moveplates nas seguintes posições
                PawnMovePlate(Xboard, Yboard - 1);
                //VerifyEnPassant("black");
                break;
            case "white_pawn":
                // se for um peão branco
                //Moveplates nas seguintes posições
                PawnMovePlate(Xboard, Yboard + 1);
                VerifyEnPassant();
                break;

        }

    }

    public void LineMovePlate(int xIncrement, int yIncrement) {
        /*A variavel do tipo game "sc" vai receber um componente
        do objeto Controller,sendo esse componente o script Game*/
        Game sc = Controller.GetComponent<Game>();
        /*recebemos o x e o y da peça no tabuleiro e icrementamos
         de acordo com os argumentos do método*/
        int x = Xboard + xIncrement;
        int y = Yboard + yIncrement;
        /*enquanto a posição no tabuleiro existir,e não ouver nada 
          nessa posição*/
        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) {
            //Coloca um moveplate no tabuleiro na posição x e y
            MovePlateSpawn(x, y, Tipos.Normal);
            x += xIncrement;
            y += yIncrement;
        }
        /*se a posição existir e a posição for de um jogador que 
        não é o jogador atual*/
        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().Player != Player) {
            //Cria um moveplate de ataque
            MovePlateSpawn(x, y, Tipos.Attack);
        }
    }

    public void LMovePlate() {
        PointMovePlate(Xboard + 1, Yboard + 2);
        PointMovePlate(Xboard - 1, Yboard + 2);
        PointMovePlate(Xboard + 2, Yboard + 1);
        PointMovePlate(Xboard + 2, Yboard - 1);
        PointMovePlate(Xboard + 1, Yboard - 2);
        PointMovePlate(Xboard - 1, Yboard - 2);
        PointMovePlate(Xboard - 2, Yboard + 1);
        PointMovePlate(Xboard - 2, Yboard - 1);
    }

    public void SurroundMovePlate() {
        PointMovePlate(Xboard, Yboard + 1);
        PointMovePlate(Xboard, Yboard - 1);
        PointMovePlate(Xboard - 1, Yboard + 0);
        PointMovePlate(Xboard - 1, Yboard - 1);
        PointMovePlate(Xboard - 1, Yboard + 1);
        PointMovePlate(Xboard + 1, Yboard + 0);
        PointMovePlate(Xboard + 1, Yboard - 1);
        PointMovePlate(Xboard + 1, Yboard + 1);
    }

    public void PointMovePlate(int x, int y) {
        Game sc = Controller.GetComponent<Game>();
        //se a posição existir
        if (sc.PositionOnBoard(x, y)) {
            //recebemos a posição
            GameObject cp = sc.GetPosition(x, y);
            //se a posição for nula
            if (cp == null) {
                //colocamos uma moveplate
                MovePlateSpawn(x, y, Tipos.Normal);
            }
            //se a peça na posição for do jogador inimigo
            else if (cp.GetComponent<Chessman>().Player != Player) {
                //Coloca uma moveplate de ataque
                MovePlateSpawn(x, y, Tipos.Attack);
            }
        }
    }
    //movimento do peão
    public void PawnMovePlate(int x, int y) {
        Game sc = Controller.GetComponent<Game>();
        //se a posição existe
        if (sc.PositionOnBoard(x, y)) {
            //se a posição for nula
            if (sc.GetPosition(x, y) == null) {
                //cria um moveplate
                MovePlateSpawn(x, y, Tipos.Normal);
                //movimento duplo inicial
                if (Moves == 0 && sc.PositionOnBoard(x, y + 1) && sc.GetPosition(x, y + 1) == null && name == "white_pawn") {
                    MovePlateSpawn(x, y + 1, Tipos.DoubleMovePawn);

                }
                else if (Moves == 0 && sc.PositionOnBoard(x, y - 1) && sc.GetPosition(x, y - 1) == null && name == "black_pawn") {
                    MovePlateSpawn(x, y - 1, Tipos.DoubleMovePawn);

                }
            }

            //
            //se a posição x+1 existir no mapa, a posição não for nula e for uma peça do oponente
            //*Peão Branco pois é uma posição para frente
            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().Player != Player) {
                MovePlateSpawn(x + 1, y, Tipos.Attack);
            }
            //se a posição x-1 existir no mapa, a posição não for nula e for uma peça do oponente
            //*Peão branco pois é uma posição para trás
            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().Player != Player) {
                MovePlateSpawn(x - 1, y, Tipos.Attack);
            }
        }
    }

  
    void CheckCastling() {
        Game jogo = Controller.GetComponent<Game>();
        string player = Controller.GetComponent<Game>().GetCurrentPlayer();
        if (player == "white") {
            //jogo.GetPosition();
            if (PieceExist(4, 0, "white_king")) {
                if (PieceExist(0, 0, "white_rook") && jogo.GetPosition(1, 0) == null
                    && jogo.GetPosition(2, 0) == null && jogo.GetPosition(3, 0) == null) {
                    MovePlateSpawn(0, 0, Tipos.Castling);

                }
                if (PieceExist(7, 0, "white_rook") && jogo.GetPosition(5, 0) == null
                    && jogo.GetPosition(6, 0) == null) {
                    MovePlateSpawn(7, 0, Tipos.Castling);
                }
            }
        }
        else {
            if (PieceExist(4, 7, "black_king")) {
                if (PieceExist(0, 7, "black_rook") && jogo.GetPosition(1, 7) == null
                    && jogo.GetPosition(2, 7) == null && jogo.GetPosition(3, 7) == null) {
                    MovePlateSpawn(0, 7, Tipos.Castling);

                }
                if (PieceExist(7, 7, "black_rook") && jogo.GetPosition(5, 7) == null
                    && jogo.GetPosition(6, 7) == null) {
                    MovePlateSpawn(7, 7, Tipos.Castling);
                }
            }
        }


    }

    bool PieceExist(int x, int y, string namepiece) {
        Game jogo = Controller.GetComponent<Game>();
        GameObject posicao = Controller.GetComponent<Game>().GetPosition(x, y);

        if (jogo.PositionOnBoard(x, y) && !(jogo.GetPosition(x, y) == null)
            && posicao.name == namepiece) {
            return true;
        }
        else {
            return false;
        }

    }

    void VerifyEnPassant() {

        if (this.GetXBoard() == 0) {
            if (this.name == "white_pawn" && PieceExist(Xboard + 1, Yboard, "black_pawn")) {
                EnpassanContinuation(1, 1);
            }
            else if (this.name == "black_pawn" && PieceExist(Xboard + 1, Yboard, "white_pawn")) {
                EnpassanContinuation(1, -1);
            }

        }
        else if (this.GetXBoard() == 7) {
            if (this.name == "white_pawn" && PieceExist(Xboard - 1, Yboard, "black_pawn")) {
                EnpassanContinuation(-1, 1);
            }
            else if (this.name == "black_pawn" && PieceExist(Xboard - 1, Yboard, "white_pawn")) {
                EnpassanContinuation(-1, -1);
            }

        }
        else {
            if (this.name == "white_pawn" && PieceExist(Xboard + 1, Yboard, "black_pawn")) {
                EnpassanContinuation(1, 1);
            }
            else if (this.name == "black_pawn" && PieceExist(Xboard + 1, Yboard, "white_pawn")) {
                EnpassanContinuation(1, -1);
            }
            else if (this.name == "white_pawn" && PieceExist(Xboard - 1, Yboard, "black_pawn")) {
                EnpassanContinuation(-1, 1);
            }
            else if (this.name == "black_pawn" && PieceExist(Xboard - 1, Yboard, "white_pawn")) {
                EnpassanContinuation(-1, -1);
            }
        }

        void EnpassanContinuation(int x, int y) {
            Game game = Controller.GetComponent<Game>();
            GameObject Posicao = Controller.GetComponent<Game>().GetPosition(Xboard + x, Yboard);
            if (Posicao.GetComponent<Chessman>().GetXBoard() == game.LastMoves[0]
                && Posicao.GetComponent<Chessman>().GetYBoard() == game.LastMoves[1]
                && Posicao.GetComponent<Chessman>().Moves == 1 &&
                Posicao.GetComponent<Chessman>().PawnDoubleMove == true) {
                MovePlateSpawn(Xboard + x, Yboard + y, Tipos.EnPassant);
            }
        }
    }
}