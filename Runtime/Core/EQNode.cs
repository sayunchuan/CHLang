/*
 * EQNode.cs
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

using CHLang.Node;

namespace CHLang.Core
{
    /// <summary>
    /// 判等运算节点
    /// </summary>
    public class EQNode : ALogicNode
    {
        public INode l;
        public INode r;

        #region INode implementation

        public override bool GetBool()
        {
            if (l.GetNodeType() == Type.DOUBLE || r.GetNodeType() == Type.DOUBLE)
            {
                return l.GetNum() == r.GetNum();
            }

            return l.GetBool() == r.GetBool();
        }

        public override void BindStatement(Statement statement)
        {
            l.BindStatement(statement);
            r.BindStatement(statement);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("({0} == {1})", l, r);
        }
    }
}