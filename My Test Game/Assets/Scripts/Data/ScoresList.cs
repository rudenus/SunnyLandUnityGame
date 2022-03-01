using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class ScoresList : MonoBehaviour
{
    private static string fileName = "ListScores.xml";
    public static Dictionary<string, int> listScores = new Dictionary<string, int>();
    public static bool SaveData(string name, int scores)
    {
        if (listScores.ContainsKey(name))
        {
            return false;
        }
        listScores.Add(name, scores);
        var xElement = new XElement("Scores");
        foreach (var score in listScores)
        {
            xElement.Add(new XElement("Result",
            new XAttribute("Name", score.Key),
            new XElement("score", score.Value)));
        }
        var xDocument = new XDocument(xElement);
        xDocument.Save(Application.persistentDataPath + "\\" + fileName);
        return true;
    }
    public static Dictionary<string, int> LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "\\" + fileName))
        {
            listScores.Clear();
            var xDocument = XDocument.Load(Application.persistentDataPath + "\\" + fileName);
            var xElements = xDocument.Root.Elements("Result").ToList();
            foreach (var elem in xElements)
            {
                listScores.Add(elem.Attribute("Name").Value, Convert.ToInt32(elem.Element("score").Value));
            }
        }
        return listScores;
    }
    public static Dictionary<string,int> GetTop10()//получает 10 лучших игроков
    {
        listScores = LoadData();
        var ordered = listScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        Dictionary<string, int> toReturn = new Dictionary<string, int>(); 
        for(int i = 0; i < 10; i++)
        {
            if (ordered.Count == 0) break;
            var first = ordered.First();
            ordered.Remove(first.Key);
            toReturn.Add(first.Key,first.Value);
        }
        return toReturn; 
        
    }
}
