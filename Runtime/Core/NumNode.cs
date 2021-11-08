/*
 * NumNode.cs
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
    /// 纯数据节点
    /// </summary>
    public class NumNode : ANode
    {
        protected override Type _type => Type.DOUBLE;

        public NumNode(string str)
        {
            int res;
            if (int.TryParse(str, out res))
            {
                _SetInt(res);
            }
            else
            {
                _SetDouble(double.Parse(str));
            }
        }

        public override void BindStatement(Statement statement)
        {
        }
    }
}