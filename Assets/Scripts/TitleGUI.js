#pragma strict

var customSkin:GUISkin;
var buttonW:int = 100;
var buttonH:int = 50;
var halfScreenW:float = Screen.width/2;
var halfButtonW:float = buttonW/2;

function OnGUI()
{
	GUI.skin = customSkin;

	if(GUI.Button(Rect(halfScreenW-halfButtonW,560,buttonW,buttonH), "Play Game"))
	{
		Application.LoadLevel("game");
	}
}

function Start () {
	halfScreenW = Screen.width/2;
}

function Update () {

}