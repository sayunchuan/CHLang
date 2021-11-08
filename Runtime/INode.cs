/*
 * INode.cs
 * 
 * Author:
 * 		Jack Wen <sachuan@foxmail.com>
 * 
 * Copyright (c) 2018 a. All Right Reserved
 * 
 * Desc:
 * 
 * History:
 * 		Data	|	Version		|	Author	|	Details
 * -------------|---------------|-----------|---------------------------------------------------------------------------
 * 
*/

namespace CHLang
{
    public enum Type
    {
        DOUBLE,
        BOOL
    }

    public abstract class INode
    {
        public abstract Num GetNum();

        public abstract bool GetBool();

        public abstract Type GetNodeType();

        /// <summary>
        /// 语句环境引用，节点内部可访问
        /// </summary>
        protected Statement _statement;

        public virtual void BindStatement(Statement statement)
        {
            _statement = statement;
        }

        protected void _LinkChildNode(INode child)
        {
            child.BindStatement(_statement);
        }
    }
}