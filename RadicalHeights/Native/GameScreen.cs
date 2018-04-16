namespace RadicalHeights.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public static class GameScreen
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr Handle);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr Handle, IntPtr HandleDc);

        /// <summary>
        /// Gets the matching pixels.
        /// </summary>
        public static List<Point> MatchingPixels
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the game screen pixels.
        /// </summary>
        public static Bitmap GetGamePixels()
        {
            if (RadicalHeights.IsAttached == false)
            {
                throw new Exception("Radical Heights is not attached at GameScreen.GetScreenPixels().");
            }

            var Game    = RadicalHeights.WindowRec;
            var Pixels  = new Bitmap(Game.Width, Game.Height);
            var Drawing = Graphics.FromImage(Pixels);
            
            using (Drawing)
            {
                Drawing.CopyFromScreen(Game.Left, Game.Top, 0, 0, new Size(Game.Width, Game.Height));
            }

            return Pixels;
        }

        /// <summary>
        /// Searches for matching pixels.
        /// </summary>
        public static void SearchMatchingPixels()
        {
            const int GreenRMax = 230;
            const int GreenRMin = 130;

            const int GreenGMax = 255;
            const int GreenGMin = 200;

            const int GreenBMax = 30;
            const int GreenBMin = 0;

            var Pixels          = GameScreen.GetGamePixels();
            var Matching        = new List<Point>();

            for (int x = 0; x < Pixels.Width; x++)
            {
                for (int y = 0; y < Pixels.Height; y++)
                {
                    var Pixel = Pixels.GetPixel(x, y);

                    if (Pixel.IsEmpty)
                    {
                        continue;
                    }

                    if ((Pixel.R >= GreenRMin && Pixel.R <= GreenRMax) == false)
                    {
                        continue;
                    }

                    if ((Pixel.G >= GreenGMin && Pixel.G <= GreenGMax) == false)
                    {
                        continue;
                    }

                    if ((Pixel.B >= GreenBMin && Pixel.B <= GreenBMax) == false)
                    {
                        continue;
                    }

                    // Logging.Info(typeof(RadicalHeights), "Pixel : " + Pixel.R + ", " + Pixel.G + ", " + Pixel.B);

                    Matching.Add(new Point(x, y));
                }
            }

            GameScreen.MatchingPixels = Matching;
        }
    }
}
