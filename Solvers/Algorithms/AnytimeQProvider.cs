
using Planning.Interfaces;

using WindowsFormsApplication1;
namespace Planning.Algorithms
{
	public class AnytimeQProvider:IQProvider
	{
		private baseA A;
		public AnytimeQProvider(baseA A, double TimeForSearch)
		{
			this.A = A;
		}

		public double getQ(IState state, IOperator op)
		{
			
			INode node = path.Find((INode obj) => obj.State.Equals(state));
			return node.G + node.H;
		
		}

	}
}
