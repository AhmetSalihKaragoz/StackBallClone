using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public ColorLists colorLists;
    public static List<Color32> CurrentColorList;
    private List<List<Color32>> m_permColorList = new List<List<Color32>>();
    private List<List<Color32>> m_tempColorList = new List<List<Color32>>();

    

    private void Awake()
    {
        AddLists();
        SetCurrentColor();
        
    }

    private void SetCurrentColor()
    {
        if (m_tempColorList.Count == 0)
        {
            FillTempList();
        }
        if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 1)
        {
            CurrentColorList = m_tempColorList[0];
        }
        else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 2)
        {
            CurrentColorList = m_tempColorList[1];
        }
        else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 3)
        {
            CurrentColorList = m_tempColorList[2];
        }
        else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 4)
        {
            CurrentColorList = m_tempColorList[3];
        }
        else if (PlayerPrefs.GetInt("level", LevelManager.Instance.level) == 5)
        {
            CurrentColorList = m_tempColorList[4];
        }
        else
        {
            SetRandomColor();
        }
        
    }

    private void FillTempList()
    {
        foreach (var colorList in m_permColorList) 
        { 
            m_tempColorList.Add(colorList); 
        }
    }

    private void SetRandomColor()
    {
        int index = Random.Range(0, 5);
        CurrentColorList = m_tempColorList[index];
        m_tempColorList.Remove(m_tempColorList[index]);
    }
    
    private void AddLists()
    {
        m_permColorList.Add(colorLists.colorList1);
        m_permColorList.Add(colorLists.colorList2);
        m_permColorList.Add(colorLists.colorList3);
        m_permColorList.Add(colorLists.colorList4);
        m_permColorList.Add(colorLists.colorList5);
    }
}
