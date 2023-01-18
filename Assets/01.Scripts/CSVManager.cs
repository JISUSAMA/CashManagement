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
    {   //�����̸�,��¥,����,�Ա�,���,
        List<Dictionary<string, object>> data;
        string filePath = Application.dataPath + "/" + fileName+".csv";
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            //���� ���� ����
            File.Create(filePath); //���� ����
        }
        data = CSVReader.Read(fileName);
        StreamWriter Writer = new StreamWriter(filePath);

        Writer.WriteLine("��¥,�����̸�,�Աݱݾ�,��ݱݾ�,����");
        for(int i =0; i<=data.Count+1; i++)
        {
            //������ ������ŭ �ۼ�
            if (i <= data.Count - 1)
            {
                Writer.WriteLine(data[i]["��¥"] + "," +
                    "�����̸�" + "," + "�Աݱݾ�" + "," +
                    "��ݱݾ�" + "," + "����");
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
