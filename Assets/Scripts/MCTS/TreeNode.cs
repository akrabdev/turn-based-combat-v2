//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Game
//{
//    public int CurrentPlayer;

//    public List<int> GetLegalMoves()
//    {
//        return new List<int>();
//    }
//    public Game DeepCopy()
//    {
//        return new Game();
//    }
//    /// <summary>
//    /// Returns whether game is over
//    /// </summary>
//    public bool IsGameOver()
//    {
//        return true;
//    }

//    public bool IsWinner(int player)
//    {
//        return true;
//    }

//    public int RandomItem(List<int> list, System.Random random)
//    {
//        return list[random.Next(0, list.Count)];
//    }

//    public void DoMove (int move)
//    {

//    }

//}
//public struct MoveStats
//{
//    internal int executions;
//    internal int victories;

//    internal double Score() => (double)victories / executions;
//}
//public static int Search(Game game, int simulations)
//{
//    int aiPlayer = game.CurrentPlayer;
//    List<int> legalMoves = game.GetLegalMoves();
//    int count = legalMoves.Count;
//    MoveStats[] moveStats = new MoveStats[count];
//    int moveIndex;
//    Game copy;
//    System.Random rng = new System.Random();

//    for (int i = 0; i < simulations; i++)
//    {
//        moveIndex = rng.Next(0, count);
//        copy = game.DeepCopy();
//        copy.DoMove(legalMoves[moveIndex]);

//        while (!copy.IsGameOver())
//            copy.DoMove(
//                copy.RandomItem(copy.GetLegalMoves(), rng));

//        moveStats[moveIndex].executions++;
//        if (copy.IsWinner(aiPlayer))
//            moveStats[moveIndex].victories++;
//    }

//    int bestMoveFound = 0;
//    double bestScoreFound = 0f;
//    for (int i = 0; i < count; i++)
//    {
//        double score = moveStats[i].Score();
//        if (score > bestScoreFound)
//        {
//            bestScoreFound = score;
//            bestMoveFound = i;
//        }
//    }

//    return legalMoves[bestMoveFound];
//}


