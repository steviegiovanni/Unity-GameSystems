namespace RPGSystems.StatSystem{
	/// <summary>
	/// an entity implementing this interface should have the following
	/// - a function that returns the stat value in percent
	/// </summary>
	public interface IHasStats {
		float GetStatPercentValue (RPGStatType statName);
		bool TryGetStatPercentValue (RPGStatType statName, out float value); // a way to return false if stat doesn't exist
		void ModifyStat(RPGStatType statName,float modifier, int flatValue, int baseValue);
		void ModifyStat (RPGStatType statName, int value);
		int GetStatValue (RPGStatType statName);
		bool TryGetStatValue (RPGStatType statName, out int value);
	}
}
