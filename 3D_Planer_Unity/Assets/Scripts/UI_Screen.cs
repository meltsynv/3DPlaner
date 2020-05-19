using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEditor.EventSystems;
using UnityEditor.Events;

namespace UI.Start
{
    [UnityEngine.RequireComponent(typeof(Animator))]
    [UnityEngine.RequireComponent(typeof(CanvasGroup))]
    
    public class UI_Screen : MonoBehaviour
    {
        #region variables
        public Selectable m_StartSelectable;

        [Header("Screen Events")]
        public UnityEvent onScreenStart = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();

        private Animator animator;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            if (m_StartSelectable)
            {
                EventSystem.current.SetSelectedGameObject(m_StartSelectable.gameObject);
            }
        }

        #region helper methods
        public virtual void StartScreen()
        {
            if(onScreenStart != null)
            {
                onScreenStart.Invoke();
            }
            handleAnimator("show");
        }
        public virtual void CloseScreen()
        {
            if(onScreenClose != null)
            {
                onScreenClose.Invoke();
            }
            handleAnimator("hide");


        }
         public void handleAnimator(string trigger)
        {
            if (animator)
            {
                animator.SetTrigger(trigger);
            }
        }
        
        #endregion

    }
}

