//***************
// name : ASystemProcess
// createtime: 5/2/2019
// desc: 
// create by asvo
//***************
using System;
using System.Collections.Generic;

namespace Asvo.ECS
{
	public class ASystemProcess {
		
		protected ASystem m_system;
		protected List<Type> m_compoTypes;
		public CEntityList EntityList {get; protected set;}

		public ASystemProcess(ASystem system, params Type[] components)
		{
			m_system = system;
			m_compoTypes = new List<Type>();
			m_compoTypes.AddRange(components);
			EntityList = new CEntityList();
		}
		
		public virtual void AddEntity(CEntity entity){}
		public virtual void RemoveEntity(CEntity entity){}		
		public void Process(float deltaT){
            m_system.m_deltaTime = deltaT;
            m_system.PreUpdate();
            for(var node = EntityList.head;node != null;node = node.next)
            {
                m_system.Update(node.Entity);
            }
            m_system.PostUpdate();
        }
        
		public void MatchAdd(CEntity entity)
		{
			Match(entity, m_compoTypes, m_system);
		}

		public void MatchRemove(CEntity entity)
		{
			Match(entity, m_compoTypes, m_system);
		}

		protected void Match(CEntity entity, List<Type> matchTypes, ASystem system)
		{
			if (EntityList.HasEntity(entity))
            {
                InternalEntityChange(EntityList, entity, matchTypes, system);
            }
            else if (entity.HasComponents(matchTypes))
            {
                EntityList.AddEntity(entity);

                if (system != null)
                {
                    system.UpdateAddEntity(entity);
                }
            }
		}

		private void InternalEntityChange(CEntityList entityList, CEntity entity, List<Type> matchTypes, ASystem system)
        {
            bool isEntityDeled = entityList.RemoveEntity(entity);
            bool isEntityAdded = false;

            if (entity.HasComponents(matchTypes))
            {
                entityList.AddEntity(entity);
                isEntityAdded = true;
            }

            if (isEntityAdded == false && isEntityDeled)
            {
                if (system != null)
                {
                    system.UpdateRemoveEntity(entity);
                }
            }

            if (isEntityDeled == false && isEntityAdded)
            {
                if (system != null)
                {
                    system.UpdateAddEntity(entity);
                }
            }
        }
	}
}
