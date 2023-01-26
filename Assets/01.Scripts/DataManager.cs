using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }
    [Header("Import 수입")]
    public float month_import_f;
    [Space(5)][Header("Spend 지출")]
    public float month_spend_f;
    [Space(5)][Header("Total Money 보유 잔액")]
    public float total_money_f;

    public List<Dictionary<string, object>> PassBook_List_data;
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
    }
 
}
