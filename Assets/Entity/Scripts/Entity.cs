using System.Collections;
using GameSystems.GambitSystem;
using GameSystems.LevelSystem;
using GameSystems.Movement;
using GameSystems.PerceptionSystem;
using GameSystems.SkillSystem;
using UnityEngine;
using UnityEngine.AI;
using GameSystems.Entities.Database;
using RPGSystems.StatSystem;

namespace GameSystems.Entities{
	/// <summary>
	/// Entity class. Each instantiated unit is an entity.
	/// An entity implements the followings:
	/// IPerceivable   : lets the entity be seen by the perception component
	/// IMovable       : Defines the entity's ability to move
	/// IHasPerception : Specifies that the entity has a perception component
	/// IHasStats      : Specifies that the entity has a stat component
	/// IHasLevel      : Specifies that the entity has a levelling component
	/// </summary>
	[System.Serializable]
	public class Entity : MonoBehaviour, IPerceivable, IMovable, IHasPerception, IHasStats, IHasLevel {
		#region IHasStats implementation
		public bool TryGetStatValue(RPGStatType statName, out int value){
			RPGStat stat = null;
			value = 0;
			if (StatCollection.TryGetStat (statName, out stat)) {
				value = stat.StatValue;
				return true;
			}
			return false;
		}

		public bool TryGetStatCurrentValue (RPGStatType statName, out float value){
			RPGVital stat = null;
			value = 0;
			if (StatCollection.TryGetStat<RPGVital> (statName, out stat)) {
				value = stat.StatValueCurrent;
				return true;
			}
			return false;
		}

		public bool TryGetStatPercentValue (RPGStatType statName, out float value){
			RPGVital stat = null;
			value = 0;
			if (StatCollection.TryGetStat<RPGVital> (statName, out stat)) {
				value = stat.StatValueCurrent/stat.StatValue;
				return true;
			}
			return false;
		}

		public void ModifyStat(RPGStatType sourceStat, RPGStatType targetStat, float modifier, float flatValue, float baseValue){
			float rawValue = flatValue + modifier * baseValue;
			float bufferValue = 1.0f;
			if (sourceStat == RPGStatType.Power) {
				RPGStat stat = null;
				if (StatCollection.TryGetStat (RPGStatType.Defense, out stat)) {
					bufferValue = stat.StatValue;
				}
			}else if(sourceStat == RPGStatType.Mind){
				RPGStat stat = null;
				if (StatCollection.TryGetStat (RPGStatType.Spirit, out stat)) {
					bufferValue = stat.StatValue;
				}
			}

			ModifyStat (targetStat, (int)((rawValue < 0) ? (-1) : (1) * rawValue * rawValue / bufferValue));
		}

		public void ModifyStat(RPGStatType statName,float value){
			RPGVital stat = null;
			if (StatCollection.TryGetStat<RPGVital> (statName, out stat)) {
				stat.StatValueCurrent += value;
			}
		}

		#endregion


		#region IHasLevel implementation
		/// <summary>
		/// level of this entity
		/// </summary>
		private RPGEntityLevel _rpgEntityLevel;
		public RPGEntityLevel EntityLevel{
			get{
				if (_rpgEntityLevel == null) 
					_rpgEntityLevel = GetComponent<RPGEntityLevel> ();
				return _rpgEntityLevel;
			}
		}

		public int GetLevel ()
		{
			if (EntityLevel == null)
				return 1000;
			else
				return EntityLevel.Level;
		}

		#endregion

		#region IHasPerception implementation

		/// <summary>
		/// the perception component. stores all seen entity along with their enmity
		/// </summary>
		[SerializeField]
		private Perception _perception;
		public Perception Perception{
			get{
				if (_perception == null)
					_perception = GetComponent<Perception> ();
				if (_perception == null)
					_perception = this.gameObject.AddComponent<Perception> ();
				return _perception;
			}
		}

		#endregion

		#region IPerceivable implementation
		/// <summary>
		/// the tag of the entity (e.g. none, ally, or enemy)
		/// </summary>
		[SerializeField]
		private PerceptionTags _tag;
		public int Tag {
			get{ return (int)_tag;}
			set{ _tag = (PerceptionTags)value;}
		}
		#endregion

		#region IMovable implementation
		/// <summary>
		/// movement component of an entity, using navmesh for this example
		/// </summary>
		[SerializeField]
		private NavMeshAgent _movementComp;

		public NavMeshAgent MovementComponent{
			get{ 
				if (_movementComp == null) 
					_movementComp = GetComponent<NavMeshAgent> ();
				if (_movementComp == null)
					_movementComp = this.gameObject.AddComponent<NavMeshAgent> ();
				return _movementComp;
			}
		}

