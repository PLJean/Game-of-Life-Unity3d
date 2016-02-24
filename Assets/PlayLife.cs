using UnityEngine;
using System.Collections;


public class PlayLife : MonoBehaviour {

    public bool play = false;
    public GameObject cellPrefab;
    int population = 0, generation = 0;
    // Length is 66 to account for outside rows. (64 + 2)
    static int length = 66;
    static int area = length * length;

    GameObject[] cells = new GameObject[area];
    SetAlive[] cellScripts = new SetAlive[area];

    // Use this for initialization
    void Start () {
        CreateCells();
    }
	
    public void PlayPressed()
    {
        print("play pressed.");
        play = true;
        StartCoroutine(Play());
    }

    public void QuitPressed()
    {
        play = false;
    }

    IEnumerator Play()
    {
        print("Playing.");
        while (play) {
            //while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
            yield return null;

            for (int r = 0; r  < 64; ++r)
            {
                for (int c = 0; c < 64; ++c)
                {
                    //print("r: " + r + " c: " + c);
                    cellScripts[at(r, c)].setNeighbors(getNeighbors(r, c));
                }
            }

            population = 0;
            for (int r = 0; r < 64; ++r)
            {
                for (int c = 0; c < 64; ++c)
                {
                    if (cellScripts[at(r, c)].setAlive()) {
                        ++population;
                    }
                }
            }

            ++generation;
        }

        for (int r = 0; r < 64; ++r)
        {
            for (int c = 0; c < 64; ++c)
            {
                cellScripts[at(r, c)].forceAlive(false);
            }
        }
    }

    void CreateCells()
    {
        for (int i = 0; i < 66; ++i)
        {
            // top row
            cells[i] = null;
            cellScripts[i] = null;
            // bottom row
            cells[65 * 66 + i] = null;
            cellScripts[65 * 66 + i] = null;
            // left row
            cells[i * 66] = null;
            cellScripts[i * 66] = null;
            //right row
            cells[(1 + i) * 66 - 1] = null;
            cellScripts[(1 + i) * 66 - 1] = null;
        }

        // Get the location of the a definied, first cell.
        Vector3 startPos = transform.position;

        for (int r = 0; r < length - 2 ; ++r)
        {
            for (int c = 0; c < length - 2; ++c)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(startPos.x + c * 1.5f, startPos.y + r * 1.5f, startPos.z), Quaternion.identity) as GameObject;
                cells[at(r, c)] = cell;
                cellScripts[at(r, c)] = cell.GetComponent<SetAlive>();
            }
        }
    }

    int at(int r, int c)
    {
        return (length + 1) + r * length + c;
    }

    int getNeighbors(int r, int c)
    {
        int neighbors = 0;
        if (cellScripts[at(r - 1, c -   1)] != null && cellScripts[at(r - 1, c - 1)].alive == true)
            ++neighbors;

        if (cellScripts[at(r - 1, c)] != null && cellScripts[at(r - 1, c)].alive == true)
            ++neighbors;

        if (cellScripts[at(r - 1, c + 1)] != null && cellScripts[at(r - 1, c + 1)].alive == true)
            ++neighbors;

        if (cellScripts[at(r, c + 1)] != null && cellScripts[at(r, c + 1)].alive == true)
            ++neighbors;

        if (cellScripts[at(r + 1, c + 1)] != null && cellScripts[at(r + 1, c + 1)].alive == true)
            ++neighbors;

        if (cellScripts[at(r + 1, c)] != null && cellScripts[at(r + 1, c)].alive == true)
            ++neighbors;

        if (cellScripts[at(r + 1, c - 1)] != null && cellScripts[at(r + 1, c - 1)].alive == true)
            ++neighbors;

        if (cellScripts[at(r, c - 1)] != null && cellScripts[at(r, c - 1)].alive == true)
            ++neighbors;

        if (r == 0 || r == 1)
            print("(" + r + ", " + c + "): " + neighbors);

        return neighbors;
    }
}
