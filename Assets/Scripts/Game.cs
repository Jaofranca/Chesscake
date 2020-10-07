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
    public GameObject ChessPiece;

    //O tabuleiro vai ser uma matriz de posições,matrix 8 por 8
    private GameObject[,] Positions = new GameObject[8, 8];
    //Array de peças do jogador preto e branco respectivamente
    private GameObject[] PlayerBlack = new GameObject[16];
    private GameObject[] PlayerWhite = new GameObject[16];

    //ultima peça movida
    public int[] LastMoves = new int[2];
    
    public void SetMoves(int x, int y) {
        LastMoves[0] = x;
        LastMoves[1] = y;
    }

    //Turno atual,começa no branco mas dps posso randomizar
    private string CurrentPlayer = "white";

    //Fim de jogo
    private bool GameOver = false;

    //Quando o jogo começa essa função é chamada
    public void Start() {
        //Cria cada peça com seu x e y
        PlayerWhite = new GameObject[] { Create("white_rook", 0, 0), 
            Create("white_knight", 1, 0),Create("white_bishop", 2, 0), Create("white_queen", 3, 0), 
            Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), 
            Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) 
        };
        PlayerBlack = new GameObject[] { Create("black_rook", 0, 7), 
            Create("black_knight",1,7),Create("black_bishop",2,7), Create("black_queen",3,7), 
            Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), 
            Create("black_rook",7,7)
            ,Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) 
        };

        //Coloca cada peça na matriz
        for (int i = 0; i < PlayerBlack.Length; i++) {
            SetPosition(PlayerBlack[i]);
            SetPosition(PlayerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y) {
        GameObject obj = Instantiate(ChessPiece, new Vector3(0, 0, -1), Quaternion.identity);
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
        Positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }
    //Deixa a posição nula
    public void SetPositionEmpty(int x, int y) {
        Positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y) {
        return Positions[x, y];
    }
    //verifica se a posição existe de verdade
    public bool PositionOnBoard(int x, int y) {
        if (x < 0 || y < 0 || x >= Positions.GetLength(0) || y >= Positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer() {
        return CurrentPlayer;
    }
    //verifica se o jogo acabou
    public bool IsGameOver() {
        return GameOver;
    }
    //função que muda de turno basicamente mudando o player atual
    public void NextTurn() {
        if (CurrentPlayer == "white") {
            CurrentPlayer = "black";
           
        }
        else {
            CurrentPlayer = "white";
            
        }
    
    }
    //função que da update a cada frame
    public void Update() {
        //se o jogo acabou e o player apertou um botão
        if (GameOver == true && Input.GetMouseButtonDown(0)) {
            GameOver = false;
            //jogo reinicia carregando a tela denovo
           
            SceneManager.LoadScene("Game"); 
        }
    }
    public void openPanel() {
        GameObject panel = GameObject.FindGameObjectWithTag("PromotionPanel");
        panel.SetActive(true);
    }
    //Verifica o vencedor(precisa de ajustes pois tem problema com texto)
    public void Winner(string playerWinner) {
        GameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}