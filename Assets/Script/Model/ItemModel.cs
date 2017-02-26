using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 静态,容易访问
/// </summary>
public static class ItemModel
{
    private static Dictionary<string, Item> GridItemDic = new Dictionary<string, Item>();

    public static void StoreItem(string name, Item item)
    {
        DeleteItem(name);

        GridItemDic.Add(name,item);
    }
    public static Item GetItem(string name)
    {
        if (GridItemDic.ContainsKey(name))
        {
            return GridItemDic[name];
        }
        else
            return null;
    }

    public static void DeleteItem(string name)
    {
        if (GridItemDic.ContainsKey(name))
            GridItemDic.Remove(name);

    }

}
