using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GlobalVars : MonoBehaviour
{
    public Dictionary<int, string> posToColumn = new Dictionary<int, string>() {
        {0, "A"},
        {1, "B"},
        {2, "C"},
        {3, "D"},
        {4, "E"},
        {5, "F"},
        {6, "G"},
        {7, "H"}
    };
    public Dictionary<string, int> colToPos = new Dictionary<string, int>(){
        {"A", 0},
        {"B", 1},
        {"C", 2},
        {"D", 3},
        {"E", 4},
        {"F", 5},
        {"G", 6},
        {"H", 7},
    };
    [Header("Piece Images")]
    public List<Sprite> pieceImages;
    [Header("Cell Colors")]
    public Color white;
    public Color black;

    public static GlobalVars instance;

    public static List<Sprite> pieceImagesStat;
    void Awake(){
        if(instance)
            Destroy(this);
        instance = this;
        pieceImagesStat = new List<Sprite>(this.pieceImages);
    }

    public static bool InBoundsInclusive(int _value, int _low, int _high){
        return (_value >= _low && _value <= _high);
    }

}
