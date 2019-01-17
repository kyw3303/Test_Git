using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Setting : MonoBehaviour {

    public GameObject m_goItem;
    public int m_iZ, m_iX;
    public List<GameObject> m_List_Puzzle = new List<GameObject>();
    //private List<Vector3> m_List_Pos = new List<Vector3>();
    private List<Vector3> m_List_Org_Pos = new List<Vector3>();
    Vector3 m_Pos_Empty;
    int m_iIndex_empty;

    bool m_bClear = false;
    private void Awake()
    {
        if (m_iZ <= 1)
        {
            m_iZ = 3;
        }

        if (m_iX <= 1)
        {
            m_iX = 3;
        }

        m_goItem.SetActive(false);
        //m_List_Pos.Clear();
        int iCount = 0;
        for (int z = 0; z < m_iZ; z++)
        {
            for (int x = 0; x < m_iX; x++)
            {
                Vector3 vec = new Vector3(x * 1.1f, 0.0f, z * 1.1f);
                //m_List_Pos.Add(vec);
                m_List_Org_Pos.Add(vec);
                GameObject go = Instantiate(m_goItem);
                go.transform.parent = m_goItem.transform.parent;
                go.SetActive(true);

                TextMesh text = go.GetComponentInChildren<TextMesh>();
                if (text != null)
                    text.text = string.Format("{0}", iCount + 1);

                m_List_Puzzle.Add(go);

                iCount++;
            }
        }
    }
    // Use this for initialization
    void Start () {
        //m_List_Play.Clear();
        //for (int i = 0; i < 9; i++)
        //{
        //    m_List_Play.Add(i + 1);
        //}
        //int iCount = m_List_Pos.Count;
        //for (int i = 0; i < iCount; i++)
        //{
        //    int dest = Random.Range(0, iCount);
        //
        //    Vector3 temp = m_List_Pos[i];
        //    m_List_Pos[i] = m_List_Pos[dest];
        //    m_List_Pos[dest] = temp;
        //}
        List<Vector3> list_RandPos = new List<Vector3>(m_List_Org_Pos);
        int iCount = m_List_Puzzle.Count;
        for (int i = 0; i < iCount; i++)
        {
            int dest = Random.Range(0, iCount);

            Vector3 temp = list_RandPos[i];
            list_RandPos[i] = list_RandPos[dest];
            list_RandPos[dest] = temp;
        }

        for (int i = 0; i < iCount; i++)
        {
            m_List_Puzzle[i].transform.position = list_RandPos[i];
        }

        int iRand = Random.Range(0, iCount);
        m_List_Puzzle[iRand].gameObject.SetActive(false);
        m_Pos_Empty = m_List_Puzzle[iRand].transform.position;

        for (int i = 0; i < iCount; i++)
        {
            if (m_List_Org_Pos[i].Equals(m_Pos_Empty) == true)
            {
                m_iIndex_empty = i;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_bClear == false)
        {
            bool bCheck = false;
            if (Input.GetKeyUp(KeyCode.LeftArrow) == true)
            {
                if (m_iIndex_empty % m_iX < (m_iX - 1))
                {
                    Vector3 TargetPos = m_List_Org_Pos[m_iIndex_empty + 1];
                    for (int i = 0; i < m_List_Puzzle.Count; i++)
                    {
                        if (m_List_Puzzle[i].activeSelf == true && m_List_Puzzle[i].transform.position == TargetPos)
                        {
                            m_List_Puzzle[i].transform.position = m_List_Org_Pos[m_iIndex_empty];
                            break;
                        }
                    }

                    m_iIndex_empty += 1;
                    bCheck = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) == true)
            {
                if (m_iIndex_empty % m_iX > 0)
                {
                    Vector3 TargetPos = m_List_Org_Pos[m_iIndex_empty - 1];
                    for (int i = 0; i < m_List_Puzzle.Count; i++)
                    {
                        if (m_List_Puzzle[i].activeSelf == true && m_List_Puzzle[i].transform.position == TargetPos)
                        {
                            m_List_Puzzle[i].transform.position = m_List_Org_Pos[m_iIndex_empty];
                            break;
                        }
                    }

                    m_iIndex_empty -= 1;
                    bCheck = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) == true)
            {
                if (m_iIndex_empty / m_iX > 0)
                {
                    Vector3 TargetPos = m_List_Org_Pos[m_iIndex_empty - m_iX];
                    for (int i = 0; i < m_List_Puzzle.Count; i++)
                    {
                        if (m_List_Puzzle[i].activeSelf == true && m_List_Puzzle[i].transform.position == TargetPos)
                        {
                            m_List_Puzzle[i].transform.position = m_List_Org_Pos[m_iIndex_empty];
                            break;
                        }
                    }

                    m_iIndex_empty -= m_iX;
                    bCheck = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) == true)
            {
                if (m_iIndex_empty / m_iX < (m_iZ - 1))
                {
                    Vector3 TargetPos = m_List_Org_Pos[m_iIndex_empty + m_iX];
                    for (int i = 0; i < m_List_Puzzle.Count; i++)
                    {
                        if (m_List_Puzzle[i].activeSelf == true && m_List_Puzzle[i].transform.position == TargetPos)
                        {
                            m_List_Puzzle[i].transform.position = m_List_Org_Pos[m_iIndex_empty];
                            break;
                        }
                    }

                    m_iIndex_empty += m_iX;
                    bCheck = true;
                }
            }


            if (bCheck == true)
            {
                bool bClear = true;
                for (int i = 0; i < m_List_Org_Pos.Count; i++)
                {
                    if (m_List_Puzzle[i].activeSelf == true && m_List_Puzzle[i].transform.position != m_List_Org_Pos[i])
                    {
                        bClear = false;
                        break;
                    }
                }

                if (bClear == true)
                {
                    m_bClear = true;
                    m_List_Puzzle[m_iIndex_empty].transform.position = m_List_Org_Pos[m_iIndex_empty];
                    m_List_Puzzle[m_iIndex_empty].SetActive(true);
                }
                else
                    m_bClear = false;
            }
        }
        
    }
}
