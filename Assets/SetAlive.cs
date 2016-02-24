using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetAlive : MonoBehaviour {

    public bool alive;
    public bool lastAlive;
    public Material deadMat;
    public Material aliveMat;

    PlayLife playScript;
    int neighbors;
    Renderer rend;
    List<Transform> checkList;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        playScript = GameObject.Find("PlayCell").GetComponent<PlayLife>();
        checkList = new List<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        setMat();
	}

    void OnMouseDown()
    {
        if (playScript.play)
            return;

        checkList.Add(transform);
        if (alive)
        {
            alive = false;
        }

        else
        {
            alive = true;
        }
    }

    void OnMouseDrag()
    {
        if (!checkList.Contains(transform))
        OnMouseDown();
    }

    void OnMouseUp()
    {
        checkList.Clear();

    }

    public bool setAlive()
    {
        if (alive == true)
        {
            if (neighbors < 2 || neighbors > 3)
            {
                alive = false;
            }
            else
            {
                return true;
            }
        }

        else if (alive == false && neighbors == 3)
        {
            alive = true;
        }

        return alive;
    }

    public void forceAlive(bool force)
    {
        alive = force;
        
    }

    public void setMat()
    {
        if (lastAlive != alive)
        {
            if (alive)
            {
                rend.material = aliveMat;
            }

            else
            {
                rend.material = deadMat;
            }
        }
        lastAlive = alive;

    }

    public void setNeighbors(int count)
    {
        neighbors = count;
    }
}
