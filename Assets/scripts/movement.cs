using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {


    Rigidbody rBody;
    public Vector3 COM = new Vector3(0, 0, 0);
    public WheelCollider[] wc;
    public bool BreakAllowed;
    public int wc_Torque_Length;
    public float m_Steer = 25f;
    public float max_break = 10000;
    public float m_Torque = 2500f;
    public float max_speed = 1000f;
    public int wc_decelerationspeed_length;
    public GUIText speedText;
    public WheelCollider wheelRR;
    public WheelCollider whelRL;
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
        rBody.centerOfMass = COM;
	}
	
    void Update()
    {
        if(speedText != null)
        {
            speedText.text = "Speed:" + Speed().ToString("f0") + " km/h";
        }
    }
	
    public float Speed()
    {
        return wheelRR.radius * Mathf.PI * wheelRR.rpm * 60f / 1000f;
    }

    public float Rpm()
    {
        return whelRL.rpm;
    }

    private void DecelarationSpeed()
    {
        Debug.Log("Deceleration speed");
        if(!BreakAllowed && Input.GetButton("Vertical") == false)
        {
            for(int i = 0; i < wc_decelerationspeed_length; i++)
            {
                wc[i].brakeTorque = max_speed;
                wc[i].motorTorque = 0;
            }
        }
    }

    private void Handbrake()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            BreakAllowed = true;
        } else
        {
            BreakAllowed = false;
        }
        if(BreakAllowed)
        {
            for (int i = 0; i < wc_Torque_Length; i++)
            {
                wc[i].brakeTorque = max_break;
                wc[i].motorTorque = 0f;
            }
        } else if(!BreakAllowed && Input.GetButton("Vertical") == true)
        {
            for (int i = 0; i < wc_Torque_Length; i++)
            {
                wc[i].brakeTorque = 0;
            }
        }
    }
}
