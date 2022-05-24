using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PieceType {
    None,
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}
enum Direction {
    None = -1,
    Up = 0,
    UpRight = 1,
    Right = 2,
    DownRight = 3,
    Down = 4,
    DownLeft = 5,
    Left = 6,
    UpLeft = 7
}

public class BoardController : MonoBehaviour
{

    [SerializeField] 
    GameObject cellPrefab;

    [SerializeField]
    GameObject piecePrefab;

    [SerializeField]
    int boardDim;
    GameObject[][] cellObjs;

    public static BoardController instance;
    
    void Awake(){
        if(instance)
            Destroy(this);
        instance = this;
    }
    
    void Start()
    {
        InitCells();
        InitBoard();
    }

    public GameObject[][] GetCellObjs() {
        return this.cellObjs;
    }

    void InitBoard(){
        Color color = GlobalVars.instance.white;
        Color pieceColor = Color.black;
        for (int i = 0; i < this.boardDim; i++)
        {
            if(i>2) pieceColor = Color.white;
            for(int j = 0; j<this.boardDim; j++){

                //Make new cell
                GameObject newCellObj = MakeNewCell(color, i, j);
                this.cellObjs[i][j] = newCellObj;

                //make new piece
                if(i<2 || i>5)
                    MakeNewPiece(newCellObj, pieceColor, GetPieceType(i,j));
                color = (color == GlobalVars.instance.white) ? GlobalVars.instance.black : GlobalVars.instance.white;
        }

        if(i%this.boardDim != this.boardDim-1)
            color = (color == GlobalVars.instance.white) ? GlobalVars.instance.black : GlobalVars.instance.white;
        }
    }

    PieceType GetPieceType(int row, int col){
        if(row == 1 || row == 6)
            return PieceType.Pawn;
        switch(col){
            case 0:
            case 7:
                return PieceType.Rook;
            case 1:
            case 6:
                return PieceType.Knight;
            case 2:
            case 5:
                return PieceType.Bishop;
            case 3:
                return PieceType.Queen;
            case 4:
                return PieceType.King;
        }
        return PieceType.None;
    }

    void InitCells(){
        this.cellObjs = new GameObject[this.boardDim][];
        for(int i=0; i<this.boardDim; i++){
            this.cellObjs[i] = new GameObject[this.boardDim];
        }
    }

    GameObject MakeNewCell(Color _color, int _i, int _j){
        GameObject newCellObj = Instantiate(cellPrefab);
        newCellObj.transform.SetParent(this.gameObject.transform);
        newCellObj.GetComponent<Image>().color = _color;
        newCellObj.transform.localScale = Vector3.one;
        newCellObj.AddComponent<Cell>().InitCell(_color, _i, _j);
        return newCellObj;
    }
    
    void MakeNewPiece(GameObject _cellObj, Color _color, PieceType _type){
        GameObject newPieceGO = Instantiate(piecePrefab);
        newPieceGO.transform.SetParent(_cellObj.transform);
        Piece newPiece=null;
        switch(_type){
            case PieceType.Pawn:
                newPiece = newPieceGO.AddComponent<Pawn>();
                break;
            
            case PieceType.Knight:
                newPiece = newPieceGO.AddComponent<Knight>();
                break;
            
            case PieceType.Bishop:
                newPiece = newPieceGO.AddComponent<Bishop>();
                break;
            
            case PieceType.Rook:
                newPiece = newPieceGO.AddComponent<Rook>();
                break;
            
            case PieceType.Queen:
                newPiece = newPieceGO.AddComponent<Queen>();
                break;
            
            case PieceType.King:
                newPiece = newPieceGO.AddComponent<King>();
                break;
        }
        newPiece.SetColor(_color);

        //Centralize
        newPieceGO.transform.position = Vector3.zero;
        Cell newCell = _cellObj.GetComponent<Cell>();
        newCell.SetCurrPiece(newPiece);
        newPiece.SetCurrCell(newCell);
        RectTransform rect =  newPiece.GetComponent<RectTransform>();
        rect.anchorMin= new Vector2(0.5f, 0.5f);
        rect.anchorMax= new Vector2(0.5f, 0.5f);

        if(_color==Color.white) GameController.instance.whitePlayer.AddPiece(newPiece);
        else GameController.instance.blackPlayer.AddPiece(newPiece);

    }

    //test. delete later

    public void UnhighlightAll(){
        for (int i = 0; i < this.cellObjs.Length; i++) {
            for (int j = 0; j < this.cellObjs.Length; j++) {
                this.cellObjs[i][j].GetComponent<Cell>().Unhighlight();
            }
        }
    }

    // turn to "Legal moves" later
    public void CalculateLegalMoves(GameObject _cellObj){
        //find game object cell in list
        #region find game object cell
        GameObject found = null;
        Vector2Int position = new Vector2Int();
        for (int i = 0; i < this.cellObjs.Length; i++){
            for(int j =0 ; j < this.cellObjs.Length; j++){
                if(this.cellObjs[i][j] == _cellObj){
                    found = this.cellObjs[i][j];
                    position[0] = i;
                    position[1] = j;
                    break;
                }
            }
        }

        if(!found)
            return;
        #endregion

        Cell pieceCell = _cellObj.GetComponent<Cell>();
        List<Vector2Int> moves = _cellObj.GetComponent<Cell>().currentPiece.GetMoves();
        List<Vector2Int> legalMoves = new List<Vector2Int>();

        Direction dir = Direction.None;
        Dictionary<Direction, int> canceledDirections = new Dictionary<Direction, int>();
        foreach(Direction d in Enum.GetValues(typeof(Direction)))
            canceledDirections.Add(d,0);


        //TODO: special moves for pawns
        foreach (Vector2Int move in moves) {
            //Check direction based on movement
            if(move[0] == 0) //horizontal movement
                dir = move[1] > 0 ? Direction.Right : Direction.Left;
            else if(move[1] == 0 ) //vertical movement
                dir = move[0] > 0 ? Direction.Up : Direction.Down;
            else if (move[0] > 0) //Upper right/ left
                dir = move[1] > 0 ? Direction.UpRight : Direction.UpLeft;
            else if(move[0] < 0) // Lower right/ left  
                dir = move[1] > 0 ? Direction.DownRight : Direction.DownLeft;

            //if this direction is blocked
            if(canceledDirections[dir]!=0){
                continue;
            }
            
            int[] finalPosition = {position[0]-move[0], position[1]+move[1]};
            if(GlobalVars.InBoundsInclusive(finalPosition[0], 0, 7) && GlobalVars.InBoundsInclusive(finalPosition[1], 0, 7)){
                GameObject cellGO = this.cellObjs[finalPosition[0]][finalPosition[1]];
                Cell targetCell = cellGO.GetComponent<Cell>();
                //block direction(doesn't apply to Knights)
                if(targetCell.currentPiece != null && !(pieceCell.currentPiece is Knight)){
                    canceledDirections[dir] = 1;
                }
                //only ignore same color pieces
                if(targetCell.currentPiece == null 
                    || (targetCell.currentPiece != null && targetCell.currentPiece.GetColor() != pieceCell.currentPiece.GetColor())){
                    //this.cellObjs[finalPosition[0]][finalPosition[1]].GetComponent<Cell>().Highlight();
                    legalMoves.Add(move);
                }
            }
        }
        pieceCell.currentPiece.SetLegalMoves(legalMoves);
    }

    
}
