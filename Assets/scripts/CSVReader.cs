using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CSVReader : MonoBehaviour {

    int StartMoney = 0;
    StreamReader myReader;
    string readMoney;

	void Start ()
    {

        string fullPath = Application.dataPath;
        string pathAddition = "/scripts/CSV/test.csv";
        print(fullPath+pathAddition);

        //String[] values = File.ReadAllText(@"d:\test.csv").Split(',');
        myReader = new StreamReader(fullPath+pathAddition);
        // List<string> listA = new List<string>();
        // List<string> listB = new List<string>();
        // while (!myReader.EndOfStream)
        // {
        //     var line = myReader.ReadLine();
        //     var values = line.Split(';');

        //     listA.Add(values[0]);
        //     listB.Add(values[1]);
        // }
        while (!myReader.EndOfStream)
        {
            var line = myReader.ReadLine();
            string[] values = line.Split(';');
            readMoney = values[0];
            print(readMoney);
        }
        myReader.Close();
        StartMoney = int.Parse(readMoney);
        print(StartMoney * 2);

    }


    void Update () {
	
	}
}
