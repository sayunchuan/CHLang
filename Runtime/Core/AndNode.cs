/*
 * AndNode.cs
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
    /// 逻辑与运算节点
    /// </summary>
    public class AndNode : ALogicNode
    {
        public INode l;
        public INode r;

        #region INode implementation

        public override bool GetBool()
        {
            return l.GetBool() && r.GetBool();
        }

        public override void BindStatement(Statement statement)
        {
            l.BindStatement(statement);
            r.BindStatement(statement);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("({0} && {1})", l, r);
        }
    }
}