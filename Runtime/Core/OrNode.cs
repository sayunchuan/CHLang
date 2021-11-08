/*
 * OrNode.cs
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
    /// 逻辑或运算节点
    /// </summary>
    public class OrNode : ALogicNode
    {
        public INode l;
        public INode r;

        public override bool GetBool()
        {
            return l.GetBool() || r.GetBool();
        }

        public override void BindStatement(Statement statement)
        {
            l.BindStatement(statement);
            r.BindStatement(statement);
        }

        public override string ToString()
        {
            return string.Format("({0} || {1})", l, r);
        }
    }
}