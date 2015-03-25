
namespace Stronghold
{
	public class CharacterAction
	{
		#region Members

		private CharacterState _action;
		private int _priority; //the lower the number, the higher the priority
		private Gametime _finishtime; //when the action should finish

		public CharacterState Action
		{
			get { return _action; }
		}

		public int Priority
		{
			get { return _priority; }
		}

		public Gametime FinishTime
		{
			get { return _finishtime; }
		}

		#endregion

		#region Constructor

		public CharacterAction()
		{
			_action = CharacterState.Undefined;
			_priority = -1;
			_finishtime = new Gametime();
		}

		public CharacterAction(CharacterState actionValue, int priorityValue, Gametime finishTimeValue)
		{
			_action = actionValue;
			_priority = priorityValue;
			_finishtime = new Gametime(finishTimeValue);
		}

		public CharacterAction(CharacterAction targetCharacterAction)
		{
			_action = targetCharacterAction.Action;
			_priority = targetCharacterAction.Priority;
			_finishtime = new Gametime(targetCharacterAction.FinishTime);
		}

		#endregion
	}
}
