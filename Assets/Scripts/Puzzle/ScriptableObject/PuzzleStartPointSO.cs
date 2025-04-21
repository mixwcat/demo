
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleStartPointSO", menuName = "Puzzle/PuzzleStartPointSO")]
public class PuzzleStartPointSO : ScriptableObject
{
    public List<PuzzleStartPoint> puzzleStartPoints;
}

[System.Serializable]
public struct PuzzleStartPoint
{
    public int playerIndex;
    public Vector2 position;
    public PoleType poleType;
}