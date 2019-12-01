using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class KeyboardListener : MonoBehaviour
{
	public static string inputKeyName;
	
	// Start is called before the first frame update
	void Start()
	{
		inputKeyName = "";
	}

	// Update is called once per frame
	void Update()
	{
		if ( Input.anyKeyDown )
		{	// なにかキーが押された場合
			foreach ( KeyCode code in Enum.GetValues ( typeof ( KeyCode ) ) )
			{
				if ( Input.GetKeyDown ( code ) )
				{
					// 入力されたキー名を表示
					Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  Input key : " + code.ToString () );
					
					inputKeyName = code.ToString ();
				}
			}
		}
	}
	
	
	public static void ClearInputKeyName ()
	{
		inputKeyName = "";
	}
	
//	// inputKeyNameを取得する
//	public string GetinputKeyName ()
//	{
//		return inputKeyName;
//	}
//	
//	// InputKeyNameを設定する
//	private void SetinputKeyName ( string inputKeyName )
//	{
//		this.inputKeyName = inputKeyName;
//	}
}
