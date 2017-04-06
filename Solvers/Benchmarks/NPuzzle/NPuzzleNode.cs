using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning.Interfaces;

namespace Planning.NPuzzle
{
    public class NPuzzleNode : INode
    {
        
        private int level;
        private IOperator usedOperator;

        public int Level
        {
            set
            {
                level = value;
            }
            get
            {
                return level;
            }
        }

        public NPuzzleNode()
        {           
            level = 0;
			usedOperator = new DefaultOperator();
        }

        

        #region INode Members
               
        public double G { get; set; }
        public double H { get; set; }

        public INode Parent { get; set; }


        public int GetID()
        {
            return string.Join("", Tiles).GetHashCode();
            /*int finalscore = 0;
            for (int i = 0; i < Tiles.Length; i++)
                finalscore += Tiles[i] * Convert.ToInt32(Math.Pow(10, Tiles.Length - i - 1));
            return finalscore;*/
        }


		override public bool Equals(Object obj)
		{
			var node = obj as NPuzzleNode;

			return node != null && Tiles.SequenceEqual(node.Tiles);
		}

		override public int GetHashCode()
		{
			return string.Join("", Tiles).GetHashCode();
		}








		public bool Equals(INode node)
        {
            var testNode = node as NPuzzleNode;            

            return testNode != null && Tiles.SequenceEqual(testNode.Tiles);
        }

        public IOperator UsedOperator
        {
            get
            {
                return usedOperator;
            }

            set
            {
                usedOperator = value;
            }

        }


        #endregion

       







    }
}
