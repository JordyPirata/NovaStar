using System;
using UnityEngine.Events;
namespace Services.Interfaces
{
public interface IFadeController
{
    public void FadeOut(Action onFadeCompleted = null);
    void FadeIn(Action action = null);
}
}