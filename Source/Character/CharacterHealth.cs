
namespace Stronghold
{
	public class CharacterHealth
	{
		#region Members

		public struct status
		{
			private int _current; //current value
			private int _max; //max value
			private int _regeneration; //how much is regenerated over 1 game tick                       
			private HealthState _healthState; //health state of the person

			public int Current
			{
				get { return _current; }
				set
				{
					_healthState = HealthState.Alive;
					_current = value;
					if (_current > _max)
					{
						_current = _max;
					}
					if (_current < 0)
					{
						_current = 0;
						_healthState = HealthState.Dead;
					}
				}
			}

			public int Max
			{
				get { return _max; }
				set { _max = value; }
			}

			public int Regeneration
			{
				get { return _regeneration; }
				set { _regeneration = value; }
			}

			public HealthState HealthState
			{
				get { return _healthState; }
			}
		}

		public status hp;
		public status mp;
		public status stamina;
		public int staminaUsedThisTick;

		#endregion

		#region Constructor

		public CharacterHealth()
		{
			hp.Current = 0;
			hp.Max = 0;
			hp.Regeneration = 0;
			mp.Current = 0;
			mp.Max = 0;
			mp.Regeneration = 0;
			stamina.Current = 0;
			stamina.Max = 0;
			stamina.Regeneration = 0;
			staminaUsedThisTick = 0;
		}

		#endregion

		#region Methods

		public void defineHP(int value, int regenValue)
		{
			hp.Max = value;
			hp.Current = value;
			hp.Regeneration = regenValue;
		}

		public void defineMP(int value, int regenValue)
		{
			mp.Max = value;
			mp.Current = value;
			mp.Regeneration = regenValue;
		}

		public void defineStamina(int value, int regenValue)
		{
			stamina.Max = value;
			stamina.Current = value;
			stamina.Regeneration = regenValue;
		}

		public string getCharacterHealthString()
		{
			string result = "";
			result += "HP: " + this.hp.Current + "/" + this.hp.Max + " (" + this.hp.Regeneration + ") \n";
			result += "MP: " + this.mp.Current + "/" + this.mp.Max + " (" + this.mp.Regeneration + ") \n";
			result += "Stamina: " + this.stamina.Current + "/" + this.stamina.Max + " (" + this.stamina.Regeneration + ") \n";
			result += "Stamina used this tick: " + this.staminaUsedThisTick + "\n";
			result += "Health state: " + this.hp.HealthState + "\n";
			return result;
		}

		#endregion
	}//character health related
}
