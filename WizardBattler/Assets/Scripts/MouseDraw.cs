using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseDraw : MonoBehaviour
{
    //Constant floats to represent how much time must pass before each
    //Timer is set off.
    private const float DRAW_ITERATION = .005f;
    private const float LINE_DELETE_TIME = .500f;

    //Holds all the points for various lines
    private List<Vector3> points;

    private float lineDrawTimer;
    private float lineDeleteTimer;

    //Vector3s to hold positions for the mouse stuff
    private Vector3 startMousePos;
    private Vector3 MousePosition;

    //A reference to the lineRenderer component. Used to draw our lines
    public LineRenderer lr;

    private Player player;

    // Use this for initialization
    void Start()
    {

    }

    public void init()
    {
        //Gets the line renderer already attached to the object
        lr = GetComponent<LineRenderer>();

        //Setswidth and color for the lines drawn
        lr.SetWidth(.1f, .1f);
        lr.SetColors(Color.red, Color.red);

        //Initializes the list of 3d points
        points = new List<Vector3>();

        //Initialize timers to 0
        lineDeleteTimer = 0;
        lineDrawTimer = 0;

        player = GameObject.FindGameObjectWithTag("player1").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.LockedOut())
        {
            if (Input.GetMouseButton(0))
            {
                //If the timer to draw a new line segment gives the go ahead
                if (lineDrawTimer <= 0)
                {
                    //Creates two new points and adds them too the list of points
                    //One point is blank (0,0,0) so that later in the function call its
                    //set too the mouse position, providing two different positions
                    points.Add(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
                    points.Add(new Vector3(0, 0, 0));

                    //Sets the amount of vertices in the line renderer to the size of our list of points
                    lr.SetVertexCount(points.Count);

                    //Sets the timer to the amount of time to pass needed
                    lineDrawTimer = DRAW_ITERATION;
                }
                else
                {
                    //Subtracts elapsed time from the timer
                    lineDrawTimer -= Time.deltaTime;
                }

                //Resets the line deletion timer
                lineDeleteTimer = LINE_DELETE_TIME;

                //Sets our blank point to the current mouse position
                if (points.Count > 0)
                    points[points.Count - 1] = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);

                DrawPixels();
            }
        }

        //When mouse button isnt clicked, start subtracting time
        //to line deletion
        if (lineDeleteTimer <= 0)
        {
            if (points.Count > 0)
            {
                Vector3[] _points = new Vector3[points.Count];
                points.CopyTo(_points);

                player.checkDraw(_points);

                //clear the list of points
                points.Clear();

                //Reset the line renderer vertex count to 0
                lr.SetVertexCount(0);

                //Reset the timer that draws lines
                lineDrawTimer = 0;
            }
        }
        else
        {
            lineDeleteTimer -= Time.deltaTime;
        }
    }

    void DrawPixels()
    {
        //Sets the positions of vertices of the LAST TWO in the renderer to the last two points
        //in the list of points and draws a line between them.
        lr.SetPosition(points.Count - 2, Camera.main.ScreenToWorldPoint(points[points.Count - 2]));
        lr.SetPosition(points.Count - 1, Camera.main.ScreenToWorldPoint(points[points.Count - 1]));
    }
}
