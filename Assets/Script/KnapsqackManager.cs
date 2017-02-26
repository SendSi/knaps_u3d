using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;


public class KnapsqackManager : MonoBehaviour
{

    private static KnapsqackManager _instance;
    public static KnapsqackManager Instance { get { return _instance; } }
    public GridPanelUI GridPanelUI;

    public ToolKitUIScript toolKitUI;

    void Awake()
    {
        Load();//数据
        _instance = this;//单例  
        GridUIScript.onEnter += GridUI_OnEnter;
        GridUIScript.onExit += GridUI_OnExit;

        GridUIScript.onLeftBeginDrag += GridUIItem_onLeftBeginDrag;
        GridUIScript.onLeftEndDrag += GridUIItem_onLeftEndDrag;
    }

    public bool isDrag = false;
    public DragItemUIScript dragItemUIScript;
    private void GridUIItem_onLeftEndDrag(Transform prevTransform,Transform enterTransform)
    {
        //ragItemUIScript
        isDrag = false;
        dragItemUIScript.IsShowDragItem(false);

        if (enterTransform == null)//拖到UI外面,扔了
        {
            ItemModel.DeleteItem(prevTransform.name);
            Debug.Log("扔东西");
        }
        //1.拖到另一地方,
   else if (enterTransform.tag == "Grid")
        {//是否子物体有值
            if (enterTransform.childCount == 0)
            {
                Item item = ItemModel.GetItem(prevTransform.name);
                createNewItem(item,enterTransform);
                ItemModel.DeleteItem(prevTransform.name);
            }
            else
            {
                Destroy(enterTransform.GetChild(0).gameObject);
                Item prevGridItem = ItemModel.GetItem(prevTransform.name);
                Item enterGridItem = ItemModel.GetItem(enterTransform.name);

                this.createNewItem(prevGridItem,enterTransform);
                this.createNewItem(enterGridItem, prevTransform);
            }
        }
        else//拖到UI的其他 的地方
        {
            Item item = ItemModel.GetItem(prevTransform.name);
            createNewItem(item, prevTransform);
        }
    }

    private void GridUIItem_onLeftBeginDrag(Transform gridTransform)
    {
        if (gridTransform.childCount == 0) return;
        else
        {
            Item item = ItemModel.GetItem(gridTransform.name);
            dragItemUIScript.UpdateItem(item.Name);
            Destroy(gridTransform.GetChild(0).gameObject);
         //   ItemModel.DeleteItem(gridTransform.name);
            isDrag = true;
        }
    }

    public void GridUI_OnEnter(Transform gridTransform)
    {
        Item item = ItemModel.GetItem(gridTransform.name);

        if (item == null)
            return;

        toolKitUI.updateText(GetToolTipText(item));
        isShow = true;
    }

    public void GridUI_OnExit()
    {
        toolKitUI.IsShowToolKit(false);
        isShow = false;
    }


    public Dictionary<int, Item> ItemList;
    void Load()
    {
        ItemList = new Dictionary<int, Item>();

        Weapon w1 = new Weapon(0, "牛刀", "牛B的刀！", 20, 10, "", 100);
        Weapon w2 = new Weapon(1, "羊刀", "杀羊刀。", 15, 10, "", 20);
        Weapon w3 = new Weapon(2, "宝剑", "大宝剑！", 120, 50, "", 500);
        Weapon w4 = new Weapon(3, "军枪", "可以对敌人射击，很厉害的一把枪。", 1500, 125, "", 720);

        Consumable c1 = new Consumable(4, "红瓶", "加血", 25, 11, "", 20, 0);
        Consumable c2 = new Consumable(5, "蓝瓶", "加蓝", 39, 19, "", 0, 20);

        Armor a1 = new Armor(6, "头盔", "保护脑袋！", 128, 83, "", 5, 40, 1);
        Armor a2 = new Armor(7, "护肩", "上古护肩，锈迹斑斑。", 1000, 0, "", 15, 40, 11);
        Armor a3 = new Armor(8, "胸甲", "皇上御赐胸甲。", 153, 0, "", 25, 30, 11);
        Armor a4 = new Armor(9, "护腿", "预防风寒，从腿做起", 999, 60, "", 19, 30, 51);

        ItemList.Add(w1.Id, w1);
        ItemList.Add(w2.Id, w2);
        ItemList.Add(w3.Id, w3);
        ItemList.Add(w4.Id, w4);
        ItemList.Add(c1.Id, c1);
        ItemList.Add(c2.Id, c2);
        ItemList.Add(a1.Id, a1);
        ItemList.Add(a2.Id, a2);
        ItemList.Add(a3.Id, a3);
        ItemList.Add(a4.Id, a4);
    }
    public void StoreItem(int itemId)
    {
        if (!ItemList.ContainsKey(itemId)) return;

        Transform emptyGird = GridPanelUI.GetEmpeyGrid();
        if (emptyGird == null)
        {
            print("背包已满");
            return;
        }
        Item temp = ItemList[itemId];
        createNewItem(temp,emptyGird);

    }

    public string GetToolTipText(Item item)
    {
        if (item == null) return "";

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color=red>{0}</color>\n\n", item.Name);
        switch (item.ItemType)
        {
            case "Armor":
                {
                    Armor aror = item as Armor;
                    sb.AppendFormat("力量:{0}\n防御:{1}\n敏捷:{2}\n\n", aror.Power, aror.Defend, aror.Agility);
                }
                break;
            case "Consumable":
                {
                    Consumable consu = item as Consumable;
                    sb.AppendFormat("HP:{0}\nMP:{1}\n\n", consu.BackHp, consu.BackMp);
                }
                break;
            case "Weapon":
                {
                    Weapon we = item as Weapon;
                    sb.AppendFormat("攻击:{0}\n\n", we.Damage);
                }
                break;
            default:
                break;
        }
        sb.AppendFormat("<size=25><color=white>购买价格:{0}\n出售价格:{1}</color></size>\n\n<color=yellow><size=20>描述:{2}</size></color>", item.BuyPrice, item.SellPrice, item.Description);
        return sb.ToString();
    }

    public bool isShow = false;
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, Input.mousePosition, null, out position);

              if (isDrag)
        {
            dragItemUIScript.IsShowDragItem(true);
            dragItemUIScript.SetLocalPosition(position);
        }
              else if (isShow)
        {
            toolKitUI.IsShowToolKit(true);//true为显示
            toolKitUI.SetLocalPosition(position);
        }
   
    }

    private void createNewItem(Item item, Transform emptyGird)
    {
        if (item == null) return;
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        itemPrefab.GetComponent<ItemUI>().UpdateItem(item.Name);
        GameObject itemGo = GameObject.Instantiate(itemPrefab);
        itemGo.transform.SetParent(emptyGird);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;


        ItemModel.StoreItem(emptyGird.name, item);//存储数据 
    }
}
