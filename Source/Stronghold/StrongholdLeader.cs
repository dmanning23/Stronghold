
namespace Stronghold
{
	public class StrongholdLeader : Character
	{
		#region Members

		public DecisionMaker _decisionmaker;

		#endregion

		#region Constructor

		public StrongholdLeader()
			: base()
		{
			_decisionmaker = new DecisionMaker();
		}

		#endregion
	}
}
