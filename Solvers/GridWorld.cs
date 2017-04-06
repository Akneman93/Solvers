using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1.GridWorld
{
    public class GridWorld
    {
        private readonly Bitmap bitmap;
        public Bitmap BitMap
        {
            get
            {
                return bitmap;
            }
        }

        public GridWorld(Bitmap BitMap)
        {
            bitmap = BitMap;
        }

        public GridWorld(int Width, int Length)
        {
            bitmap = new Bitmap(Width, Length);
            for (int i = 0; i < bitmap.Height; i++)
                for (int j = 0; j < bitmap.Width; j++)
                    bitmap.SetPixel(j, i, Color.White);
        }

        public void CreateBlocks(int blockSize)
        {
            Random random = new Random();
            for (int i = 0; i < bitmap.Height; i++)
                for (int j = 0; j < bitmap.Width; j++)
                    if (i + blockSize < bitmap.Height && j + blockSize < bitmap.Width && random.Next(2) == 0 
                        && i % blockSize == 0 && j % blockSize == 0)
                    {
                        for (int k = 0; k < blockSize; k++)
                        {
                            bitmap.SetPixel(j + k, i + k, Color.Black);
                        }
                    }
        }
    
    }
}
