using Services.Interfaces;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Services
{
    public class TextureMapGen
    {
        private readonly INoiseService noiseService = ServiceLocator.GetService<INoiseService>();
        private readonly IBiomeDic biomeDic = ServiceLocator.GetService<IBiomeDic>();
        public Texture2D Texture2D { get; private set; }

        public void Init ()
        {
            Texture2D = new Texture2D(512, 512)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };
            Texture2D.Apply();
        }
    }
}
