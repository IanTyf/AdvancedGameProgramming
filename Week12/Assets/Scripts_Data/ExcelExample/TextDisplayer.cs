using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayer : MonoBehaviour {

	public delegate void TextDisplayerGroup();
	public static TextDisplayerGroup TextDisplayerGroupRefresh;
	public static string CURRENT_LANGUAGE = "English";

	protected languageExcelData languageData;

	private void Awake()
	{
		//Load the language data locally
		languageData = Resources.Load<languageExcelData>("ExcelAsset/languageExcelData");
		Debug.Log(languageData.GetExcelItemByID("T001"));
	}

	void OnEnable()
	{
		TextDisplayerGroupRefresh += LanguageCheck;
		LanguageCheck();
	}

	void OnDisable()
	{
		TextDisplayerGroupRefresh -= LanguageCheck;
	}

	// Update is called once per frame
	public virtual void LanguageCheck()
	{

	}
}
