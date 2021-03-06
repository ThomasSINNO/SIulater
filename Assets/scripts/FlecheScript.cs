﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum typearrow
{
    LINK,AGGREG,COMPO,ASSO,HERIT,UNDEF,SUPPR,BPMN
};
public class FlecheScript : MonoBehaviour {

    public Sprite spr;
    protected List<GameObject> tab;
    public typearrow type;

    protected bool isActive;

    protected GameObject arrow;
	// Use this for initialization
	void Start () {
        isActive = false;
        tab = new List<GameObject>();
        arrow = GameObject.Find("Arrow");
        gameObject.tag = "ArrowButton";
    }

    static public GameObject[] getrects()
    {
        return(GameObject.FindGameObjectsWithTag("ArrowHitbox"));
    }

    static public GameObject[] getarrowbuttons()
    {
        return (GameObject.FindGameObjectsWithTag("ArrowButton"));
    }

    private void OnMouseDown()
    {
        GameObject[] buttons = getarrowbuttons();
        foreach(GameObject b in buttons)
        {
            if (this.gameObject != b)
            {
                b.GetComponent<FlecheScript>().ResetArrow();
            }
            
        }
        GameObject[] targets = getrects();
        foreach (GameObject gobj in targets)
        {
            if (!isActive)
            {
                gobj.GetComponent<ArrowHitboxScript>().Activate(this.gameObject);
            } else
            {
                gobj.GetComponent<ArrowHitboxScript>().Deactivate();
            }
            
        }
        if (!isActive)
        {
            isActive = true;

        } else
        {
            isActive = false;
        }
        
    }

    public void ResetArrow()
    {
        tab.Clear();
        isActive = false;
    }



    public void EndArrow(GameObject g)
    {
        if (g == null)
        {
            print("g null");
        }
        tab.Add(g);
        if (this.type == typearrow.SUPPR)
        {
            ArrowScript ars = tab[0].transform.parent.gameObject.GetComponent<ArrowScript>();
            if (ars != null)
            {
                ars.deletearrow();
                GameObject[] targets = getrects();
                foreach (GameObject gobj in targets)
                    gobj.GetComponent<ArrowHitboxScript>().Deactivate();
                ResetArrow();

            }
        }
        else
        {
            if (tab.Count > 1)
            {

                GameObject newArrow = GameObject.Instantiate(arrow);
                newArrow.tag = "ParentArrowTag";
                newArrow.transform.position = new Vector3(0f, 0f, 0f);

                ArrowScript arrow_script = newArrow.GetComponent<ArrowScript>();
                if (arrow_script == null)
                {
                    print("ERROR: the arrow (parent of depart and arrivee) doesn't have an arrowscript");
                    return;
                }
                GameObject true_start_point = tab[0].transform.parent.gameObject;
                GameObject true_end_point = tab[1].transform.parent.gameObject;
                arrow_script.setArrowProperties(type, true_start_point, true_end_point);
                //check if the start/end point is an arrow
                ArrowScript true_start_point_arrow_script = true_start_point.GetComponent<ArrowScript>();
                ArrowScript true_end_point_arrow_script = true_end_point.GetComponent<ArrowScript>();

                //check for a feature we don't support    
                if (true_start_point_arrow_script != null && true_end_point_arrow_script != null)
                {
                    print(System.Reflection.MethodBase.GetCurrentMethod().Name + ":ERROR:\n"
                   + "Attaching an arrow between two arrows is not supported ");
                    //we delete the newly created arrow and we remove the boolean we set earlier
                    arrow_script.deletearrow();
                    return;
                }
                if (true_start_point_arrow_script != null)
                    true_start_point_arrow_script.setMidPointAttached(true);
                if (true_end_point_arrow_script != null)
                    true_end_point_arrow_script.setMidPointAttached(true);

                GameObject dep = newArrow.transform.Find("depart").gameObject;
                dep.transform.position = tab[0].transform.position+new Vector3(0,0,-3);
                SpriteRenderer rend1 = dep.gameObject.GetComponent<SpriteRenderer>();
                rend1.color = Color.clear;
                //print("Instantiate dep !");

                GameObject arr = newArrow.transform.Find("arrivee").gameObject;
                arr.transform.position = tab[1].transform.position+new Vector3(0, 0, -3);
                SpriteRenderer rend2 = arr.gameObject.GetComponent<SpriteRenderer>();
                if (newArrow.GetComponent<ArrowScript>().type == typearrow.AGGREG)
                {
                    rend2.sprite = spr;
                    //dep.GetComponent<NameBoxScript>().spr = spr;
                    //arr.GetComponent<NameBoxScript>().spr = spr;
                } else if (newArrow.GetComponent<ArrowScript>().type == typearrow.ASSO)
                {
                    rend2.color = Color.clear;
                } else if (newArrow.GetComponent<ArrowScript>().type == typearrow.COMPO)
                {
                    rend2.sprite=spr;
                    //dep.GetComponent<NameBoxScript>().spr = spr;
                    //arr.GetComponent<NameBoxScript>().spr = spr;
                } else if (newArrow.GetComponent<ArrowScript>().type == typearrow.HERIT)
                {
                    rend2.sprite=spr;
                    //dep.GetComponent<NameBoxScript>().spr = spr;
                    //arr.GetComponent<NameBoxScript>().spr = spr;
                } else if (newArrow.GetComponent<ArrowScript>().type == typearrow.LINK)
                {
                    rend2.color = Color.clear;
                } else if (newArrow.GetComponent<ArrowScript>().type == typearrow.BPMN)
                {
                    rend2.sprite = spr;
                    //dep.GetComponent<NameBoxScript>().spr = spr;
                    //arr.GetComponent<NameBoxScript>().spr = spr;
                }

                    GameObject mid = newArrow.transform.Find("milieu").gameObject;
                float x = (dep.transform.position.x + arr.transform.position.x) / 2;
                float y = (dep.transform.position.y + arr.transform.position.y) / 2;
                //float z = (dep.transform.position.z + arr.transform.position.z) / 2;
                mid.transform.position = new Vector3(x, y, dep.transform.position.z);
                SpriteRenderer rend3 = mid.gameObject.GetComponent<SpriteRenderer>();

                GameObject line = newArrow.transform.Find("Line").gameObject;
                LineRenderer l = line.GetComponent<LineRenderer>();
                Vector3[] pos = { dep.transform.position, arr.transform.position };
                l.SetPositions(pos);
                Renderer rend = line.GetComponent<Renderer>();
                if (newArrow.GetComponent<ArrowScript>().type == typearrow.ASSO)
                {
                    rend.material.shader = Shader.Find("Unlit/Color");
                    rend.material.color = Color.red;
                }
                else
                {
                    rend.material.shader = Shader.Find("Unlit/Color");
                    rend.material.color = Color.black;
                }
                

                GameObject[] targets = getrects();
                foreach (GameObject gobj in targets)
                    gobj.GetComponent<ArrowHitboxScript>().Deactivate();
                ResetArrow();
            }
            else
                print("1st part added");
        }
        
    }
    // Update is called once per frame
    void Update () {
		
	}
}
