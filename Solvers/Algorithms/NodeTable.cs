using Solvers.Algorithms.Astar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solvers.Algorithms
{

    /* public class NodeTable<TKey1, TKey2, TValue> : SortedDictionary<TKey1, SortedDictionary<TKey2, List<TValue>>>
     {

         public long NumberOfElements = 0;

         //private Dictionary<TValue, List<TValue>> values = new Dictionary<TValue, List<TValue>>();



         public NodeTable(NodeTable<TKey1, TKey2, TValue> sd)
             : base(sd)
         {

         }
         public NodeTable()
             : base()
         {

         }

         public TValue Min
         {
             get
             {
                 return base.Values.First().Values.First().First();
             }
         }

         public void AddValue(TKey1 key1, TKey2 key2,  TValue value)
         {
             TValue out_value;
             if (!TryGetValue(value, out out_value))
             {
                 //values.Add(value, value);
                 NumberOfElements++;

             }



             SortedDictionary<TKey2, List<TValue>> dic2;
             List<TValue> set;
             if (base.TryGetValue(key1, out dic2))
             {
                 if (dic2.TryGetValue(key2, out set))
                 {
                     set.Add(value);
                     NumberOfElements++;

                 }
                 else
                 {
                     set = new List<TValue>();
                     set.Add(value);
                     NumberOfElements++;
                     dic2.Add(key2, set);
                 }                
             }
             else
             {

                 dic2 = new SortedDictionary<TKey2, List<TValue>>();
                 set = new List<TValue>();
                 set.Add(value);
                 NumberOfElements++;
                 dic2.Add(key2, set);
                 this.Add(key1, dic2);
             }

         }


         public void AddValues(NodeTable<TKey1, TKey2, TValue> table)
         {
             foreach (KeyValuePair<TKey1, SortedDictionary<TKey2, List<TValue>>> pair1 in table)
                 foreach (KeyValuePair<TKey2, List<TValue>> pair2 in pair1.Value)
                     foreach (TValue value1 in pair2.Value)
                     {
                         TValue out_value;
                         if (!TryGetValue(value1, out out_value))
                         {
                             AddValue(pair1.Key, pair2.Key, value1);
                         }

                     }
         }






         public void RemoveValue(TKey1 key1, TKey2 key2, TValue value)
         {
             SortedDictionary<TKey2, List<TValue>> dic2;
             List<TValue> set;
             if (base.TryGetValue(key1, out dic2))
             {
                 if (dic2.TryGetValue(key2, out set))
                 {
                     set.Remove(value);
                     //values.Remove(value);
                     if (set.Count() == 0)
                         dic2.Remove(key2);
                     if (dic2.Count() == 0)
                         this.Remove(key1);
                     NumberOfElements--;
                 }
             }
         }


         public bool TryGetValue(TValue value, out TValue value2)
         {
             bool found;
             if (values.TryGetValue(value, out value2))
             {
                 found = true;
             }
             else
             {
                 value2 = default(TValue);
                 found = false;
             }
             return found;

         }






        // public System.Collections.IEnumerator GetEnumerator2()
       //  {
         //	return values.Values.GetEnumerator();            
         }

       //  public int Size()
        // {
         //	return values.Count;

       //  }




     }*/



    /*public class NodeTable<TNode> : Dictionary<TNode, List<TNode>> 

    {

        public long NumberOfElements = 0;      


        IComparer<TNode> comparer = null;



        public NodeTable(NodeTable<TNode> sd, IComparer<TNode> comparer)
            : base(sd)
        {           
            this.comparer = comparer;
        }

        public NodeTable(IComparer<TNode> comparer)
           : base()
        {         
            this.comparer = comparer;
        }

       



       

        public TNode Min
        {
            get
            {


                  List < TNode > firstNodes = new List<TNode>();

                  TNode currNode = this.Values.FirstOrDefault().FirstOrDefault();
                  int i = 0;

                  foreach (List<TNode> list in this.Values)
                  {
                      if (comparer.Compare(currNode, list[0]) == 1)
                          currNode = list[0];
                      i++;                   
                  }

                  return currNode;

               
            }
        }

        public void AddValue(TNode node)
        {
            List<TNode> list;

            if (this.TryGetValue(node, out list))
            {
                list.Add(node);
                list.Sort(comparer);
            }

            else
            {
                list = new List<TNode>();
                list.Add(node);
                this.Add(node, list);
            }
           

            NumberOfElements++;


         

        }


        public void AddValues(NodeTable<TNode> table)
        {
            foreach (KeyValuePair<TNode, List<TNode>> pair1 in table)
                foreach (TNode node in pair1.Value)
                {
                    this.AddValue(node);
                }

        }


        public void RemoveValue(TNode node, Predicate<TNode> pred)
        {
            List<TNode> list;

            if (base.TryGetValue(node, out list))
            {
                NumberOfElements -= list.RemoveAll(pred);
                if (list.Count == 0)
                    this.Remove(node);
            }




        }


        public void RemoveAllNodes(Predicate<TNode> pred)
        {
            //List<TNode> list;

            foreach(TNode node in this.Keys.ToList())
            {
                NumberOfElements -= this[node].RemoveAll(pred);
                if (this[node].Count == 0)
                    this.Remove(node);
            }


            
        }




        public bool TryGetValue(TNode node, Predicate<TNode> pred, out TNode node2)
        {
            
            List<TNode> list;
            int index;
            if (this.TryGetValue(node, out list))
            {
                index = list.FindIndex(pred);
                if (index == -1)
                {
                    node2 = default(TNode);
                    return false;
                }

                node2 = list[index];
                return true;
            }

            node2 = default(TNode);
            return false;
        }


    }*/



    public class NodeTable<TNode> : SortedList<TNode, List<TNode>> where TNode: ANode

    {

        public long NumberOfElements = 0;


        IComparer<TNode> comparer = null;


        Dictionary<TNode, List<TNode>> nodesLists = new Dictionary<TNode, List<TNode>>();



        public NodeTable(NodeTable<TNode> sd, IComparer<TNode> comparer)
            : base(sd, comparer)
        {
            this.comparer = comparer;
        }

        public NodeTable(IComparer<TNode> comparer)
           : base(comparer)
        {
            this.comparer = comparer;
        }




        public TNode Min
        {
            get
            {

                return this[Keys[0]].First(); ;
            }
        }

        public void AddValue(TNode node)
        {
            List<TNode> list;

            if (this.TryGetValue(node, out list))
            {
                list.Add(node);                
            }

            else
            {
                list = new List<TNode>();
                list.Add(node);
                this.Add(node, list);
            }



            if (nodesLists.TryGetValue(node, out list))
            {
                list.Add(node);
            }

            else
            {
                list = new List<TNode>();
                list.Add(node);
                nodesLists.Add(node, list);
            }

            NumberOfElements++;




        }


        public void AddValues(NodeTable<TNode> table)
        {
            foreach (KeyValuePair<TNode, List<TNode>> pair1 in table)
                foreach (TNode node in pair1.Value)
                {
                    this.AddValue(node);
                }

        }


        public void RemoveValue(TNode node)
        {
            

            foreach (List<TNode> list in Values.ToList())
            {
                for (int i = 0; i < list.Count; i++)
                    if (list[i] == node)
                    {
                        list.RemoveAt(i);
                        if (list.Count == 0)
                            this.Remove(node);
                        return;
                    }
                
            }

            List<TNode> list2;
            if (nodesLists.TryGetValue(node, out list2))
            {
                for (int i = 0; i < list2.Count; i++)
                    if (list2[i] == node)
                    {
                        list2.RemoveAt(i);
                        if (list2.Count == 0)
                            nodesLists.Remove(node);
                        return;
                    }
            }

        }


        




        public bool TryGetValue(TNode node, out TNode node2)
        {

            /*  foreach (List<TNode> list in Values.ToList())
              {
                  for (int i = 0; i < list.Count; i++)
                      if (list[i].Equals(node))
                      {
                          node2 = list[i];
                          return true;
                      }

              }

              node2 = default(TNode);
              return false;
              */

            List<TNode> list;
            if (nodesLists.TryGetValue(node, out list))
            {
                node2 = list[0];
                return true;
            }

            node2 = default(TNode);
            return false;




        }


    }











}
     
