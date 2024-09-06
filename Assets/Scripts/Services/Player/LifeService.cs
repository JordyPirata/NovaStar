using System.Collections;
using Services.Interfaces;
using UnityEngine;
public class LifeService : StatService, ILifeService
{
    public int Life { get => Stat; set => Stat = value; }

}
