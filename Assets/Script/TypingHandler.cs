using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TMPro;

public class TypingHandler : MonoBehaviour
{
	// 操作するテキスト
	public TextMeshProUGUI TextMesh;

	// 回答済みの色
	public string answeredColor;

	
	// お題の文字列
	private string themeString;
	
	// 現在の回答文字数
	private int currentAnswerLength;
	
	// colorタグの開始・終了
	private string colorTagStart;
	private string colorTagEnd;
	
	
	// Start is called before the first frame update
	void Start ()
	{
		// 現在の回答文字数初期化
		currentAnswerLength = 0;
		
		// colorタグの開始・終了を設定
		colorTagStart = "<color=" + answeredColor + ">";
		colorTagEnd = "</color>";
	}

	// Update is called once per frame
	void Update ()
	{
		UpdateThemeString ();
		UpdateAnsweredCharacterToRed ();
		KeyboardListener.ClearInputKeyName ();
	}
	
	
	// お題の文字列を更新する
	private void UpdateThemeString ()
	{
		// TODO
//		if ( GetStringExceptColorTag ().Length <= currentAnswerLength )
//		{
//			
//		}
	}
	
	// 回答した文字色を赤色にする
	private void UpdateAnsweredCharacterToRed ()
	{
		if ( IsCorrectAnswer () )
		{
			// 入力とお題の1文字が一致していた場合、現在の回答文字数を加算
			currentAnswerLength++;
			
			Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  currentAnswerLength : " + currentAnswerLength );
		}
		
		// 回答した文字色を赤色にする
		string redToString;
		if ( currentAnswerLength <= 0 )
		{
			redToString = "";
		}
		else
		{
			redToString = GetStringExceptColorTag ().Substring ( 0, currentAnswerLength );
		}
		
		// 未回答の文字列を取得
		string restString = GetStringExceptColorTag ().Substring (
			currentAnswerLength
			, GetStringExceptColorTag ().Length - currentAnswerLength
		);
		
		TextMesh.text = colorTagStart + redToString + colorTagEnd + restString;
	}
	
	// 回答が合っているか
	private bool IsCorrectAnswer ()
	{
//		Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  KeyboardListener.inputKeyName : " + KeyboardListener.inputKeyName );

		if ( GetNextAnswerCharacter () == null )
		{
			// 文末の場合は回答と一致しないのでfalseを返却
			return false;
		}
		// お題を全て大文字に変換して入力キーと比較する
		return KeyboardListener.inputKeyName == GetNextAnswerCharacter ().ToUpper ();
	}
	
	
	// 次に回答する文字を取得する
	private string GetNextAnswerCharacter ()
	{
		if ( GetStringAfterColorTag ().Length == 0 )
		{
//			Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  return null" );

			// 全文字回答済みで文末にいる場合、nullを返却
			return null;
		}
		
//		Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  GetStringAfterColorTag ().Substring ( 1, 1 ) : " + GetStringAfterColorTag ().Substring ( 1, 1 ) );

		// colorタグの次にある文字が回答対象
		return GetStringAfterColorTag ().Substring ( 0, 1 );
	}
	
	// colorタグの後ろにある文字列(未回答の文字)を取得する
	private string GetStringAfterColorTag ()
	{
		if ( TextMesh.text.LastIndexOf ( colorTagEnd ) == -1 )
		{
//			Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  return : " + TextMesh.text );

			// お題が出された直後(または1文字も回答していない場合)、colorタグが無いためtextをそのまま返却
			return TextMesh.text;
		}
		
		// colorタグの後ろにある文字列(未回答の文字)を取得する
		int colorTagEndIndex = TextMesh.text.LastIndexOf ( colorTagEnd );
		string result = TextMesh.text.Substring (
			colorTagEndIndex + colorTagEnd.Length
			, TextMesh.text.Length - ( colorTagEndIndex + colorTagEnd.Length )
		);
		
//		Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  result : " + result );

		return result;
	}
	
	// colorタグ以外の文字列(お題の文字列)を取得する
	private string GetStringExceptColorTag ()
	{
		string tagEnd = ">";
		int tagEndIndex = TextMesh.text.IndexOf ( tagEnd );
		int colorTagEndIndex = TextMesh.text.LastIndexOf ( colorTagEnd );
		
		// colorタグの中の文字列を取得
		string colorTagDuringString;
		if ( tagEndIndex == -1 )
		{
			// お題が出された直後(または1文字も回答していない場合)はcolorタグが無いので空文字に設定
			colorTagDuringString = "";
		}
		else
		{
			colorTagDuringString = TextMesh.text.Substring (
				tagEndIndex + tagEnd.Length
				, colorTagEndIndex - ( tagEndIndex + tagEnd.Length )
			);
		}
		
		// colorタグの後の文字列を所得、colorタグの中の文字列と結合
		string result = colorTagDuringString + GetStringAfterColorTag ();
		
//		Debug.Log ( "Class-" + this.GetType().Name + " Method-" + MethodBase.GetCurrentMethod().Name + "  result : " + result );
		
		return result;
	}
}
