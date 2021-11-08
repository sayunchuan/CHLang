/*
 * ANumNode.cs
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

namespace CHLang.Node
{
    public abstract class ANumNode : INode
    {
        #region implemented abstract members of AbsNode

        public sealed override bool GetBool()
        {
            return GetNum().Memory != 0;
        }

        public sealed override Type GetNodeType()
        {
            return Type.DOUBLE;
        }

        #endregion
    }
}