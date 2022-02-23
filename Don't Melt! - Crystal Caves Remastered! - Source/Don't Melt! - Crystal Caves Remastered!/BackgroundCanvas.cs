using System;
using EpsilonEngine;
namespace DMCCR
{
    public sealed class BackgroundCanvas : Canvas
    {
        private float _gradientProgress = 0;
        private Image _backgroundImage = null;
        public BackgroundCanvas(DMCCR dontmelt, Texture backgroundTexture) : base(dontmelt)
        {
            _backgroundImage = new Image(this, backgroundTexture);
            _backgroundImage.LocalMinX = 0.0f;
            _backgroundImage.LocalMinY = 0.0f;
            _backgroundImage.LocalMaxX = 1.0f;
            _backgroundImage.LocalMaxY = 1.0f;
        }
        protected override void Update()
        {
            _gradientProgress += 0.001f;
            if (_gradientProgress > 1)
            {
                _gradientProgress--;
            }
            _backgroundImage.Color = ColorHelper.SampleHueGradient(_gradientProgress, 100);
        }
    }
}
