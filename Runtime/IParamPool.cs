using CHLang.StatementLog;

namespace CHLang
{
    public interface IParamPool
    {
        object GetObj(string key);
        void SetObj(string key, object obj);
        double GetNum(string key);
        void SetNum(string key, double num);
        void Clear();

        ILog Log { get; }
        void BindLogKit(ILog log);
        void UnbindLogKit();
    }
}