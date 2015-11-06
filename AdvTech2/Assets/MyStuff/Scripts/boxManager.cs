﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class boxManager : MonoBehaviour {

    public GameObject[] boxes;
    public Text lengthText;
    public Image GuiImage;
    public Sprite play, pause, stop, fwd, back;
    public Slider SliderObject;
    public float updateTime = 0.01f;

    List<Vector3> pos = new List<Vector3>();
    List<Quaternion> rot = new List<Quaternion>();
  
    List<List<Vector3>> posArray = new List<List<Vector3>>();
    List<List<Quaternion>> rotArray = new List<List<Quaternion>>();

    int scroll = 0;
    bool isPaused;
    float tick = 0;
	// Use this for initialization
	void Start () {
        StartCoroutine(record());
	}

    IEnumerator record()
    {
        while (!isPaused)
        {
            updateVariables();
            yield return new WaitForSeconds(updateTime);
        }
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SliderObject.interactable = true;
            SliderObject.maxValue = posArray.Count;
            SliderObject.value = posArray.Count-1;
            //lengthText.text = ""+(tick * updateTime);

            scroll = posArray.Count;
            isPaused = !isPaused;
        }

        if (Input.GetKey(KeyCode.W))
        {
            GuiImage.sprite = fwd;
            scroll++;
            if (scroll >= posArray.Count)
                scroll = posArray.Count -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            GuiImage.sprite = back;
            scroll--;
            if (scroll < 1)
                scroll = 1;
        }
        
        if (isPaused)
        {
            GuiImage.sprite = pause;
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].transform.position = posArray[scroll-1][i];
                boxes[i].transform.rotation = rotArray[scroll-1][i];
            }
        }
        else
        {
            SliderObject.interactable = false;
            GuiImage.sprite = play;
        }
    }

    void updateVariables()
    {
        pos.Clear();
        rot.Clear();
        for (int i = 0; i < boxes.Length; i++)
        {
            pos.Add(boxes[i].transform.position);
            rot.Add(boxes[i].transform.rotation);
        }
        
        posArray.Add(new List<Vector3>(pos));
        rotArray.Add(new List<Quaternion>(rot));
        tick++;
    }

    public void setScroll(float newscroll)
    {
        scroll = (int)newscroll;
    }

}
