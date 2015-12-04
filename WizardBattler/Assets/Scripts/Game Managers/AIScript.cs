using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

    public enum AI_Difficulty
    {
        AI_MODE_EASY,
        AI_MODE_MEDIUM,
        AI_MODE_HARD,
        AI_MODE_NETWORKED
    }

    //Keeps track of the difficulty setting
    private AI_Difficulty difficulty;

    //Variables to track time
    private float elapsedTime;
    private float TIME_TO_CAST;

    //Reference to the NPC player
    private Player AIPlayer;

    //Called to set the AIDifficulty
    public void init(AI_Difficulty _difficulty)
    {
        difficulty = _difficulty;

        elapsedTime = 0.0f;
        TIME_TO_CAST = 0.0f;

        AIPlayer = GameObject.FindGameObjectWithTag("player2").GetComponent<Player>();

        switch (difficulty)
        {
            case AI_Difficulty.AI_MODE_EASY:
                TIME_TO_CAST = 3.0f;
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        difficulty = AI_Difficulty.AI_MODE_EASY;
	}
	
	// Update is called once per frame
	void Update () {
	    switch(difficulty)
        {
            case AI_Difficulty.AI_MODE_EASY:
                AIUpdateEasy();
                break;
            default:
                break;
        }
	}

    void AIUpdateEasy()
    {
        //if (!AIPlayer.LockedOut())
        //{
        //    if (elapsedTime >= TIME_TO_CAST)
        //    {
        //        int i = Random.Range(0, AIPlayer.SpellBook.Count);

        //        AIPlayer.activeSpell = AIPlayer.SpellBook[i];
        //        AIPlayer.Mana -= AIPlayer.SpellBook[i].manaCost;

        //        elapsedTime = 0;
        //    }
        //    else
        //    {
        //        elapsedTime += Time.deltaTime;
        //    }
        //}
    }
}
