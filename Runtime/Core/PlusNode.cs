/*
 * PlusNode.cs
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
    /// 加法运算节点
    /// </summary>
    public class PlusNode : ANumNode
    {
        public INode l;

        public override Num GetNum()
        {
            return l.GetNum();
        }

        public override void BindStatement(Statement statement)
        {
            l.BindStatement(statement);
        }

        public override string ToString()
        {
            return string.Format("{0}", l);
        }
    }
}