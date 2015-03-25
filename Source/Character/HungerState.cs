namespace Stronghold
{
	public enum HungerState
	{
		Hungry, //needs to eat immediately (eating is highest priority)
		Normal, //can perform other actions (eating is medium priority)
		Full, //don't need to eat (eating is lowest priority or no priority)
		JustAte //just ate
	};
}