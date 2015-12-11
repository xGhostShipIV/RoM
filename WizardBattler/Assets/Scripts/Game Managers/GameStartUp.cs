using UnityEngine;
using System.Collections;

public class GameStartUp : MonoBehaviour
{

    public Player player1;
    public Player player2;

    bool setup = false;

    public GameObject playerPrefab;

    //public AIScript ais;

    // Use this for initialization
    void Start()
    {

        //GameObject g = GameObject.Instantiate(playerPrefab);
        //g.transform.position = GameObject.FindWithTag("spawn1").transform.position;

        //player1 = g.GetComponent<Player>();
        //player1.tag = "player1";

        //GameObject g2 = GameObject.Instantiate(playerPrefab);
        //g2.transform.position = GameObject.FindWithTag("spawn2").transform.position;
        //g2.transform.Rotate(new Vector3(0, 1, 0), 180, Space.Self);

        //player2 = g2.GetComponent<Player>();
        //player2.tag = "player2";

        //player1.otherPlayer = player2;
        //player2.otherPlayer = player1;

        //player1.init(0, 100);
        //player2.init(1, 100);

        //ais = gameObject.GetComponent<AIScript>();
        //ais.init(AIScript.AI_Difficulty.AI_MODE_EASY);



        //MouseDraw md = GetComponent<MouseDraw>();
        //md.init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!setup)
            if (GameObject.FindGameObjectWithTag("player1") != null &&
                GameObject.FindGameObjectWithTag("player2") != null)
            {
                player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player>();
                player2 = GameObject.FindGameObjectWithTag("player2").GetComponent<Player>();

                player1.otherPlayer = player2;
                player2.otherPlayer = player1;

                //player1.pnb.Init();
                //player2.pnb.Init();

                player1.init(0, 100);
                player2.init(1, 100);

                MouseDraw md = GetComponent<MouseDraw>();
                md.init();

                setup = true;
            }
    }
}