		/// <summary>
		/// used by a gambit to move entities to a target position
		/// </summary>
		public void MoveTo (Vector3 targetPos)
		{
			MovementComponent.SetDestination (targetPos);
		}

		/// <summary>
		/// used by a gambit to tell the entity to stop moving
		/// </summary>
		public void StopMove ()
		{
			MovementComponent.isStopped = true;
		}

		public bool Stopped{
			get{ return MovementComponent.isStopped; }
			set{ MovementComponent.isStopped = value;}
		}

		/// <summary>
		/// used by a gambit in its coroutine to query whether entity is close enough 
		/// to a target position
		/// </summary>
		public float RemainingDistance (Vector3 targetPos)
		{
			if (!MovementComponent.hasPath || (MovementComponent.destination != targetPos)) 
				return Vector3.Distance(this.transform.position,targetPos);

			return MovementComponent.remainingDistance;
		}
		#endregion

		/// <summary>
		/// a reference to a gambit collection associated to a this entity
		/// </summary>
		[SerializeField]
		private GambitCollection _gambitCollection;
		public GambitCollection GambitCollection{
			get{ 
				if(_gambitCollection == null)
					_gambitCollection = GetComponent<GambitCollection> ();
				return _gambitCollection;
			}
		}

		/// <summary>
		/// a reference to a skill collection associated to a this entity
		/// </summary>
		[SerializeField]
		private SkillCollection _skillCollection;
		public SkillCollection SkillCollection{
			get{ 
				if(_skillCollection == null)
					_skillCollection = GetComponent<SkillCollection> ();
				return _skillCollection;
			}
		}

		/// <summary>
		/// a reference to a stat collection associated to a this entity
		/// </summary>
		[SerializeField]
		private RPGStatCollection _statCollection;
		public RPGStatCollection StatCollection{
			get{ 
				if(_statCollection == null)
					_statCollection = GetComponent<RPGStatCollection> ();
				return _statCollection;
			}
		}

		/// <summary>
		/// whether the entity is being controlled by player (true) or AI (false) 
		/// </summary>
		private bool _controlled = false;
		public bool Controlled{
			get{ return _controlled;}
			set{ _controlled = value;}
		}
			
		// Use this for initialization
		void Start () {
			// associate functions to be called when perception becomes either alerted or unalerted
			if (Perception != null) {
				Perception.OnAlerted.AddListener (OnAlerted);
				Perception.OnUnalerted.AddListener (OnUnalerted);
			}
			if (StatCollection != null)
				StatCollection.SetupCollection ();
		}
		
		// Update is called once per frame
		void Update () {
			// make every entity broadcasts an event to the other entities every frame
			PerceptionEVManager.TriggerEvent ("PERCEPTION", new Hashtable (){ { "OBJECT",this.gameObject } });

			// every update, if the entity is not being controlled, check for perception component alerted or not
			if (!Controlled && Perception != null && GambitCollection != null) {
				// enable the gambit if gambit is off and is alerted
				if (Perception.Alerted && !GambitCollection.enabled)
					GambitCollection.enabled = true;
			}

			// test levelling
			if (Input.GetKeyUp (KeyCode.Space)) {
				if (EntityLevel != null)
					EntityLevel.ModifyExp (50);
			}
		}

		/// <summary>
		/// when perception is alerted, enable gambit
		/// </summary>
		void OnAlerted(){
			// enable gambit if the entity is not being commanded by player
			if (!Controlled) {
				if (GambitCollection != null)
					GambitCollection.enabled = true;
			}
		}

		/// <summary>
		/// when perception is unalerted, disable gambit
		/// </summary>
		void OnUnalerted(){
			// disable gambit if the entity is not being commanded by player
			// if it is, then the gambit must've been disabled when the entity was given the command
			if (!Controlled) {
				if (GambitCollection != null)
					GambitCollection.enabled = false;
			}
		}

		/// <summary>
		/// entity id
		/// </summary>
		[SerializeField]
		private int _entityId = -1;
		public int EntityId {
			get { return _entityId;}
			set{ _entityId = value;}
		}

		public void Setup(EntityAsset asset){
			SkillCollection.SkillCollectionId = asset.SkillCollectionId;
			SkillCollection.SetupCollection ();
			GambitCollection.GambitCollectionId = asset.GambitCollectionId;
			GambitCollection.SetupCollection ();
			Perception.AlertMask = asset.AlertMask;
			Perception.Range = asset.Vision;
			Tag = asset.Tag;
		}
	}
}
