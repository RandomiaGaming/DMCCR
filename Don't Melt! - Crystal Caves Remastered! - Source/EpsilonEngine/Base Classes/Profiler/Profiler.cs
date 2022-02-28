using System;
namespace EpsilonEngine
{
    internal static class Profiler
    {
        #region Private Variables
        private static long _updateStart = 0;
        private static long _updateEnd = 0;

        private static long _renderStart = 0;
        private static long _renderEnd = 0;

        private static long _lastPrintTime = 0;

        private static System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
        #endregion
        #region Constructors
        static Profiler()
        {
            _stopWatch.Restart();
        }
        #endregion
        #region Internal Methods
        internal static void UpdateStart()
        {
            _updateStart = _stopWatch.ElapsedTicks;
        }
        internal static void UpdateEnd()
        {
            _updateEnd = _stopWatch.ElapsedTicks;
        }

        internal static void RenderStart()
        {
            _renderStart = _stopWatch.ElapsedTicks;
        }
        internal static void RenderEnd()
        {
            _renderEnd = _stopWatch.ElapsedTicks;
        }

        internal static void Print()
        {
            long currentTime = _stopWatch.ElapsedTicks;

            long updateTime = _updateEnd - _updateStart;

            long renderTime = _renderEnd - _renderStart;

            long frameTime = currentTime - _lastPrintTime;

            if (frameTime <= 0)
            {
                Console.WriteLine($"Debug Profiler - Infinity FPS - {frameTime} Tick Frame - {frameTime - updateTime - renderTime} Tick MonoGame Update - {updateTime} Tick Update - {renderTime} Tick Render.");
                return;
            }

            Console.WriteLine($"Debug Profiler - {10000000 / frameTime} FPS - {frameTime} Tick Frame - {frameTime - updateTime - renderTime} Tick MonoGame Update - {updateTime} Tick Update - {renderTime} Tick Render.");

            _lastPrintTime = _stopWatch.ElapsedTicks;
        }
        #endregion
    }
}