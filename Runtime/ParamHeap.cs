/*
 * ParamHeap.cs
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

using CHLang.StatementLog;

namespace CHLang
{
    public class ParamHeap
    {
        private IParamPool __paramPool;

        public ParamHeap(IParamPool paramPool)
        {
            __paramPool = paramPool;
        }

        public void Clear()
        {
            __paramPool.Clear();
        }

        public double this[string key]
        {
            get { return __paramPool.GetNum(key); }
            set { __paramPool.SetNum(key, value); }
        }

        public object GetParam(string id)
        {
            return __paramPool.GetObj(id);
        }

        public void SetParam(string index, object obj)
        {
            __paramPool.SetObj(index, obj);
        }

        public ILog Log => __paramPool.Log;

        public void BindLogKit(ILog log)
        {
            __paramPool.BindLogKit(log);
        }

        public void UnbindLogKit()
        {
            __paramPool.UnbindLogKit();
        }
    }
}