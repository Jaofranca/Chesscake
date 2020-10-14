using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Xsl;
using UnityEngine;
using Chesscake.Utilitaries;
using Chesscake.Providers;
using Chesscake.Enumerations;
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
    public MovePlateUtilitaries movePlateUtilitaries;
    //Posição da peça no tabuleiro
    private int Xboard = -1;
    private int Yboard = -1;
    public int Moves;
    public TiposPeca tipo;
    //Qual jogador a peça pertence
    private string Player;
    public bool PawnDoubleMove = false;
    public bool promovida = false;
    /*Sprites possíveis que cada peça pode receber
    Como esse código serve para todas as peças,
    quando o jogo começa a peça específica recebe
    um sprite.*/
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;
    private void Start() {
        movePlateSpawner = FindObjectOfType<MovePlateSpawner>();
        movePlateUtilitaries = FindObjectOfType<MovePlateUtilitaries>();
    }
    public void Activate(string name) {
        int len = name.Length;
        string realName = name.Substring(0, len - 1);
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

        switch (realName) {
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
    public TiposPeca GetTiposPeca() {
        return tipo;
    }
    public string GetPlayer() {
        return Player;
    }
    public void SetXBoard(int x) {
        Xboard = x;
    }
    public void SetYBoard(int y) {
        Yboard = y;
    }
    public void SetTipoPeca(TiposPeca tipopeca) {
        tipo = tipopeca;
    }
    /*Quando apertamos na Peça,Deletamos e geramos novos 
    moveplates*/
    private void OnMouseUp() {
        /*se o jogo não tiver acabado e o jogador atual
        for o jogado que está clicando*/
        if (!Controller.GetComponent<Game>().IsGameOver() && Controller.GetComponent<Game>().GetCurrentPlayer() == Player) {
            //Remove todas as moveplates
            movePlateUtilitaries.DestroyMovePlates();
            //Cria novas moveplates
            movePlateSpawner.InitiateMovePlates(this.name);
        }
    }
}