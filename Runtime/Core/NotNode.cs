/*
 * NotNode.cs
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
    /// 逻辑非运算节点
    /// </summary>
    public class NotNode : ALogicNode
    {
        public INode l;

        #region INode implementation

        public override bool GetBool()
        {
            return !l.GetBool();
        }

        public override void BindStatement(Statement statement)
        {
            l.BindStatement(statement);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("(!{0})", l);
        }
    }
}