using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*Comando GetComponent:acessamos um objeto do jogo e
utilizamos alguma característica sua,se acessarmos
o script,podemos então modificar algum atributo ou 
realizar algum método da classe,logo o getcomponent é uma 
maneira de modificar o jogo ao vivo pelo script.
*/
//Script que toma conta do jogo em Si
public class Game : MonoBehaviour {


    //Peça de Referencia
    public GameObject chesspiece;

    //O tabuleiro vai ser uma matriz de posições,matrix 8 por 8
    private GameObject[,] positions = new GameObject[8, 8];
    //Array de peças do jogador preto e branco respectivamente
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    //Turno atual,começa no branco mas dps posso randomizar
    private string currentPlayer = "white";

    //Fim de jogo
    private bool gameOver = false;

    //Quando o jogo começa essa função é chamada
    public void Start() {
        //Cria cada peça com seu x e y
        playerWhite = new GameObject[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) };
        playerBlack = new GameObject[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) };

        //Coloca cada peça na matriz
        for (int i = 0; i < playerBlack.Length; i++) {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y) {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }
    //Coloca as peças na matriz
    public void SetPosition(GameObject obj) {
        Chessman cm = obj.GetComponent<Chessman>();

        //sobrescreve o espaço vazio ou qualquer coisa que esteja la
        //adiciona no x e y a peça de xadrez.
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }
    //Deixa a posição nula
    public void SetPositionEmpty(int x, int y) {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y) {
        return positions[x, y];
    }
    //verifica se a posição existe de verdade
    public bool PositionOnBoard(int x, int y) {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer() {
        return currentPlayer;
    }
    //verifica se o jogo acabou
    public bool IsGameOver() {
        return gameOver;
    }
    //função que muda de turno basicamente mudando o player atual
    public void NextTurn() {
        if (currentPlayer == "white") {
            currentPlayer = "black";
        }
        else {
            currentPlayer = "white";
        }
    }
    //função que da update a cada frame
    public void Update() {
        //se o jogo acabou e o player apertou um botão
        if (gameOver == true && Input.GetMouseButtonDown(0)) {
            gameOver = false;
            //jogo reinicia carregando a tela denovo
           
            SceneManager.LoadScene("Game"); 
        }
    }
    //Verifica o vencedor(precisa de ajustes pois tem problema com texto)
    public void Winner(string playerWinner) {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}