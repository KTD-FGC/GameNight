using UnityEngine;

[System.Serializable]
public struct ButtonData
{
    //we define here the: animations, hitboxes, relevant data, 'while jumping/crouching' variants
}

[System.Serializable]
public struct CharacterData
{
    //movespeeds
    public float fWalkSpeed;
    public float bWalkSpeed;
    public float fDashSpeed;
    public float bDashSpeed;
    public float fJumpSpeed;
    public float nJumpSpeed;
    public float bJumpSpeed;
    public float fAirDashSpeed;
    public float bAirDashSpeed;

    //buttonDefs
    public ButtonData btn1;
    public ButtonData btn2;
    public ButtonData btn3;
    public ButtonData btn4;
    public ButtonData btn5;
    public ButtonData btn6;


    public CharacterData(float fWalkSpeed, float bWalkSpeed, float fDashSpeed, float bDashSpeed, float fJumpSpeed, 
        float nJumpSpeed, float bJumpSpeed, float fAirDashSpeed, float bAirDashSpeed, 
        ButtonData btn1, ButtonData btn2, ButtonData btn3, ButtonData btn4, ButtonData btn5, ButtonData btn6) {

        this.fWalkSpeed = fWalkSpeed;
        this.bWalkSpeed = bWalkSpeed;
        this.fDashSpeed = fDashSpeed;
        this.bDashSpeed = bDashSpeed;
        this.fJumpSpeed = fJumpSpeed;
        this.nJumpSpeed = nJumpSpeed;
        this.bJumpSpeed = bJumpSpeed;
        this.fAirDashSpeed = fAirDashSpeed;
        this.bAirDashSpeed = bAirDashSpeed;

        
        this.btn1 = btn1;
        this.btn2 = btn2;
        this.btn3 = btn3;
        this.btn4 = btn4;
        this.btn5 = btn5;
        this.btn6 = btn6;

    } //overwrite these values on children or directly call constructor
    
}
