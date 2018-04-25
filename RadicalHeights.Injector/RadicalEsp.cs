namespace RadicalHeights.Injector
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;

    using global::RadicalHeights.Native;

    public static class RadicalEsp
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="RadicalEsp"/> is initialized.
        /// </summary>
        public static bool Initialized
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RadicalEsp"/> is drawing.
        /// </summary>
        public static bool Drawing
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RadicalEsp"/> is searching for matches.
        /// </summary>
        public static bool Searching
        {
            get;
            private set;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            if (RadicalEsp.Initialized)
            {
                return;
            }

            // RadicalEsp.FindMatches();
            RadicalEsp.Draw();
            RadicalEsp.Initialized = true;
        }

        /// <summary>
        /// Finds matches.
        /// </summary>
        private static async Task FindMatches()
        {
            RadicalEsp.Searching = true;

            while (RadicalEsp.Searching)
            {
                await Task.Run(() => GameScreen.SearchMatchingPixels());;
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        private static async Task Draw()
        {
            RadicalEsp.Drawing = true;

            while (RadicalEsp.Drawing)
            {
                await Task.Run(() => RadicalEsp.OnRender());
            }
        }

        /// <summary>
        /// Called when we have to render stuff.
        /// </summary>
        private static void OnRender()
        {
            // Logging.Warning(typeof(RadicalEsp), "OnRender()");

            // ...

            // var Pixels   = GameScreen.GetGamePixels();
            var HandleDc = GameScreen.GetDC(IntPtr.Zero);
            // var Matches  = GameScreen.MatchingPixels;

            // if (Matches == null)
            // {
            //     return;
            // }

            if (RadicalHeights.IsOnScreen == false)
            {
                return;
            }

            using (Graphics Graphics = Graphics.FromHdc(HandleDc))
            {
                /* foreach (Point Point in Matches)
                {
                    Graphics.FillRectangle(new SolidBrush(Color.Black), Point.X, Point.Y, 3, 3);
                } */

                Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 300, 30);
            }

            GameScreen.ReleaseDC(IntPtr.Zero, HandleDc);
        }
    }
}
