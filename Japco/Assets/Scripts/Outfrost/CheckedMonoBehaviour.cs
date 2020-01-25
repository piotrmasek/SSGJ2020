using System.Reflection;
using UnityEngine;

namespace Outfrost {

	public class CheckedMonoBehaviour : MonoBehaviour {

		protected void CheckReferences() {
			if (Util.IsPrefab(gameObject)) return;
			
			TypeInfo scriptType = GetType().GetTypeInfo();
			bool passed = true;
			
			foreach (FieldInfo field in scriptType.GetFields()) {
				ExpectAttachedAttribute attribute = field.GetCustomAttribute<ExpectAttachedAttribute>();
				if (attribute != null && field.GetValue(this) == null) {
					Debug.LogErrorFormat("ExpectAttached: Field {0}.{1} contains a null reference", scriptType.Name, field.Name);
					passed = false;
				}
			}

			if (! passed) {
				enabled = false;
			}
		}

	}

}