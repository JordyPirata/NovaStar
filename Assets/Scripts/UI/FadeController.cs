using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class FadeController : MonoBehaviour, IFadeController
    {
        [SerializeField] private Image image;
        [SerializeField] private float fadeDuration;
        public void FadeIn(Action onFadeCompleted = null)
        {
            var sequence = DOTween.Sequence();
            sequence.Insert(0, image.DOFade(1, fadeDuration));
            if (onFadeCompleted != null)
            {
                sequence.onComplete = () => { onFadeCompleted.Invoke(); };
            }
        }

        public void FadeOut(Action onFadeCompleted = null)
        {
            var sequence = DOTween.Sequence();
            sequence.Insert(0, image.DOFade(0, fadeDuration));
            if (onFadeCompleted != null)
            {
                sequence.onComplete = () => { onFadeCompleted.Invoke(); };
            }
        }
    }
}