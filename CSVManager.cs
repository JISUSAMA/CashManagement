using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.IO;

public class CSVManager : MonoBehaviour
{
    //CSV Reader
    static string SPLIT_RE = @",";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    string[] tempData;
    public static CSVManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }
    public static void SaveCSVFile(string fileName)
    {   
        //통장이름,날짜,내용,입금,출금,
        List<Dictionary<string, object>> data;
        string filePath = Application.dataPath + "/" + fileName+".csv";
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            //파일 존재 안함
            File.Create(filePath); //파일 생성
        }
        data = Read(fileName);
        StreamWriter Writer = new StreamWriter(filePath);
        Writer.WriteLine("날짜,통장이름,입금금액,출금금액,내용");
        for(int i =0; i<=data.Count+1; i++)
        {
            //기존의 갯수만큼 작성
            if (i <= data.Count - 1)
            {
                Writer.WriteLine(data[i]["날짜"] + "," +
                    data[i]["통장이름"] + "," + data[i]["입금금액"] + "," +
                    data[i]["출금금액"] + "," + data[i]["내용"]);
            }
            else if(i.Equals(data.Count))
            {
                Writer.WriteLine((i + 1).ToString() + "," );
            }
        }
        Writer.Flush();
        //This closes the file
        Writer.Close();

        data =Read(fileName);
    }

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                string finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n.ToString();
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f.ToString();
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
