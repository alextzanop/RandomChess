using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Players")]
    public Player whitePlayer;
    public Player blackPlayer;

    [Header("Cards")]
    public GameObject cardTemplate;
    public GameObject[] cardPositions;

    public static GameController instance;

    Player playing;

    void Awake(){
        if(instance)
            Destroy(this);
        instance = this;
        playing = whitePlayer;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.K))
            AddCardToPosition();
    }

    public void AddCardToPosition(){
        GameObject position = null;
        foreach(GameObject cardPos in this.cardPositions){
            if(cardPos.transform.childCount==0){
                position = cardPos;
                break;
            }
        }
        if(!position) return;

        GameObject newCard = Instantiate(cardTemplate);
        Card c = newCard.AddComponent<Card>();
        newCard.transform.SetParent(position.transform);
        newCard.transform.localPosition = Vector3.zero;
        Vector2Int[] positions =  this.SelectRandomMove(this.playing);
        Debug.Log(string.Format("{0},{1}|{2},{3}",positions[0].x, positions[0].y,positions[1].x,positions[1].y));
        c.startPosition = positions[0];
        c.endPosition = positions[1];
        c.SetText();
        
    }

    public Vector2Int[] SelectRandomMove(Player _playing){
        Dictionary<Piece, List<Vector2Int>> legalMoves = new Dictionary<Piece, List<Vector2Int>>();
        foreach(Piece piece in _playing.pieces){
            BoardController.instance.CalculateLegalMoves(piece.currCell.gameObject);
            List<Vector2Int> pieceMoves = piece.GetLegalMoves();
            if(pieceMoves.Count<=0) continue;
            if(legalMoves.ContainsKey(piece))
                legalMoves[piece].AddRange(pieceMoves);
            else
                legalMoves.Add(piece, pieceMoves);
        }
        List<Piece> keyList = new List<Piece>(legalMoves.Keys);
        System.Random rand = new System.Random();
        int randKeyIndex = rand.Next(keyList.Count);

        Piece randomPiece = keyList[randKeyIndex];
        List<Vector2Int> randomMoveList = legalMoves[randomPiece];
        
        int randMoveIndex = rand.Next(randomMoveList.Count);
        Vector2Int randomMove = randomMoveList[randMoveIndex];
        Vector2Int startPosition = new Vector2Int(randomPiece.currCell.boardPosition.x, randomPiece.currCell.boardPosition.y);
        Vector2Int finalPosition = new Vector2Int(randomPiece.currCell.row+randomMove.x, 
            GlobalVars.instance.colToPos[randomPiece.currCell.column]+randomMove.y);

        //delete later
        BoardController.instance.GetCellObjs()[randomPiece.currCell.boardPosition.x][randomPiece.currCell.boardPosition.y].GetComponent<Cell>().Highlight();
        return new Vector2Int[2]{startPosition, finalPosition};
    }
}
