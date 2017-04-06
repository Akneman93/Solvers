using Solvers.Interfaces;
using System;
namespace Solvers.Algorithms
{
    public class DefaultOperator : IOperator
	{
		private string name = "None";
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public DefaultOperator(string name)
		{
			this.name = name;
		}

		public DefaultOperator()
		{
		}

		override public bool Equals(Object obj)
		{
			var op = obj as DefaultOperator;
			if (obj == null)
				return false;
			return op.Name.Equals(this.Name);
		}



		override public int GetHashCode()
		{
			return this.Name.GetHashCode();
		}



	}
}
