/*
 * ALogicNode.cs
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
    public abstract class ALogicNode : INode
    {
        #region implemented abstract members of AbsNode

        public sealed override Num GetNum()
        {
            return GetBool() ? Num.One : Num.Zero;
        }

        public sealed override Type GetNodeType()
        {
            return Type.BOOL;
        }

        #endregion
    }
}