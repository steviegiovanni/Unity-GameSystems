﻿using RPGSystems.StatSystem;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// prerequisite of using a skill (e.g. need a certain amount of mp, or energy)
	/// </summary>
	public class SkillPrerequisite {
		/// <summary>
		/// The stat name for the prerequisite
		/// </summary>
		private RPGStatType _statName;
		public RPGStatType StatName{
			get{ return _statName;}
			set{ _statName = value;}
		}

		/// <summary>
		/// the value needed
		/// </summary>
		private int _statValue;
		public int StatValue{
			get{ return _statValue;}
			set{ _statValue = value;}
		}

		/// <summary>
		/// constructor
		/// </summary>
		public SkillPrerequisite(SkillPrerequisiteAsset asset){
			StatName = (RPGStatType)asset.StatName;
			StatValue = asset.StatValue;
		}

		/// <summary>
		/// called when a skill is used on the stat owner
		/// </summary>
		public void ApplyPrerequisite(IHasStats affected){
			affected.ModifyStat (StatName, -StatValue);
		}

		/// <summary>
		/// check whether this prerequisite is met
		/// </summary>
		public bool CheckPrerequisite(IHasStats statOwner){
			float statValue = 0;
			if(statOwner.TryGetStatCurrentValue(StatName,out statValue)){
			//if(statOwner.TryGetStatValue("health",out statValue)){
				if (statValue >= StatValue)
					return true;
				else
					return false;
			}else
				return false;
		}
	}
}