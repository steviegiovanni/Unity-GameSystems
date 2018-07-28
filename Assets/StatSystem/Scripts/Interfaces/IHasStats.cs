namespace RPGSystems.StatSystem{
	/// <summary>
	/// an entity implementing this interface should have the following
	/// - a function that returns the stat value in percent
	/// </summary>
	public interface IHasStats {
		bool TryGetStatPercentValue (RPGStatType statName, out float value); 
		bool TryGetStatValue (RPGStatType statName, out int value);
		bool TryGetStatCurrentValue (RPGStatType statName, out float value);
		void ModifyStat (RPGStatType statName, float value);
		void ModifyStat (RPGStatType sourceStat, RPGStatType targetStat, float modifier, float flatValue, float baseValue);
	}
}
