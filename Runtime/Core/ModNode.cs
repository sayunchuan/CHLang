/*
 * ModNode.cs
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

using CHLang.Node;

namespace CHLang.Core
{
    public class ModNode : ANumNode
    {
        public INode l;
        public INode r;

        public override Num GetNum()
        {
            return l.GetNum() % r.GetNum();
        }

        public override void BindStatement(Statement statement)
        {
            l.BindStatement(statement);
            r.BindStatement(statement);
        }

        public override string ToString()
        {
            return string.Format("({0} % {1})", l, r);
        }
    }
}