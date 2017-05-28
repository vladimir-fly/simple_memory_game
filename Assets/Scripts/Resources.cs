using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SMG
{
    public class Resources : MonoBehaviour
    {
        [SerializeField] private List<RawImage> _cardImages;
        public RawImage GetCardImage(ECardType cardType)
        {
            return _cardImages[(int) cardType];
        }
    }
}