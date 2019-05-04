//***************
// name : AComponent
// createtime: 5/1/2019
// desc: 
// create by asvo
//***************
using System;

namespace Asvo.ECS
{
	public abstract class AComponent {
		
		public Type Type{get; private set;}
		public string Name{get; private set;}

		private CEntity m_entity;
		public CEntity Entity {
			get
			{
				return m_entity;
			}
		 	set
			{
				if (null != m_entity && value != null)
				{
					//error
					return;
				}
				m_entity = value;
				OnBindEntity();
			}
		}

		public bool IsValid {get; private set;}

		public AComponent()
		{
			Type = GetType();
			Name = Type.Name;
		}
		
		protected virtual void OnBindEntity()
		{

		}

		public void Clear(){
			OnDestroy();
            IsValid = false;
            Entity = null;
		}

		protected abstract void OnDestroy();
	}
}
