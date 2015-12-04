using UnityEngine;
using System.Collections;

public class GameWorldAmbienceUpdate : MonoBehaviour {

    private GameObject sun;
	// Use this for initialization
	void Start () {
        sun = GameObject.FindGameObjectWithTag("sun");
	}
	
	// Update is called once per frame
	void Update () {
        sun.transform.Rotate(Vector3.right, 0.01f, Space.Self);
	}
}
