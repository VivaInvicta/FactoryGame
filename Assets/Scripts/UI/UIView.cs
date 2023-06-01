using System;
using UnityEngine;

namespace FactoryGame
{
    public abstract class UIView : MonoBehaviour
    {
        public event Action OnClose;
        
        public void SetInteractable(bool interactable)
        {
            SetInteractableInternal(interactable);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }

        protected virtual void SetInteractableInternal(bool interactable) { }
    }
}