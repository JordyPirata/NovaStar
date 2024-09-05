using System;
using UnityEngine.Events;

public interface IFadeController
{
    public void FadeOut(Action onFadeCompleted = null);
    void FadeIn(Action action = null);
}