using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XmlSistemiScript2 : MonoBehaviour
{
    XmlDocument itemDataXml;                                                                 //Burada XmlDocument oluşturuyoruz xmldeki verileri çekmek için.
   
    private void Awake()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("ItemData");                     //Buradan iligig xml dosyamızı yüklüyoruz.
        itemDataXml = new XmlDocument();                                                    //Burada bir boş bir XmlDosyası oluşturuyoruz.
        itemDataXml.LoadXml(xmlTextAsset.text);                                             //Burada ItemData daki yazıları itemDataXml boş dosyamıza yüklüyoruz.
        InventoryTitle();
    }

    private void Update()
    {
        
    }

    public void InventoryTitle()
    {
        XmlNodeList items = itemDataXml.SelectNodes("/InventoryItems/InventoryItem");               //Burada select nodes ile ilgili nodes a gidiyoruz ve Listeliyoruz. !!!!!!!!!!!!

        foreach (XmlNode item in items)
        {
           Debug.Log("ItemTitle = " +item["ItemTitle"].InnerText);                                //Bu şekilde ilgili node un textine erişiyoruz :DDDDD
           Debug.Log("ItemID = " + item.Attributes["ID"].Value);                                //Attributes ile daldaki özelliklere erişiyoruz .
        }

    }

/*
    class InventoryItem
    {
        public string itemID { get; private set; }
        public string itemType { get; private set; }
        public string itemTitle { get; private set; }
        public string itemDescription { get; private set; }
        public Color bgColor { get; private set; }

        public InventoryItem(XmlNode curItemNode)
        {
            itemID = curItemNode.Attributes["ID"].Value;
            itemType = curItemNode.Attributes["Type"].Value;
            itemID = curItemNode["ItemTitle"].InnerText;
            itemID = curItemNode.Attributes["ITemDesc"].Value;

            XmlNode colorNode = curItemNode.SelectSingleNode("Color");                      //Ne olduğunu sonra anlayacağım.

            float bgR = float.Parse(colorNode["r"].InnerText);
            float bgG = float.Parse(colorNode["g"].InnerText);
            float bgB = float.Parse(colorNode["b"].InnerText);
            float bgA = float.Parse(colorNode["a"].InnerText);


            
        }

    }*/
}
