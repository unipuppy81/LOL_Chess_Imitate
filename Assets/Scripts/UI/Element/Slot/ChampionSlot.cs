using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChampionSlot : MonoBehaviour
{
    #region SerializeField 
    [Header("Top")]
    [SerializeField] private Image image_ChampionBackground;
    [SerializeField] private Image image_Champion;
    [SerializeField] private Image image_Attribute_1;
    [SerializeField] private Image image_Attribute_2;
    [SerializeField] private Image image_Attribute_3;
    [SerializeField] private TextMeshProUGUI text_Attribute_1;
    [SerializeField] private TextMeshProUGUI text_Attribute_2;
    [SerializeField] private TextMeshProUGUI text_Attribute_3;

    [SerializeField] private Image image_Upgrade_1;
    [SerializeField] private Image image_Upgrade_2;
    [SerializeField] private Image image_Upgrade_3;


    [Header("Bottom")]
    [SerializeField] private Image image_NameBackground;
    [SerializeField] private TextMeshProUGUI text_ChampionName;
    [SerializeField] private TextMeshProUGUI text_GoldCost;


    [Header("Attribute String")]
    private List<string> attributeString = new List<string>();
    private Image[] image_Attributes;
    private TextMeshProUGUI[] text_Attributes;

    #endregion

    #region Init

    public void ChampionSlotInit(ChampionBlueprint championBlueprint, Color color)
    {
        attributeString.Clear();

        image_ChampionBackground.color = color;
        // Image_champion = championBlueprint.championImage;
        // image_Attribute_1 = Dic ����ؼ� �Ӽ� �̹��� ����
        // image_Attribute_2 = Dic ����ؼ� �Ӽ� �̹��� ����


        attributeString.AddRange(new List<string> {
            championBlueprint.ChampionLine_First.ToString(),
            championBlueprint.ChampionLine_Second.ToString(),
            championBlueprint.ChampionJob_First.ToString(),
            championBlueprint.ChampionJob_Second.ToString()
        }.Where(attribute => attribute != "None"));
        

        int startIndex = 3 - attributeString.Count;

        for (int i = 0; i < image_Attributes.Length; i++)
        {
            image_Attributes[i].gameObject.SetActive(false);
            text_Attributes[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < attributeString.Count; i++)
        {
            int index = startIndex + i;
            image_Attributes[index].gameObject.SetActive(true);
            text_Attributes[index].gameObject.SetActive(true);

            text_Attributes[index].text = attributeString[i];
        }

        text_GoldCost.text = Utilities.SetSlotCost(championBlueprint.ChampionCost).ToString();
    }

    private void AttributeInit()
    {
        for (int i = 0; i < image_Attributes.Length; i++)
        {
            image_Attributes[i].gameObject.SetActive(false);
            text_Attributes[i].gameObject.SetActive(false);
        }
    }

    #endregion


    #region Unity Flow

    private void Start()
    {
        image_Attributes = new Image[] { image_Attribute_1, image_Attribute_2, image_Attribute_3 };
        text_Attributes = new TextMeshProUGUI[] { text_Attribute_1, text_Attribute_2, text_Attribute_3 };

        AttributeInit();
    }
    #endregion

}


