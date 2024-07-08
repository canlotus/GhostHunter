using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static List<string> playerNames = new List<string>();
    private static Dictionary<string, string> assignedCharacters = new Dictionary<string, string>();
    private static Dictionary<string, int> playerVotes = new Dictionary<string, int>();
    private static int blankVoteCount = 0;
    private static string eliminatedPlayer = null;
    private static bool canMove = true;
    private static List<string> eliminatedPlayers = new List<string>();
    private static string protectedPlayer = null;
    private static Dictionary<string, int> protectedSelfCount = new Dictionary<string, int>(); // Added

    public static List<string> PlayerNames
    {
        get { return playerNames; }
        set { playerNames = value; }
    }

    public static Dictionary<string, string> AssignedCharacters
    {
        get { return assignedCharacters; }
        set { assignedCharacters = value; }
    }

    public static Dictionary<string, int> PlayerVotes
    {
        get { return playerVotes; }
        set { playerVotes = value; }
    }

    public static int BlankVoteCount
    {
        get { return blankVoteCount; }
        set { blankVoteCount = value; }
    }

    public static string EliminatedPlayer
    {
        get { return eliminatedPlayer; }
        set { eliminatedPlayer = value; }
    }

    public static bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public static List<string> EliminatedPlayers
    {
        get { return eliminatedPlayers; }
        set { eliminatedPlayers = value; }
    }

    public static string ProtectedPlayer
    {
        get { return protectedPlayer; }
        set { protectedPlayer = value; }
    }

    public static Dictionary<string, int> ProtectedSelfCount
    {
        get { return protectedSelfCount; }
        set { protectedSelfCount = value; }
    }

    public static void SetPlayerVoteCount(string playerName, int count)
    {
        if (playerVotes.ContainsKey(playerName))
        {
            playerVotes[playerName] = count;
        }
        else
        {
            playerVotes.Add(playerName, count);
        }
    }

    public static void SetBlankVoteCount(int count)
    {
        blankVoteCount = count;
    }

    public static void ResetGameData()
    {
        playerNames.Clear();
        assignedCharacters.Clear();
        playerVotes.Clear();
        blankVoteCount = 0;
        eliminatedPlayer = null;
        canMove = true;
        eliminatedPlayers.Clear();
        protectedPlayer = null;
        protectedSelfCount.Clear();
    }

    // Karakter listesini rastgele sýrala
    public static void Shuffle(List<string> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Oyun boyunca hamle yapma yetkisini kontrol et
    public static bool CanPlayerMove()
    {
        return canMove;
    }
}