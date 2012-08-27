using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
//using System.Xml.Linq;
using System.Linq;

public class battleGameBehaviors : MonoBehaviour {
	public static object traitFile;
	// Use this for initialization
	void Start () {
		//traitFile = XElement.Load("TraitFile.xml");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
			float friendlyHealth = 200;
		
		float enemyHealth = 200;
	void Awake(){

		
	}
	
	public GUIContent friendlyHealthBar;
	public GUIContent enemyHealthBar;
	
	void OnGui(){
		GUI.Box( new Rect(1, 1, friendlyHealth, 20),friendlyHealthBar);
		GUI.Box( new Rect(1, 959 - friendlyHealth, enemyHealth, 20), enemyHealthBar);
		if (PlayerPeas.won)GUI.Box( new Rect(200, 200, 500, 300), "WIN");
		if (PlayerPeas.lost)GUI.Box( new Rect(200, 200, 500, 300), "LOST");
	}
	
	public static int compareArmyValue(ArmyTraits one, ArmyTraits two){
		return Random.Range(1,5);
	}


public class Trait{
	public string code = "";
	public string name = "";
	public int Atk = 0;
	public int Def = 0;
	public int Agi = 0;
	public Trait(int pAtk, int pDef, int pAgi, string pname, string pcode){
		Atk = pAtk;
		Def = pDef;
		Agi = pAgi;
		name = pname;
		code = pcode;
	}
}

public class ArmyTraits{
	public List<Trait> traitCollection = new List<Trait>();
	public ArmyTraits(string code, object traitFile){
		for (int i = 0; i < (code.Length - 4) ; i += 4){
			string codeTest = code.Substring(i, 4);
			//what the fuck, load the goddamn variables from the xml file!!!
			//it may be enum time.
		}
	}
	public int Atk{
		get {
			return calcAtk();
		}
	}
	int baseAtk = 20;
	int calcAtk(){
		int result = baseAtk;
		foreach (Trait bleh in traitCollection){
			result += bleh.Atk;
		}
		return result;
	}
	public int Def{
		get {
			return calcDef();
		}
	}
	int baseDef = 20;
	int calcDef(){
		int result = baseDef;
		foreach (Trait bleh in traitCollection){
			result += bleh.Def;
		}
		return result;
	}
		public int Agi{
		get {
			return calcAgi();
		}
	}
	int baseAgi = 20;
	int calcAgi(){
		int result = baseAgi;
		foreach (Trait bleh in traitCollection){
			result += bleh.Agi;
		}
		return result;
	}
}
}