using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Chesscake.Providers;
using Chesscake.Enumerations;
using TMPro;
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
    public PieceProvider provider;
    public GameObject panel;
    //O tabuleiro vai ser uma matriz de posições,matrix 8 por 8
    private GameObject[,] Positions = new GameObject[8, 8];
    //Array de peças do jogador preto e branco respectivamente
    private GameObject[] PlayerBlack = new GameObject[16];
    private GameObject[] PlayerWhite = new GameObject[16];

    //ultima peça movida
    public int[] LastMoves = new int[2]; 
    //Turno atual,começa no branco mas dps posso randomizar
    private string CurrentPlayer = "white";
    //Fim de jogo
    private bool GameOver = false;
    //Quando o jogo começa essa função é chamada
    public void Start() {
        provider = FindObjectOfType<PieceProvider>();
        panel = GameObject.Find("EndGamePanel");
        panel.SetActive(false);
        //Cria cada peça com seu x e y
        PlayerWhite = new GameObject[] { provider.Create("white_rook1", 0, 0,TiposPeca.rook),
            provider.Create("white_knight1", 1, 0,TiposPeca.knight),provider.Create("white_bishop1", 2, 0,TiposPeca.bishop)
            ,provider.Create("white_queen1", 3, 0,TiposPeca.queen),
            provider.Create("white_king1", 4, 0,TiposPeca.king),
            provider.Create("white_bishop2", 5, 0,TiposPeca.bishop), provider.Create("white_knight2", 6, 0,TiposPeca.knight),
            provider.Create("white_rook2", 7, 0,TiposPeca.rook),
            provider.Create("white_pawn1", 0, 1,TiposPeca.white_pawn), 
            provider.Create("white_pawn2", 1, 1,TiposPeca.white_pawn), provider.Create("white_pawn3", 2, 1,TiposPeca.white_pawn),
            provider.Create("white_pawn4", 3, 1,TiposPeca.white_pawn), provider.Create("white_pawn5", 4, 1,TiposPeca.white_pawn),
            provider.Create("white_pawn6", 5, 1,TiposPeca.white_pawn),provider.Create("white_pawn7", 6, 1,TiposPeca.white_pawn), 
            provider.Create("white_pawn8", 7, 1,TiposPeca.white_pawn) 
        };
        PlayerBlack = new GameObject[] { provider.Create("black_rook1", 0, 7,TiposPeca.rook),
            provider.Create("black_knight1",1,7,TiposPeca.knight),provider.Create("black_bishop1",2,7,TiposPeca.bishop), 
            provider.Create("black_queen1",3,7,TiposPeca.queen),
            provider.Create("black_king1",4,7,TiposPeca.king),
            provider.Create("black_bishop2",5,7,TiposPeca.bishop), provider.Create("black_knight2",6,7,TiposPeca.knight),
            provider.Create("black_rook2",7,7,TiposPeca.rook)
            ,provider.Create("black_pawn1", 0, 6,TiposPeca.black_pawn), provider.Create("black_pawn2", 1, 6,TiposPeca.black_pawn), 
            provider.Create("black_pawn3", 2, 6,TiposPeca.black_pawn),provider.Create("black_pawn4", 3, 6,TiposPeca.black_pawn), 
            provider.Create("black_pawn5", 4, 6,TiposPeca.black_pawn), provider.Create("black_pawn6", 5, 6,TiposPeca.black_pawn),
            provider.Create("black_pawn7", 6, 6,TiposPeca.black_pawn), provider.Create("black_pawn8", 7, 6,TiposPeca.black_pawn) 
        };
        //Coloca cada peça na matriz
        for (int i = 0; i < PlayerBlack.Length; i++) {
            SetPosition(PlayerBlack[i]);
            SetPosition(PlayerWhite[i]);
        }
        
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
    public void SetMoves(int x, int y) {
        LastMoves[0] = x;
        LastMoves[1] = y;
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
        /*if (GameOver == true && Input.GetMouseButtonDown(0)) {
            GameOver = false;
            //jogo reinicia carregando a tela denovo
            SceneManager.LoadScene("Game"); 
        }*/
    }
    //Verifica o vencedor(precisa de ajustes pois tem problema com texto)
    public void Winner(string playerWinner) {
        GameOver = true;
        panel.SetActive(true);
        if (playerWinner == "white") {
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = AskPlayerName.playername1;
        }
        else{
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = AskPlayerName.playername2;
        }
        
        //Using UnityEngine.UI is needed here
        /*GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;*/
    }
}
