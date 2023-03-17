
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class RecordList_UIManager : MonoBehaviour
{
    [System.Serializable]
    public struct RecordList_Home_Grup
    {
        public ToggleGroup RecordList_toggleGrup;
        public Toggle[] RecordList_toggle;
        public GameObject[] RecordCanvasList_ob;
        [Space(5)]
        [Header("Main_Toggle_Grup")]
        public ToggleGroup Latest_toggleGrup;
        public Toggle[] Latest_toggle;
        public GameObject[] LatestCanvasList_ob;

    }
    [System.Serializable]
    public struct Create_PassBook_Data
    {
        public GameObject create_popup_ob;
        public InputField Passbook_name;
        public InputField Passbook_memo;
        public Button creat_btn;

    }
    [System.Serializable]
    public struct Add_Deposit_Withdrawal_Data
    {
        public GameObject Deposit_Withdrawal_Canvas;
        [Space(5)]
        public ToggleGroup D_W_ToggleGrup;
        public Toggle[] D_W_Toggle;
        public GameObject[] D_W_CanvasList_ob;
        [Space(5)]
        [Header("�Ա� ���� �ۼ�")]
        public ToggleGroup Deposit_method_toggleG;
        public Toggle[] Deposit_method_toggles;
        public GameObject[] Deposit_method_CanvasList_ob;
        
        [Header("��� ���� �ۼ�")]
        public ToggleGroup Withdrawal_method_toggleG;
        public Toggle[] Withdrawal_method_toggles;
        public GameObject[] Withdrawal_method_CanavasList_ob;

        [Header("DW_Method_Input_list")]
        public DW_Method_Input_list Deposit_me;
        public DW_Method_Input_list Deposit_other;
        public DW_Method_Input_list Withdrawal_me;
        public DW_Method_Input_list Withdrawal_other;

        [Header("�� ���� �Ա�")]
        public Dropdown MyPassBook_drp;
        public Dropdown OtherPassBook_drp;
        public int money_i;
        public Dropdown Category;
    }
  
    [Space(5)]
    [Header("RecordList_Home_Grup")]
    public RecordList_Home_Grup RecordList_Main;
    [Space(5)]
    [Header("Create_Passbook_Data")]
    public Create_PassBook_Data Create_passbook;
    [Space(5)]
    [Header("��/��� ��� �߰� �ۼ��ϱ�")]
    public Add_Deposit_Withdrawal_Data Add_DW_data;
    public Toggle RecordList_Seletion
    {
        get { return RecordList_Main.RecordList_toggleGrup.ActiveToggles().FirstOrDefault(); }
    }

    public void SetUpToggleChoice()
    {
        if (RecordList_Main.RecordList_toggleGrup.ActiveToggles().Any())
        {
            if (RecordList_Seletion.name.Equals("PassbookBtn_Grup1"))
            {
                PanelSelection_Active(true, false, false, false);
            }
            else if (RecordList_Seletion.name.Equals("PassbookBtn_Grup2"))
            {
                PanelSelection_Active(false, true, false, false);
            }
            else if (RecordList_Seletion.name.Equals("PassbookBtn_Grup3"))
            {
                PanelSelection_Active(true, false, true, false);
            }
            else if (RecordList_Seletion.name.Equals("PassbookBtn_Grup4"))
            {
                PanelSelection_Active(false, false, false, true);
            }
        }
    }
    void PanelSelection_Active(bool grup1, bool grup2, bool grup3, bool grup4)
    {
        if (grup1 == true) { RecordList_Main.RecordCanvasList_ob[0].gameObject.SetActive(true); }
        else { RecordList_Main.RecordCanvasList_ob[0].gameObject.SetActive(false); }
        if (grup2 == true) { RecordList_Main.RecordCanvasList_ob[1].gameObject.SetActive(true); }
        else { RecordList_Main.RecordCanvasList_ob[1].gameObject.SetActive(false); }
        if (grup3 == true) { RecordList_Main.RecordCanvasList_ob[2].gameObject.SetActive(true); }
        else { RecordList_Main.RecordCanvasList_ob[2].gameObject.SetActive(false); }
        if (grup4 == true) { RecordList_Main.RecordCanvasList_ob[3].gameObject.SetActive(true); }
        else { RecordList_Main.RecordCanvasList_ob[3].gameObject.SetActive(false); }
    }
    public Toggle Latest_Selection
    {
        get { return RecordList_Main.Latest_toggleGrup.ActiveToggles().FirstOrDefault(); }
    }
    public void Latest_Toggle()
    {
        if (RecordList_Main.Latest_toggleGrup.ActiveToggles().Any())
        {
            if (Latest_Selection.name.Equals("Latest_deposit"))
            {
                Latest_Selection_Active(true, false);
            }
            else if (Latest_Selection.name.Equals("Latest_withdrawal"))
            {
                Latest_Selection_Active(false, true);
            }

        }

    }
    void Latest_Selection_Active(bool grup1, bool grup2)
    {
        if (grup1 == true) { RecordList_Main.LatestCanvasList_ob[0].SetActive(true); }
        else { RecordList_Main.LatestCanvasList_ob[0].SetActive(false); }
        if (grup2 == true) { RecordList_Main.LatestCanvasList_ob[1].SetActive(true); }
        else { RecordList_Main.LatestCanvasList_ob[1].SetActive(false); }
    }

    ///////////////////////////  �������   ///////////////////////////////////////////////////
    public void OnClick_Create_Passbook()
    {
        Create_passbook.create_popup_ob.SetActive(true);
    
    }
    public void OnClick_Yes_Create_PassbookBtn()
    {
        if (!Create_passbook.Passbook_name.text.Length.Equals(0)
            && !Create_passbook.Passbook_memo.text.Length.Equals(0))
        {
            Create_Passbook_list("PASSBOOK_LIST");
        }
    }
    public void Create_Passbook_list(string fileName)
    {
        StartCoroutine(_Create_PassBook_list(fileName));
    }
    IEnumerator _Create_PassBook_list(string fileName)
    {
        //�����̸�
        string filePath = Application.dataPath + "/Resources/" + fileName + ".csv";
        FileInfo fileInfo = new FileInfo(filePath);
        Debug.Log(fileInfo);
        if (!fileInfo.Exists)
        {
            //���� ���� ����
            File.Create(filePath); //���� ����
            Debug.Log("-------1");
        }
        DataManager.instance.PassBook_List_data = CSVManager.Read(fileName);
        StreamWriter Writer = new StreamWriter(filePath);
        //���� ������ ������ ���ο� ���������� �������ش�
        //�ϳ��� �ִ� ���, ������ ���� ������ ���������ְ� ���ο� ���� ������ ��������

        if (!DataManager.instance.PassBook_List_data.Count.Equals(0))
        {
            Writer.WriteLine("�ѹ�,�����̸�,�޸�,����");
            Debug.Log("data.Count: " + DataManager.instance.PassBook_List_data.Count);
            for (int i = 0; i <= DataManager.instance.PassBook_List_data.Count + 1; i++)
            {
                //������ ������ŭ �ۼ�
                if (i <= DataManager.instance.PassBook_List_data.Count - 1)
                {
                    Writer.WriteLine(DataManager.instance.PassBook_List_data[i]["�ѹ�"] + "," + DataManager.instance.PassBook_List_data[i]["�����̸�"] + "," +
                           DataManager.instance.PassBook_List_data[i]["�޸�"] + "," + DataManager.instance.PassBook_List_data[i]["����"]);
                }
                else if (i.Equals(DataManager.instance.PassBook_List_data.Count))
                {
                    Writer.WriteLine(DataManager.instance.PassBook_List_data.Count + "," +
             Create_passbook.Passbook_name.text + "," +
             Create_passbook.Passbook_memo.text + "," +
             Create_passbook.Passbook_name.text);
                }
            }
        }
        else
        {
            Writer.WriteLine("�ѹ�,�����̸�,�޸�,����");
            Writer.WriteLine(DataManager.instance.PassBook_List_data.Count + "," + Create_passbook.Passbook_name.text + "," +
            Create_passbook.Passbook_memo.text + "," +
            Create_passbook.Passbook_name.text);
        }

        Writer.Flush();
        //This closes the file
        Writer.Close();

        DataManager.instance.PassBook_List_data = CSVManager.Read(fileName);
        //�����Ϸ�
        Create_passbook.create_popup_ob.SetActive(false);
        yield return null;

    }
   
    /////////////////////////////  ����� ����ϱ� //////////////////////////////////////////////
    public void OnClick_D_W_CreateBtn()
    {
        Add_DW_data.Deposit_Withdrawal_Canvas.SetActive(true);
        PassBookList_DropDown(); //���� ����Ʈ ��ӹڽ� �ؽ�Ʈ �Է�
    }
    //�Ա�/��� ���� ���
    public Toggle Add_DW_Data_selection
    {
        get { return Add_DW_data.D_W_ToggleGrup.ActiveToggles().FirstOrDefault(); }
    }
    public void Add_DW_Data_Toggle()
    {
        if (Add_DW_data.D_W_ToggleGrup.ActiveToggles().Any())
        {
            if (Add_DW_Data_selection.name.Equals("Deposit_Toggle"))
            {
                Add_DW_Selection_Active(true, false);
            }
            else if (Add_DW_Data_selection.name.Equals("Withdrwal_Toggle"))
            {
                Add_DW_Selection_Active(false, true);
            }

        }
    }
    void Add_DW_Selection_Active(bool deposit, bool withdrawal)
    {
        if (deposit == true)
        { Add_DW_data.D_W_CanvasList_ob[0].SetActive(true); }
        else { Add_DW_data.D_W_CanvasList_ob[0].SetActive(false); }
        if (withdrawal == true)
        {
            Add_DW_data.D_W_CanvasList_ob[1].SetActive(true);
        }
        else { Add_DW_data.D_W_CanvasList_ob[1].SetActive(false); }

    }

    /// <summary>
    /// �Ա�
    /// �Ա� ���� :Dropbox �Ա� ���� ������ �̸�
    /// ��� ���� :Dropbox ��ݵ� ������ �̸�
    /// ���� ���� :inputFiled �Ա� ���� ����
    /// ��ü �ݾ� :inputFiled �Ա� ���� �ݾ�
    /// �з� : Dropbox ī�װ� ���� 
    /// </summary>

    //�� ���� �Ա� /Ÿ���� �Ա� ���� ���
    public Toggle Deposit_method_toggles_selection
    {
        get { return Add_DW_data.Deposit_method_toggleG.ActiveToggles().FirstOrDefault(); }
    }
    public void Deposit_method_Toggle()
    {
        if (Add_DW_data.Deposit_method_toggleG.ActiveToggles().Any())
        {
            if (Deposit_method_toggles_selection.name.Equals("MyPB_Deposit_Toggle"))
            {
                Deposit_method_Seletion_Active(true, false);
            }
            else if (Deposit_method_toggles_selection.name.Equals("OtherPB_Deposit_Toggle"))
            {
                Deposit_method_Seletion_Active(false, true);
            }
        }
    }
    void Deposit_method_Seletion_Active(bool my, bool other)
    {
        if (my == true)
        {
            Add_DW_data.Deposit_method_CanvasList_ob[0].SetActive(true);
        }
        else { Add_DW_data.Deposit_method_CanvasList_ob[0].SetActive(false); }
        if (other == true)
        {
            Add_DW_data.Deposit_method_CanvasList_ob[1].SetActive(true);
        }
        else { Add_DW_data.Deposit_method_CanvasList_ob[1].SetActive(false); }
    }
    List<string> PassBook_List = new List<string>();
    public void PassBookList_DropDown()
    {
        Add_DW_data.Deposit_me.MyPassBook_drp.ClearOptions();
        for (int i = 0; i < DataManager.instance.PassBook_List_data.Count; i++)
        {
            PassBook_List.Add(DataManager.instance.PassBook_List_data[i]["�����̸�"].ToString());
        }

        Add_DW_data.Deposit_me.MyPassBook_drp.AddOptions(PassBook_List);
    }
    //������ ��� / Ÿ���� ��� ���
    public Toggle Withdrawal_method_toggles_selection
    {
        get { return Add_DW_data.Withdrawal_method_toggleG.ActiveToggles().FirstOrDefault(); }
    }
    public void Withdrawal_method_Toggle()
    {
        if (Add_DW_data.Withdrawal_method_toggleG.ActiveToggles().Any())
        {
            if (Withdrawal_method_toggles_selection.name.Equals("MyPB_Withdrwal_Toggle")) { Withdrawal_method_Selection_Active(true, false); }
            else if (Withdrawal_method_toggles_selection.name.Equals("OtherPB_Withdrwal_Toggle")) { Withdrawal_method_Selection_Active(false, true); }
        }
    }
    void Withdrawal_method_Selection_Active(bool my, bool other)
    {
        if (my == true) { Add_DW_data.Withdrawal_method_CanavasList_ob[0].SetActive(true); }
        else { Add_DW_data.Withdrawal_method_CanavasList_ob[0].SetActive(false); }
        if (other == true) { Add_DW_data.Withdrawal_method_CanavasList_ob[1].SetActive(true); }
        else { Add_DW_data.Withdrawal_method_CanavasList_ob[1].SetActive(false); }
    }



}
