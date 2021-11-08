using System;
using CHLang.StatementLog;

namespace CHLang
{
    /// <summary>
    /// 语句，即一条完整的计算语句
    /// </summary>
    public class Statement
    {
        /// <summary>
        /// 语句内容记录
        /// </summary>
        public string func;

        /// <summary>
        /// 语句根节点
        /// </summary>
        private INode node;

        /// <summary>
        /// 语句所用堆
        /// </summary>
        public IParamPool heap;

        public Statement(string func, INode node)
        {
            this.func = func;
            this.node = node;
            this.node.BindStatement(this);
        }

        public void ClearParam()
        {
            heap.Clear();
        }

        public double LoadNum(string key)
        {
            return heap.GetNum(key);
        }

        public object LoadObj(string id)
        {
            return heap.GetObj(id);
        }

        public void SetParam(string index, double obj)
        {
            heap.SetNum(index, obj);
        }

        public void SetParam(string index, object obj)
        {
            heap.SetObj(index, obj);
        }

        public double GetDouble()
        {
            try
            {
                return node.GetNum().DoubleValue;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Statement GetDouble ERROR of '{func}'.\n{e}");
                throw;
            }
        }

        public bool GetBool()
        {
            try
            {
                return node.GetBool();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Statement GetBool ERROR of '{func}'.\n{e}");
                throw;
            }
        }

        public Type GetNodeType()
        {
            return node.GetNodeType();
        }

        public override string ToString()
        {
            return node.ToString();
        }
    }
}