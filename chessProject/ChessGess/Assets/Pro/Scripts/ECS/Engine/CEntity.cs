//***************
// name : CEntity
// createtime: 5/1/2019
// desc: 
// create by asvo
//***************
using System;
using System.Collections.Generic;

namespace Asvo.ECS
{
	public class CEntity {
		//起始ID，每创建一个entity，id自增1
		protected static ulong START_ID = 0;
		public ulong BaseId { get; set; }
		private List<AComponent> m_components;


		public CEntity()
		{
			BaseId = ++START_ID;
			m_components = new List<AComponent>();
		}

		public void InitComponents(List<AComponent> components)
		{
			if (null != components)
			{
				m_components = components;
				foreach(var compo in m_components)
				{
					compo.Entity = this;
				}
			}
		}

		public void AddComponent(AComponent component)
		{
			if (!m_components.Contains(component))
			{
				m_components.Add(component);
				component.Entity = this;
				
				CEngine.Instance.MatchSystemsAddComponent(this);
			}
		}

		public void RemoveComponent(AComponent component)
		{
			if (m_components.Contains(component))
			{
				m_components.Remove(component);
				component.Entity = null;
				
				CEngine.Instance.MatchSystemsAddComponent(this);
			}
		}

		public T GetComponent<T>() where T : AComponent
		{
			foreach(var component in m_components)
			{
				var t = component as T;
				if (null != t)
					return t;
			}
			return null;
		}

		public bool HasComponent<T>() where T : AComponent
		{
			return GetComponent<T>() != null;
		}

		public bool HasComponents(List<Type> matchTypes)
		{
			foreach(var mathcType in matchTypes)
			{
				if (!m_components.Exists(c=> c.GetType() == mathcType))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 销毁Entity
		/// </summary>
		public void Destroy()
		{
			foreach(var c in m_components)
				c.Clear();
			m_components.Clear();
		}
	}
}
