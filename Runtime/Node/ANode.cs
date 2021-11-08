namespace CHLang.Node
{
    public abstract class ANode : INode
    {
        protected abstract Type _type { get; }
        private Num __num;

        protected void _SetDouble(double v)
        {
            __num = new Num(v);
        }

        protected void _SetInt(int v)
        {
            __num = new Num(v);
        }

        protected void _SetBool(bool v)
        {
            __num = v ? Num.One : Num.Zero;
        }

        #region INode implementation

        public override Num GetNum()
        {
            if (_type == Type.DOUBLE)
            {
                return __num;
            }

            return GetBool() ? Num.One : Num.Zero;
        }

        public override bool GetBool()
        {
            return __num.Memory != 0;
        }

        #endregion

        public override Type GetNodeType()
        {
            return _type;
        }

        public override string ToString()
        {
            if (_type == Type.DOUBLE)
            {
                return __num.ToString();
            }

            if (_type == Type.BOOL)
            {
                return GetBool().ToString();
            }

            return "";
        }
    }
}