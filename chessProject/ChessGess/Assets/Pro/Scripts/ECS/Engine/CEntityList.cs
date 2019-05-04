using System;
using Asvo.Common;

namespace Asvo.ECS
{
    /// <summary>
    /// CEntityList节点
    /// </summary>
    public class EntityNode
    {
        public CEntity Entity;
        public EntityNode pre;
        public EntityNode next;

        public EntityNode()
        {
        }

        public void Destroy()
        {
            Entity.Destroy();
            Entity = null;
        }

        public void Dispose()
        {
            Entity = null;            
        }

        public T GetComponent<T>() where T : AComponent
        {
            return Entity.GetComponent<T>();
        }
    }

    /// <summary>
    /// Entity双向链表容器
    /// </summary>
    public class CEntityList
    {
        public EntityNode head = null;
        private EntityNode tail = null;

        public uint Count { get; private set; }

        private static System.Collections.Generic.Stack<EntityNode> s_NodePool = new System.Collections.Generic.Stack<EntityNode>();

        public CEntityList()
        {
            Count = 0;
        }

        private static EntityNode PopNode()
        {
            if (s_NodePool.Count == 0)
                return new EntityNode();
            return s_NodePool.Pop();
        }

        /// <summary>
        /// 尾插法添加结点
        /// </summary>
        /// <param name="node"></param>
        public void AddEntity(CEntity entity)
        {
            EntityNode node = PopNode();
            node.Entity = entity;
            node.pre = tail;
            node.next = null;

            if (tail != null)
                tail.next = node;
            tail = node;

            if (head == null)
                head = node;

            Count++;
        }

        public bool HasEntity(CEntity entity)
        {
            var p = head;
            while (p != null)
            {
                if (p.Entity == entity)
                {
                    return true;
                }

                p = p.next;
            }
            return false;
        }

        public bool RemoveEntity(CEntity entity)
        {
            var p = head;
            while (p != null)
            {
                if (p.Entity == entity)
                {
                    _RemoveNode(p);
                    return true;
                }

                p = p.next;
            }
            return false;
        }

        /// <summary>
        /// 删除结点
        /// 使用此方法请确保Entity在EntityList中，否则，使用RemoveBool代替
        /// </summary>
        /// <param name="entity"></param>
        private void _RemoveNode(EntityNode node)
        {
#if DEBUG
            if (_ContainsNode(node) == false)
            {
                CLog.Warning(SysLogType.Battle, "entity not exit in this entityList.entity.baseid={0}", node.Entity.BaseId);
                return;
            }
#endif

            EntityNode pre = node.pre;
            EntityNode next = node.next;

            if (head == node)
                head = next;

            if (tail == node)
                tail = pre;

            if (pre != null)
                pre.next = next;

            if (next != null)
                next.pre = pre;
            
            _DisposeNode(node);

            Count--;
        }
        
        private void _DisposeNode(EntityNode node)
        {
            node.Dispose();
            s_NodePool.Push(node);
        }

        private bool _ContainsNode(EntityNode node)
        {
            var p = head;
            while (p != null)
            {
                if (p == node)
                    return true;

                p = p.next;
            }
            return false;
        }

        /// <summary>
        /// 链表是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return head == null;
        }

        public void ForEach(Action<CEntity> action)
        {
            var p = head;
            while (p != null)
            {
                action(p.Entity);
                p = p.next;
            }
        }

        /// <summary>
        /// 清空链表
        /// </summary>
        public void Clear()
        {
            while (head != null)
            {
                var node = head;
                head = head.next;
                node.pre = null;
                node.next = null;
            }

            tail = null;
            Count = 0;
        }
    }
}
