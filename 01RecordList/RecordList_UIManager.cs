
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
        [Header("입금 내역 작성")]
        public ToggleGroup Deposit_method_toggleG;
        public Toggle[] Deposit_method_toggles;
        public GameObject[] Deposit_method_CanvasList_ob;
        
        [Header("출금 내역 작성")]
        public ToggleGroup Withdrawal_method_toggleG;
        public Toggle[] Withdrawal_method_toggles;
        public GameObject[] Withdrawal_method_CanavasList_ob;

        [Header("DW_Method_Input_list")]
        public DW_Method_Input_list Deposit_me;
        public DW_Method_Input_list Deposit_other;
        public DW_Method_Input_list Withdrawal_me;
        public DW_Method_Input_list Withdrawal_other;

        [Header("내 계좌 입금")]
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
    [Header("입/출금 기록 추가 작성하기")]
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

    ///////////////////////////  통장생성   ///////////////////////////////////////////////////
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
        //통장이름
        string filePath = Application.dataPath + "/Resources/" + fileName + ".csv";
        FileInfo fileInfo = new FileInfo(filePath);
        Debug.Log(fileInfo);
        if (!fileInfo.Exists)
        {
            //파일 존재 안함
            File.Create(filePath); //파일 생성
            Debug.Log("-------1");
        }
        DataManager.instance.PassBook_List_data = CSVManager.Read(fileName);
        StreamWriter Writer = new StreamWriter(filePath);
        //통장 정보가 없으면 새로운 통장정보를 생성해준다
        //하나라도 있는 경우, 기존에 통장 정보를 새로적어주고 새로운 통장 정보도 갱신해줌

        if (!DataManager.instance.PassBook_List_data.Count.Equals(0))
        {
            Writer.WriteLine("넘버,통장이름,메모,색상");
            Debug.Log("data.Count: " + DataManager.instance.PassBook_List_data.Count);
            for (int i = 0; i <= DataManager.instance.PassBook_List_data.Count + 1; i++)
            {
                //기존의 갯수만큼 작성
                if (i <= DataManager.instance.PassBook_List_data.Count - 1)
                {
                    Writer.WriteLine(DataManager.instance.PassBook_List_data[i]["넘버"] + "," + DataManager.instance.PassBook_List_data[i]["통장이름"] + "," +
                           DataManager.instance.PassBook_List_data[i]["메모"] + "," + DataManager.instance.PassBook_List_data[i]["색상"]);
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
            Writer.WriteLine("넘버,통장이름,메모,색상");
            Writer.WriteLine(DataManager.instance.PassBook_List_data.Count + "," + Create_passbook.Passbook_name.text + "," +
            Create_passbook.Passbook_memo.text + "," +
            Create_passbook.Passbook_name.text);
        }

        Writer.Flush();
        //This closes the file
        Writer.Close();

        DataManager.instance.PassBook_List_data = CSVManager.Read(fileName);
        //생성완료
        Create_passbook.create_popup_ob.SetActive(false);
        yield return null;

    }
   
    /////////////////////////////  입출금 기록하기 //////////////////////////////////////////////
    public void OnClick_D_W_CreateBtn()
    {
        Add_DW_data.Deposit_Withdrawal_Canvas.SetActive(true);
        PassBookList_DropDown(); //통장 리스트 드롭박스 텍스트 입력
    }
    //입금/출금 선택 토글
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
    /// 입금
    /// 입금 통장 :Dropbox 입금 받은 통장의 이름
    /// 출금 통장 :Dropbox 출금된 통장의 이름
    /// 통장 내역 :inputFiled 입금 받은 내역
    /// 이체 금액 :inputFiled 입금 받은 금액
    /// 분류 : Dropbox 카테고리 종류 
    /// </summary>

    //내 계좌 입금 /타계좌 입금 선택 토글
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
            PassBook_List.Add(DataManager.instance.PassBook_List_data[i]["통장이름"].ToString());
        }

        Add_DW_data.Deposit_me.MyPassBook_drp.AddOptions(PassBook_List);
    }
    //내계좌 출금 / 타계좌 출금 토글
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
