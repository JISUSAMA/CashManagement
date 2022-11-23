
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class RecordList_UIManager : MonoBehaviour
{
    public ToggleGroup RecordList_toggleGrup;
    public Toggle[] RecordList_toggle;
    public GameObject[] RecordCanvasList_ob;

    public ToggleGroup Latest_toggleGrup;
    public Toggle[] Latest_toggle;
    public GameObject[] LatestCanvasList_ob;


    public Toggle RecordList_Seletion
    {
        get { return RecordList_toggleGrup.ActiveToggles().FirstOrDefault(); }
    }

    public void SetUpToggleChoice()
    {
        if (RecordList_toggleGrup.ActiveToggles().Any())
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
        if (grup1 == true) { RecordCanvasList_ob[0].gameObject.SetActive(true); }
        else { RecordCanvasList_ob[0].gameObject.SetActive(false); }
        if (grup2 == true) { RecordCanvasList_ob[1].gameObject.SetActive(true); }
        else { RecordCanvasList_ob[1].gameObject.SetActive(false); }
        if (grup3 == true) { RecordCanvasList_ob[2].gameObject.SetActive(true); }
        else { RecordCanvasList_ob[2].gameObject.SetActive(false); }
        if (grup4 == true) { RecordCanvasList_ob[3].gameObject.SetActive(true); }
        else { RecordCanvasList_ob[3].gameObject.SetActive(false); }
    }
    public Toggle Latest_Selection
    {
        get { return Latest_toggleGrup.ActiveToggles().FirstOrDefault(); }
    }
    public void Latest_Toggle()
    {
        if (Latest_toggleGrup.ActiveToggles().Any())
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
       if(grup1== true) { LatestCanvasList_ob[0].SetActive(true); } 
        else { LatestCanvasList_ob[0].SetActive(false); }
       if(grup2 == true) { LatestCanvasList_ob[1].SetActive(true); }
        else { LatestCanvasList_ob[1].SetActive(false); }
    }

        public void OnClick_Create_Passbook()
    {
        SceneManager.LoadScene("02PassBook");
    }
}
