using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        
        void Update()
        {
            //Mathf.Approximately compares two floats and returns true if their values are similar
            if(Mathf.Approximately(healthComponent.GetFraction(), 0) || Mathf.Approximately(healthComponent.GetFraction(), 1))
            {
                foreground.GetComponentInParent<Canvas>().enabled = false;
                return;
            }
            foreground.GetComponentInParent<Canvas>().enabled = true;            

            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1f, 1f);
        }
    }
}