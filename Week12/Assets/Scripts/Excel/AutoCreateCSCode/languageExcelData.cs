/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class languageExcelItem : ExcelItemBase
{
	public string English;
	public string SimplifiedChinese;
}

[CreateAssetMenu(fileName = "languageExcelData", menuName = "Excel To ScriptableObject/Create languageExcelData", order = 1)]
public class languageExcelData : ExcelDataBase<languageExcelItem>
{
}

#if UNITY_EDITOR
public class languageAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		languageExcelItem[] items = new languageExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new languageExcelItem();
			items[i].id = allItemValueRowList[i]["id"];
			items[i].name = allItemValueRowList[i]["name"];
			items[i].English = allItemValueRowList[i]["English"];
			items[i].SimplifiedChinese = allItemValueRowList[i]["SimplifiedChinese"];
		}
		languageExcelData excelDataAsset = ScriptableObject.CreateInstance<languageExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(languageExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


