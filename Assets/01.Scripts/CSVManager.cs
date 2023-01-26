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
        //�����̸�,��¥,����,�Ա�,���,
        List<Dictionary<string, object>> data;
        string filePath = Application.dataPath + "/" + fileName+".csv";
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            //���� ���� ����
            File.Create(filePath); //���� ����
        }
        data = Read(fileName);
        StreamWriter Writer = new StreamWriter(filePath);
        Writer.WriteLine("��¥,�����̸�,�Աݱݾ�,��ݱݾ�,����");
        for(int i =0; i<=data.Count+1; i++)
        {
            //������ ������ŭ �ۼ�
            if (i <= data.Count - 1)
            {
                Writer.WriteLine(data[i]["��¥"] + "," +
                    data[i]["�����̸�"] + "," + data[i]["�Աݱݾ�"] + "," +
                    data[i]["��ݱݾ�"] + "," + data[i]["����"]);
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
