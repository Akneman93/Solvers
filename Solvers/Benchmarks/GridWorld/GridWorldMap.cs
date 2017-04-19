using System;
using System.Drawing;

namespace Solvers.GridWorldBench
{
    public class GridWorldMap
    {

        public static Color AgentPointColor = Color.Red;
        public static Color GoalPointColor = Color.Green;
        public static Color FreePointColor = Color.White;
        public static Color BlockColor = Color.Black;




        private readonly Bitmap bitmap;

        public Bitmap BitMap
        {
            get
            {
                return bitmap;
            }
        }

        public GridWorldMap(Bitmap BitMap)
        {
            bitmap = new Bitmap(BitMap);
        }

        public GridWorldMap(GridWorldMap gwMap)
        {
            bitmap = new Bitmap(gwMap.BitMap);

        }

        public GridWorldMap(int Width, int Length)
        {
            bitmap = new Bitmap(Width, Length);
            for (int i = 0; i < bitmap.Height; i++)
                for (int j = 0; j < bitmap.Width; j++)
                    bitmap.SetPixel(j, i, FreePointColor);
        }

        public void CreateBlocks(int blockSize)
        {
            Random random = new Random();
            for (int i = 0; i < bitmap.Height / blockSize; i++)
                for (int j = 0; j < bitmap.Width / blockSize; j++)
                    if (random.Next(3) == 0)
                    {
                        DrawBlock(j * blockSize, i * blockSize, BlockColor, blockSize);
                    }

        }

        public void DrawBlock(int X, int Y, Color color, int blockSize)
        {
            for (int k = 0; k < blockSize; k++)
                for (int q = 0; q < blockSize; q++)
                {
                    bitmap.SetPixel(X + k, Y + q, color);
                }
        }

        public bool isUpFree(int X, int Y, int blockSize)
        {
            if (Y - blockSize < 0)
                return false;
            Color pixelColor = bitmap.GetPixel(X, Y - blockSize);
            return pixelColor.ToArgb() == FreePointColor.ToArgb() || pixelColor.ToArgb() == GoalPointColor.ToArgb() 
                || pixelColor.ToArgb() == AgentPointColor.ToArgb();
        }

        public bool isDownFree(int X, int Y, int blockSize)
        {
            if (Y + blockSize >= bitmap.Height)
                return false;
            Color pixelColor = bitmap.GetPixel(X, Y + blockSize);
            return pixelColor.ToArgb() == FreePointColor.ToArgb() || pixelColor.ToArgb() == GoalPointColor.ToArgb()
                 || pixelColor.ToArgb() == AgentPointColor.ToArgb();
        }

        public bool isLeftFree(int X, int Y, int blockSize)
        {
            if (X - blockSize < 0)
                return false;
            Color pixelColor = bitmap.GetPixel(X - blockSize, Y);
            return pixelColor.ToArgb() == FreePointColor.ToArgb() || pixelColor.ToArgb() == GoalPointColor.ToArgb()
                 || pixelColor.ToArgb() == AgentPointColor.ToArgb();
        }

        public bool isRightFree(int X, int Y, int blockSize)
        {
            if (X + blockSize >= bitmap.Width)
                return false;
            Color pixelColor = bitmap.GetPixel(X + blockSize, Y);
            return pixelColor.ToArgb() == FreePointColor.ToArgb() || pixelColor.ToArgb() == GoalPointColor.ToArgb()
                 || pixelColor.ToArgb() == AgentPointColor.ToArgb();
        }

         
    }
}
