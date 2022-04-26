/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class itemExcelItem : ExcelItemBase
{
	public ItemType itemType;
	public string[] tag;
	public int price;
	public int level;
}

[CreateAssetMenu(fileName = "itemExcelData", menuName = "Excel To ScriptableObject/Create itemExcelData", order = 1)]
public class itemExcelData : ExcelDataBase<itemExcelItem>
{
}

#if UNITY_EDITOR
public class itemAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		itemExcelItem[] items = new itemExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new itemExcelItem();
			items[i].id = allItemValueRowList[i]["id"];
			items[i].name = allItemValueRowList[i]["name"];
			items[i].itemType = (ItemType)(Convert.ToInt32(allItemValueRowList[i]["itemType"]));
			items[i].tag = allItemValueRowList[i]["tag"].Split(',');
			items[i].price = Convert.ToInt32(allItemValueRowList[i]["price"]);
			items[i].level = Convert.ToInt32(allItemValueRowList[i]["level"]);
		}
		itemExcelData excelDataAsset = ScriptableObject.CreateInstance<itemExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(itemExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


