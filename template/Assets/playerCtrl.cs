using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCtrl : MonoBehaviour
{
    public GameObject maincam;
    public bool is_correct = false;
    public bool is_Click = false;
    private Vector3 startPoint;
    public Image cursorgauge;
    public Text textUI;
    private float gaugeTimer = 0.0f;
    private Vector3 screenCenter;
    private float walkSpeed = 2.0f;
    private Vector3 forward;
    private double mainTimer = 0.0;
    void Start()
    {
        startPoint = new Vector3(0, 1, 1);
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        cursorgauge.fillAmount = gaugeTimer;
        mainTimer += Time.deltaTime;
        if(Physics.Raycast(ray, out hit, 100.0f))
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                forward = new Vector3(hit.transform.position.x, this.transform.position.y, hit.transform.position.z);
                is_Click = true;
                StartCoroutine(MoveForward());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                is_Click = false;
            }
            if(hit.collider.name.Equals("doorWing")||hit.collider.CompareTag("panel")||hit.collider.CompareTag("Finish")){
                gaugeTimer += 1.0f / 3.0f * Time.deltaTime;
                if (hit.collider.name.Equals("doorWing") && gaugeTimer >= 1.0f)
                {
                    hit.transform.eulerAngles = Vector3.up * 90f;
                    gaugeTimer = 0.0f;
                }
                if (hit.collider.CompareTag("panel") && gaugeTimer >= 1.0f)
                {
                    textUI.text = hit.collider.GetComponent<questiontext>().text;
                    gaugeTimer = 0.0f;
                }
                if (hit.collider.CompareTag("Finish") && gaugeTimer >= 1.0f)
                {
                    textUI.text = hit.collider.GetComponent<questiontext>().text;
                }
            }
            else
            {
                gaugeTimer = 0.0f;
            }
            

        }


        }
    IEnumerator MoveForward()
    {
        while (is_Click)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, forward, Time.deltaTime * walkSpeed);

            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wrong"))
        {
            this.transform.position = startPoint;
        }
        else if(other.CompareTag("answer"))
        {
            Debug.Log("correct");
        }
    }

    public void Finish()
    {

    }

}
