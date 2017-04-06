using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning.Interfaces;

namespace Planning.Algorithms
{

    public class NodeList : SortedDictionary<double, SortedDictionary<int, INode>>
    {
        

        public NodeList(NodeList sd)
            : base(sd)
        {
            
        }
        public NodeList()
            : base()
        {

        }

        public INode Min
        {
            get
            {
                return base.Values.First().Values.First();//Aggregate((c, d) => c.G < d.G ? c : d);
            }
        }

        public void AddNod(double F2, INode node)
        {
            SortedDictionary<int, INode> sordic;
            if (base.TryGetValue(F2, out sordic))
            {
                sordic.Add(node.GetID(), node);
            }
            else
            {
                sordic = new SortedDictionary<int, INode>();
                sordic.Add(node.GetID(), node);
                base.Add(F2, sordic);
            }


        }



        public void RemoveNode(int keyval)
        {

            foreach (KeyValuePair<double, SortedDictionary<int, INode>> kv in this)
            {
                if (kv.Value.ContainsKey(keyval))
                {
                    kv.Value.Remove(keyval);
                    if (kv.Value.Count == 0)
                        base.Remove(kv.Key);
                    return;
                }

            }
        }

        public bool TryGetNode(int keyval, out INode node)
        {
            SortedDictionary<int, INode> sd = base.Values.FirstOrDefault(n => n.ContainsKey(keyval));

            if (sd != null)
            {
                node = sd[keyval];
                return true;
            }

            node = null;
            return false;
        }


        public void AddMoreNodes(NodeList copyfrom)
        {

            foreach (KeyValuePair<double, SortedDictionary<int, INode>> kv1 in copyfrom)
                foreach (KeyValuePair<int, INode> kv2 in kv1.Value)
                    AddNod(kv1.Key, kv2.Value);

        }

        public void Reorder(double w)
        {

            SortedDictionary<int, INode>[] list = base.Values.ToArray();
            base.Clear();
            INode node;
            for (int i = 0; i < list.Length; i++)
                foreach (KeyValuePair<int, INode> kv in list[i])
                {
                    node = kv.Value;
                    AddNod(node.G + w * node.H, node);
                }
        }


        public System.Collections.IEnumerator GetEnumerator2()
        {
           foreach(KeyValuePair<double, SortedDictionary<int,INode>> kv in this)
           foreach(KeyValuePair<int,INode> kv2 in kv.Value)
                yield return kv2.Value;            
        }

        public int Size()
        {
            int result = 0;

            foreach (KeyValuePair<double, SortedDictionary<int, INode>> kv in this)
                result += kv.Value.Count;
            return result;

        }




    }
 
    




}
     
