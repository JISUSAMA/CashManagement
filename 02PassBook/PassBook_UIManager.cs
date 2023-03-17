using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PassBook_UIManager : MonoBehaviour
{
    public InputField PassBook_Name;
    public InputField PassBook_Use;
    public string PassBook_Color_str;


    public ToggleGroup PassBook_Color_toggleGrup;
    public Toggle[] PassBook_Color_toggle; 
    

    public void OnClick_CloseBtn()
    {
        SceneManager.LoadScene("01RecordList");
    }
    public void OnClick_CreatePassBook()
    {
        if(!PassBook_Name.text.Length.Equals(0)&& !PassBook_Use.text.Length.Equals(0))
        {
            //생성 성공
            
        }
        else
        {

        }
    }
    public Toggle Set_PassBook_Color
    {
         get { return PassBook_Color_toggleGrup.ActiveToggles().FirstOrDefault(); }
    }
    public void SetChoiceColorToggle()
    {
       if(PassBook_Color_toggleGrup.ActiveToggles().Any())
        {
            if (Set_PassBook_Color.name.Equals("Color1")){ PassBook_Color_str = "#C69392"; }
            else if (Set_PassBook_Color.name.Equals("Color2")){ PassBook_Color_str = "#FF8A89"; }
            else if (Set_PassBook_Color.name.Equals("Color3")){ PassBook_Color_str = "#F98358"; }
            else if (Set_PassBook_Color.name.Equals("Color4")){ PassBook_Color_str = "#FFEA00"; }
            else if (Set_PassBook_Color.name.Equals("Color5")){ PassBook_Color_str = "#FFC900"; }
            else if (Set_PassBook_Color.name.Equals("Color6")){ PassBook_Color_str = "#FF9000"; }
            else if (Set_PassBook_Color.name.Equals("Color7")){ PassBook_Color_str = "#B88ED1"; }
            else if (Set_PassBook_Color.name.Equals("Color8")){ PassBook_Color_str = "#88C8FF"; }
            else if (Set_PassBook_Color.name.Equals("Color9")){ PassBook_Color_str = "#4EB3CB"; }
            else if (Set_PassBook_Color.name.Equals("Color10")){ PassBook_Color_str = "#007355"; }
            else if (Set_PassBook_Color.name.Equals("Color11")){ PassBook_Color_str = "#33B993"; }
            else if (Set_PassBook_Color.name.Equals("Color12")){ PassBook_Color_str = "#9AE102"; }
            else if (Set_PassBook_Color.name.Equals("Color13")){ PassBook_Color_str = "#000000"; }
            else if (Set_PassBook_Color.name.Equals("Color14")){ PassBook_Color_str = "#636363"; }
            else if (Set_PassBook_Color.name.Equals("Color15")){ PassBook_Color_str = "#0000FF"; }
            else if (Set_PassBook_Color.name.Equals("Color16")){ PassBook_Color_str = "#FF0000"; }

        }
    }
}
