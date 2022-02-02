using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatManager : MonoBehaviour
{
    public static MiningGameModes currentGameMode;
    public static int maxNumberOfScans;
    public static int maxNumberOfExtractions;
    public static int scansRemaining;
    public static int extractionsRemaining;
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        maxNumberOfExtractions = 3;
        maxNumberOfScans = 6;
        scansRemaining = maxNumberOfScans;
        extractionsRemaining = maxNumberOfExtractions;
        currentGameMode = MiningGameModes.EXTRACT_MODE;
        score = 0;
    }

    public static void ResetAllGameStats()
    {
        scansRemaining = maxNumberOfScans;
        extractionsRemaining = maxNumberOfExtractions;
        currentGameMode = MiningGameModes.EXTRACT_MODE;
        score = 0;
    }
}
