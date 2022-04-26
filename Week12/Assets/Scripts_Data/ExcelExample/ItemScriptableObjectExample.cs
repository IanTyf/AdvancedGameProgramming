using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Consumable, Food, Weapon };

[CreateAssetMenu(menuName = "Asset/ItemScriptableObjectExample")]
public class ItemScriptableObjectExample : ScriptableObject
{
	[SerializeField]
	itemExcelData itemDatabase;

	[System.Serializable]
	public struct ItemInfo
	{
		public string id;
		public GameObject prefab;
		public Sprite thumbnail;
		[HideInInspector]
		public itemExcelItem excelData;
	}

	public ItemInfo[] itemInfoList;

	public ItemInfo GetItem(string id)
	{
		for(int i = 0; i < itemInfoList.Length; i++)
		{
			ItemInfo itemInfo = itemInfoList[i];
			if (itemInfo.id == id)
			{
				itemInfo.excelData = itemDatabase.GetExcelItemByID("W001");
				return itemInfo;
			}
		}
		return new ItemInfo();
	}
}
