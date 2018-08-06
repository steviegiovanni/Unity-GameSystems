using UtilitySystems;
using GameSystems.SkillSystem.Database;
using GameSystems.GambitSystem.Database;
using GameSystems.Entities.Database;
using GameSystems.Items.Database;

/// <summary>
/// main singleton database that will load the entities, skill collection, and the gambit collection
/// </summary>
public class SystemDatabase : Singleton<SystemDatabase> {
	private EntityDatabase _entities;
	private SkillCollectionDatabase _skillCollections;
	private GambitCollectionDatabase _gambitCollections;
	private ItemDatabase _itemDatabase;

	private void Awake(){
		transform.SetParent (null);
		DontDestroyOnLoad (this.gameObject);
	}

	static public ItemDatabase ItemDatabase{
		get{
			if (Instance != null) {
				if (Instance._itemDatabase == null) {
					Instance._itemDatabase = new ItemDatabase ();
					Instance._itemDatabase.LoadDatabase ();
				}
				return Instance._itemDatabase;
			}
			return null;
		}
	}

	static public EntityDatabase Entities{
		get{
			if (Instance != null) {
				if (Instance._entities == null) {
					Instance._entities = new EntityDatabase ();
					Instance._entities.LoadDatabase ();
				}
				return Instance._entities;
			}
			return null;
		}
	}

	static public SkillCollectionDatabase SkillCollections{
		get{
			if (Instance != null) {
				if (Instance._skillCollections == null) {
					Instance._skillCollections = new SkillCollectionDatabase ();
					Instance._skillCollections.LoadDatabase ();
				}
				return Instance._skillCollections;
			}
			return null;
		}
	}

	static public GambitCollectionDatabase GambitCollections{
		get{
			if (Instance != null) {
				if (Instance._gambitCollections == null) {
					Instance._gambitCollections = new GambitCollectionDatabase ();
					Instance._gambitCollections.LoadDatabase ();
				}
				return Instance._gambitCollections;
			}
			return null;
		}
	}
}
