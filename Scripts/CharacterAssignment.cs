using UnityEngine;
using System.Collections.Generic;

public static class CharacterAssignment
{
    public static void AssignCharacters(int ghostHuntersCount, int ghostsCount, int protectorsCount, int detectorsCount)
    {
        List<string> playerNames = GameData.PlayerNames;

        List<string> characters = new List<string>();

        // Ghost Hunters ekle
        for (int i = 0; i < ghostHuntersCount; i++)
        {
            characters.Add("Ghost Hunter");
        }

        // Ghosts ekle
        for (int i = 0; i < ghostsCount; i++)
        {
            characters.Add("Ghost");
        }

        // Protectors ekle
        for (int i = 0; i < protectorsCount; i++)
        {
            characters.Add("Protector");
        }

        // Detectors ekle
        for (int i = 0; i < detectorsCount; i++)
        {
            characters.Add("Detector");
        }

        // Karakterleri rastgele sýrala
        Shuffle(characters);

        // Oyuncu isimleriyle eþleþtirerek atama yap
        for (int i = 0; i < playerNames.Count; i++)
        {
            if (i < characters.Count)
            {
                GameData.AssignedCharacters[playerNames[i]] = characters[i];
            }
            else
            {
                GameData.AssignedCharacters[playerNames[i]] = "Default Character";
            }
        }
    }

    // Karakter listesini rastgele sýralamak için kullanýlacak fonksiyon
    private static void Shuffle(List<string> list)
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
}
