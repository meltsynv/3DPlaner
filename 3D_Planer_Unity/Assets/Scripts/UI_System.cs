using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using  UnityEditor.EventSystems;
using UnityEngine.UI;

namespace UI.Start
{
    public class UI_System : MonoBehaviour
    {
        #region Variables
        public UnityEvent onSwitchedScreen = new UnityEvent();
        public UnityEvent onSwitchedImage = new UnityEvent();

        public Component[] screens = new Component[0];
        public Component[] images = new Component[0];

        private UI_Screen previousScreen;
        public UI_Screen PreviousScreen { get { return previousScreen; } } //Property

        private UI_Screen currentScreen;
        public UI_Screen CurrentScreen { get { return currentScreen; } } //Property

        private RawImage currentImage;
        private RawImage previousImage;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            screens = GetComponentsInChildren<UI_Screen>(true);
            images = GetComponentsInChildren<RawImage>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        # region helper methods
        public void switchScreen(UI_Screen targetScreen)
        {
            if (targetScreen)
            {
                if (currentScreen)
                {
                    // currentScreen.Close();
                    previousScreen = currentScreen;
                }
                currentScreen = targetScreen;
                currentScreen.gameObject.SetActive(true);
                // currentScreen.Start.Screen();
                if(onSwitchedScreen != null)
                {
                    onSwitchedScreen.Invoke();
                }
            }
        }

        /*public void goToPreviousScreen()
        {
            if (previousScreen)
            {
                switchScreen(previousScreen);
            }
        }
        */
        public void switchImage(RawImage targetImage)
        {
            if (targetImage)
            {
                if (currentImage)
                {
                    previousImage = currentImage;
                }
                currentImage = targetImage;

                previousImage = images[2] as RawImage; 
                previousImage.gameObject.SetActive(false);

                
                currentImage.gameObject.SetActive(true);
                if(onSwitchedImage != null)
                {
                    onSwitchedImage.Invoke();
                }
            }
        }
        public void goToPreviousImage()
        {
            images = GetComponentsInChildren<RawImage>();

            
            if (previousImage)
            {
                switchImage(previousImage);
            }
        }
        
        #endregion
    }
}
