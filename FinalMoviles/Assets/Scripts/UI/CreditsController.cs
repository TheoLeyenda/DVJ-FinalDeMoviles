using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectCredits;
    public GameObject NameObject;
    public GameObject Y_Object;
    public float speedCredits;
    public Direction direction;
    public Vector2 Tope;
    private Vector3 initialPosition;
    public Text textNamePlayer;
    private GameData gd;
    public enum Direction
    {
        Up,
        Left,
        Right,
        Down,
    }
    private void Start()
    {
        gd = GameData.instaceGameData;
        initialPosition = ObjectCredits.transform.position;
    }
    private void OnEnable()
    {
        if (gd != null)
        {
            if (gd.currentNameUser != "None")
            {
                textNamePlayer.text = gd.currentNameUser;
                NameObject.SetActive(true);
                Y_Object.SetActive(true);
            }
            else
            {
                NameObject.SetActive(false);
                Y_Object.SetActive(false);
            }
        }
        else
        {
            NameObject.SetActive(false);
            Y_Object.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.Down:
                if (ObjectCredits.transform.position.y > Tope.y)
                {
                    ObjectCredits.transform.position = ObjectCredits.transform.position - ObjectCredits.transform.up * speedCredits * Time.deltaTime;
                }
                break;
            case Direction.Left:
                if (ObjectCredits.transform.position.x > Tope.x)
                {
                    ObjectCredits.transform.position = ObjectCredits.transform.position - ObjectCredits.transform.right * speedCredits * Time.deltaTime;
                }
                break;
            case Direction.Right:
                if (ObjectCredits.transform.position.x < Tope.x)
                {
                    ObjectCredits.transform.position = ObjectCredits.transform.position + ObjectCredits.transform.right * speedCredits * Time.deltaTime;
                }
                break;
            case Direction.Up:
                if (ObjectCredits.transform.position.y < Tope.y)
                {
                    ObjectCredits.transform.position = ObjectCredits.transform.position + ObjectCredits.transform.up * speedCredits * Time.deltaTime;
                }
                break;
        }
    }
    public void RestartPosition()
    {
        ObjectCredits.transform.position = initialPosition;
    }
}
