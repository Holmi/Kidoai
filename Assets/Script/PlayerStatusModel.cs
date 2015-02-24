using UnityEngine;
using System.Collections;

public class PlayerStatusModel : MonoBehaviour {

	public static int p1_money;
	public static int p1_stamina;
	public static int p1_looks;
    public static int p1_maxStamina;
    public static int p1_takeTime;

    public static int p2_money;
    public static int p2_stamina;
    public static int p2_looks;
    public static int p2_maxStamina;
    public static int p2_takeTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static int getP1Money() { return p1_money; }
    public static int getP1Stamina() { return p1_stamina; }
    public static int getP1Looks() { return p1_looks; }
    public static int getP1MaxStamina() { return p1_maxStamina; }
    public static int getP1TakeTime() { return p1_takeTime; }

    public static int getP2Money() { return p2_money; }
    public static int getP2Stamina() { return p2_stamina; }
    public static int getP2Looks() { return p2_looks; }
    public static int getP2MaxStamina() { return p2_maxStamina; }
    public static int getP2TakeTime() { return p2_takeTime; }
}
