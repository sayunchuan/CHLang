/*
 * ConditionNode.cs
 * 
 * Author:
 * 		Jack Wen <sachuan@foxmail.com>
 * 
 * Copyright (c) 2019 a. All Right Reserved
 * 
 * Desc:
 * 
 * History:
 * 		Data	|	Version		|	Author	|	Details
 * -------------|---------------|-----------|---------------------------------------------------------------------------
 * 
*/

namespace CHLang.Core
{
    public class ConditionNode : INode
    {
        public INode condition;
        public INode l;
        public INode r;

        public override Num GetNum()
        {
            return condition.GetBool() ? l.GetNum() : r.GetNum();
        }

        public override bool GetBool()
        {
            return condition.GetBool() ? l.GetBool() : r.GetBool();
        }

        public override Type GetNodeType()
        {
            return l.GetNodeType();
        }

        public override void BindStatement(Statement statement)
        {
            condition.BindStatement(statement);
            l.BindStatement(statement);
            r.BindStatement(statement);
        }

        public override string ToString()
        {
            return string.Format("({0} ? {1} : {2})", condition, l, r);
        }
    }
}