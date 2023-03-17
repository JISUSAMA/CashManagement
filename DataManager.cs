using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }
    [Header("Import ����")]
    public float month_import_f;
    [Space(5)][Header("Spend ����")]
    public float month_spend_f;
    [Space(5)][Header("Total Money ���� �ܾ�")]
    public float total_money_f;

    public List<Dictionary<string, object>> PassBook_List_data;
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
    }
 
}
