using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    public class ResourceDisplayerView : UIView
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private Text countText;

        public void SetItemImage(Sprite image)
        {
            itemImage.sprite = image;
        }

        public void SetCount(int count)
        {
            countText.text = count.ToString();
        }
    }
}