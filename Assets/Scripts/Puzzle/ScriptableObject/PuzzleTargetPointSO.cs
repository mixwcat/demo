using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleTargetPointSO", menuName = "Puzzle/PuzzleTargetPointSO")]
public class PuzzleTargetPointSO : ScriptableObject
{
    public List<PuzzleTargetPoint> puzzleTargetPoints;
}

[System.Serializable]
public struct PuzzleTargetPoint
{
    public int playerIndex;
    public Vector2 position;
    
}