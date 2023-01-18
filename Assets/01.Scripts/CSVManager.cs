using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class CSVManager : MonoBehaviour
{
    
    string[] tempData;

    private void Awake()
    {
        
    }
    public void SaveCSVFile(string fileName)
    {   //통장이름,날짜,내용,입금,출금,
        List<Dictionary<string, object>> data;
        string filePath = Application.dataPath + "/" + fileName+".csv";
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            //파일 존재 안함
            File.Create(filePath); //파일 생성
        }
        data = CSVReader.Read(fileName);
        StreamWriter Writer = new StreamWriter(filePath);

        Writer.WriteLine("날짜,통장이름,입금금액,출금금액,내용");
        for(int i =0; i<=data.Count+1; i++)
        {
            //기존의 갯수만큼 작성
            if (i <= data.Count - 1)
            {
                Writer.WriteLine(data[i]["날짜"] + "," +
                    "통장이름" + "," + "입금금액" + "," +
                    "출금금액" + "," + "내용");
            }
            else if(i.Equals(data.Count))
            {
                Writer.WriteLine((i + 1).ToString() + "," );
            }
        }
        Writer.Flush();
        //This closes the file
        Writer.Close();

        data = CSVReader.Read(fileName);
    }

}
