
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;

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
    public struct Write_New_Data
    {
        public GameObject InputNewDataCanvas_ob; 
        public ToggleGroup WriteData_toggleGrup;
        public Toggle[] WriteData_toggle;
        public GameObject[] NewDataCanvasList;
        [Space(5f)]
        [Header("Deposit Money")]
        public InputField DepositMoney_inf;
        [Space(5f)]
        [Header("Withdrawal Money")]
        public InputField WithdrawalMoney_inf;
    }

    [System.Serializable]
    public struct Create_PassBook_Data
    {
        public GameObject create_popup_ob;
        public InputField Passbook_name;
        public InputField Passbook_memo;
        public Button creat_btn;
    }
    [Header("RecordList_Home_Grup")]
    public RecordList_Home_Grup RecordList_Main;

    [Header("Write_New_Data 입/출금 기록")]
    public Write_New_Data WriteNewData;

    [Header("Create_Passbook_Data")]
    public Create_PassBook_Data Create_passbook;

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

    public Toggle WriteData_selection
    {
        get { return WriteNewData.WriteData_toggleGrup.ActiveToggles().FirstOrDefault(); }
    }
    public void WriteDataGrup_Toggle()
    {
        if (WriteNewData.WriteData_toggleGrup.ActiveToggles().Any())
        {
            if (WriteData_selection.name.Equals("Deposit"))
            {
                WriteDataGrup_Selection_Active(true, false);
            }
            else if (WriteData_selection.name.Equals("Withdrawal"))
            {
                WriteDataGrup_Selection_Active(false, true);
            }
        }
    }
    void WriteDataGrup_Selection_Active(bool grup1, bool grup2)
    {
        //입금
        if (grup1 == true) { WriteNewData.NewDataCanvasList[0].SetActive(true); }
        else { WriteNewData.NewDataCanvasList[0].SetActive(false); }
        //출금
        if (grup2 == true) { WriteNewData.NewDataCanvasList[1].gameObject.SetActive(true); }
        else { WriteNewData.NewDataCanvasList[1].SetActive(false); }
    }
    ///////////////////////////  통장생성   ///////////////////////////////////////////////////
    public void OnClick_Create_Passbook()
    {
        Create_passbook.create_popup_ob.SetActive(true);
    }
    public void OnClick_Yes_CreateBtn()
    {
        string pass_book = Create_passbook.Passbook_name.text;
        string pass_memo = Create_passbook.Passbook_memo.text;
    }
    //입금,출금 기록 생성하기 
    public void Open_NewDataCanvas()
    {
        WriteNewData.InputNewDataCanvas_ob.SetActive(true); //팝업 활성화 

    }

   
}
