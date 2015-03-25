
namespace Stronghold
{
	public class Treasury
	{
		#region Members

		private int _gold;

		public int Gold
		{
			get { return _gold; }
		}

		#endregion

		#region Constructor

		public Treasury()
		{
		}

		public Treasury(int startingGold)
		{
			_gold = startingGold;
		}

		#endregion

		#region Methods

		public void depositGold(int amount)
		{
			_gold += amount;
		}

		public void withdrawGold(int amount)
		{
			_gold -= amount;
		}

		public bool haveEnoughToWithdraw(int amount)
		{
			return (amount <= _gold);
		}

		#endregion
	}
}
