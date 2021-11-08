/*
 * InterPreterFactor.cs
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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CHLang.Core;

namespace CHLang
{
    /// <summary>
    /// 基础解释器生成工厂
    /// 
    /// 针对公式进行解析
    /// 
    /// 识别的操作符有：
    /// 优先级I：'(' ')'
    /// 优先级II：'+' '-',正负号
    /// 优先级III：'*' '/' '!' '%'
    /// 优先级IV：'+' '-'
    /// 优先级V：'>' '>=' '<' '<='
    /// 优先级VI：'==' '!='
    /// 优先级VII：'&&'
    /// 优先级VIII：'||'
    /// 
    /// 针对叶子节点，通过扩展_GetNumNode方法，可以构建功能所需的特殊子节点
    /// 
    /// 所有数据采用double进行计算
    /// </summary>
    public class InterpreterFactor
    {
        private static ConcurrentDictionary<string, List<Token>> _tokenNotes =
            new ConcurrentDictionary<string, List<Token>>();

        #region Flag Level

        private const ushort c_nFlagI = 1 << 15;
        private const ushort c_nFlagII = 1 << 14;
        private const ushort c_nFlagIII = 1 << 13;
        private const ushort c_nFlagIV = 1 << 12;
        private const ushort c_nFlagV = 1 << 11;
        private const ushort c_nFlagVI = 1 << 10;
        private const ushort c_nFlagVII = 1 << 9;
        private const ushort c_nFlagVIII = 1 << 8;
        private const ushort c_nFlagIX = 1 << 7;
        private const ushort c_nFlagX = 1 << 6;

        private const ushort c_nFlagALL = c_nFlagI + c_nFlagII + c_nFlagIII + c_nFlagIV + c_nFlagV + c_nFlagVI +
                                          c_nFlagVII + c_nFlagVIII + c_nFlagIX + c_nFlagX;

        #endregion

        #region Flag ID

        /// <summary>
        /// (
        /// </summary>
        public const ushort c_nLPa = c_nFlagI + 1;

        /// <summary>
        /// )
        /// </summary>
        public const ushort c_nRPa = c_nFlagI + 2;

        /// <summary>
        /// +正号
        /// </summary>
        public const ushort c_nPlus = c_nFlagII + 1;

        /// <summary>
        /// -负号
        /// </summary>
        public const ushort c_nMinus = c_nFlagII + 2;

        /// <summary>
        /// *
        /// </summary>
        public const ushort c_nMul = c_nFlagIII + 1;

        /// <summary>
        /// /
        /// </summary>
        public const ushort c_nDiv = c_nFlagIII + 2;

        /// <summary>
        /// !
        /// </summary>
        public const ushort c_nNOT = c_nFlagIII + 3;

        /// <summary>
        /// %
        /// </summary>
        public const ushort c_nMod = c_nFlagIII + 4;

        /// <summary>
        /// +
        /// </summary>
        public const ushort c_nAdd = c_nFlagIV + 1;

        /// <summary>
        /// -
        /// </summary>
        public const ushort c_nSub = c_nFlagIV + 2;

        /// <summary>
        /// >
        /// </summary>
        public const ushort c_nBig = c_nFlagV + 1;

        /// <summary>
        /// <
        /// </summary>
        public const ushort c_nLess = c_nFlagV + 2;

        /// <summary>
        /// >=
        /// </summary>
        public const ushort c_nBigEQ = c_nFlagV + 3;

        /// <summary>
        /// <=
        /// </summary>
        public const ushort c_nLessEQ = c_nFlagV + 4;

        /// <summary>
        /// ==
        /// </summary>
        public const ushort c_nEQ = c_nFlagVI + 1;

        /// <summary>
        /// !=
        /// </summary>
        public const ushort c_nNEQ = c_nFlagVI + 2;

        /// <summary>
        /// &&
        /// </summary>
        public const ushort c_nAND = c_nFlagVII + 1;

        /// <summary>
        /// ||
        /// </summary>
        public const ushort c_nOR = c_nFlagVIII + 1;

        /// <summary>
        /// :
        /// </summary>
        public const ushort c_nConR = c_nFlagIX + 1;

        /// <summary>
        /// ?
        /// </summary>
        public const ushort c_nConL = c_nFlagX + 1;

        #endregion

        #region Spe Char Num

        /// <summary>
        /// +
        /// </summary>
        public const int c_nCharAdd = '+';

        /// <summary>
        /// -
        /// </summary>
        public const int c_nCharSub = '-';

        /// <summary>
        /// *
        /// </summary>
        public const int c_nCharMul = '*';

        /// <summary>
        /// /
        /// </summary>
        public const int c_nCharDiv = '/';

        /// <summary>
        /// !
        /// </summary>
        public const int c_nCharNO = '!';

        /// <summary>
        /// =
        /// </summary>
        public const int c_nCharEQ = '=';

        /// <summary>
        /// >
        /// </summary>
        public const int c_nCharBig = '>';

        /// <summary>
        /// <
        /// </summary>
        public const int c_nCharLess = '<';

        /// <summary>
        /// &
        /// </summary>
        public const int c_nCharAND = '&';

        /// <summary>
        /// |
        /// </summary>
        public const int c_nCharOR = '|';

        /// <summary>
        /// (
        /// </summary>
        public const int c_nCharLPA = '(';

        /// <summary>
        /// )
        /// </summary>
        public const int c_nCharRPA = ')';

        /// <summary>
        /// ?
        /// </summary>
        public const int c_nCharQu = '?';

        /// <summary>
        /// :
        /// </summary>
        public const int c_nCharAns = ':';

        /// <summary>
        /// %
        /// </summary>
        public const int c_nCharMod = '%';

        /// <summary>
        /// space
        /// </summary>
        public const int c_nCharSpace = ' ';

        #endregion

        #region Stack Pool

        // 数据栈实例池，供DoString调用，确保每次调用DoString都是独立的运算空间

        private Queue<Stack<INode>> paramStackPool = new Queue<Stack<INode>>();
        private Queue<Stack<ushort>> opeStackPool = new Queue<Stack<ushort>>();

        #endregion

        private bool[] speCharSet = new bool[128];

        public InterpreterFactor()
        {
            for (int i = 0; i < 128; i++)
            {
                if (i == c_nCharAdd ||
                    i == c_nCharSub ||
                    i == c_nCharMul ||
                    i == c_nCharDiv ||
                    i == c_nCharNO ||
                    i == c_nCharEQ ||
                    i == c_nCharBig ||
                    i == c_nCharLess ||
                    i == c_nCharAND ||
                    i == c_nCharOR ||
                    i == c_nCharLPA ||
                    i == c_nCharRPA ||
                    i == c_nCharQu ||
                    i == c_nCharAns ||
                    i == c_nCharMod ||
                    i == c_nCharSpace)
                {
                    speCharSet[i] = true;
                    continue;
                }

                speCharSet[i] = false;
            }
        }

        private bool __IsSpeChar(int c)
        {
            return c >= 128 ? false : speCharSet[c];
        }

        public Statement Do(string chunkStr, object doParam)
        {
            INode n = DoString(chunkStr, doParam);
            return new Statement(chunkStr, n);
        }

        public INode DoString(string chunkStr, object doParam)
        {
            Stack<INode> paramStack = paramStackPool.Count > 0 ? paramStackPool.Dequeue() : new Stack<INode>();
            Stack<ushort> opeStack = opeStackPool.Count > 0 ? opeStackPool.Dequeue() : new Stack<ushort>();
            INode res;

            List<Token> tokens = __SplitToken(chunkStr);
#if UNITY_EDITOR
            try
            {
#endif
                if (tokens.Count <= 0)
                {
                    res = null;
                    goto DO_STRING_OVER;
                }

                paramStack.Clear();
                opeStack.Clear();

                for (int i = 0, imax = tokens.Count; i < imax; i++)
                {
                    if (tokens[i].isFlag)
                    {
                        ushort currOpe = tokens[i].flagID;
                        if (opeStack.Count <= 0)
                        {
                            opeStack.Push(currOpe);
                        }
                        else
                        {
                            ushort lastOpe = opeStack.Peek();
                            if (currOpe == c_nRPa)
                            {
                                while (lastOpe != c_nLPa)
                                {
                                    __Stack2Res(paramStack, opeStack);
                                    lastOpe = opeStack.Peek();
                                }

                                opeStack.Pop();
                            }
                            else
                            {
                                if ((currOpe & c_nFlagALL) > (lastOpe & c_nFlagALL))
                                {
                                    /// 当前操作符的优先级高于前一操作符优先级
                                    opeStack.Push(currOpe);
                                }
                                else
                                {
                                    if (lastOpe == c_nLPa)
                                    {
                                        opeStack.Push(currOpe);
                                    }
                                    else
                                    {
                                        __Stack2Res(paramStack, opeStack);
                                        opeStack.Push(currOpe);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        INode _node = _GetNumNode(tokens[i].detail, doParam);
                        paramStack.Push(_node);
                    }
                }

                while (opeStack.Count > 0)
                {
                    __Stack2Res(paramStack, opeStack);
                }

                res = paramStack.Pop();
#if UNITY_EDITOR
            }
            catch (Exception e)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0, imax = tokens.Count; i < imax; i++)
                {
                    sb.Append(tokens[i]).Append(", ");
                }

                UnityEngine.Debug.LogError($"解释器解析错误：\n{chunkStr}\n{sb}\n\n{e}");
                res = null;
            }
#endif

            DO_STRING_OVER:
            paramStackPool.Enqueue(paramStack);
            opeStackPool.Enqueue(opeStack);
            return res;
        }

        private List<Token> __SplitToken(string chunkStr)
        {
            List<Token> res;
            if (_tokenNotes.TryGetValue(chunkStr, out res) && res != null)
            {
                return res;
            }

            res = new List<Token>();

            bool beforeIsSpe = false;
            for (int i = 0, imax = chunkStr.Length; i < imax; i++)
            {
                int charNum = chunkStr[i];
                // 空格处理
                if (charNum == c_nCharSpace) continue;

                bool __beforeIsSpe = true;
                switch (charNum)
                {
                    case c_nCharAdd:
                    {
                        if (i == 0 || (beforeIsSpe && chunkStr[i - 1] != c_nCharRPA))
                        {
                            Token t = new Token(true, c_nPlus, null);
                            res.Add(t);
                        }
                        else
                        {
                            Token t = new Token(true, c_nAdd, null);
                            res.Add(t);
                        }
                    }
                        break;
                    case c_nCharSub:
                    {
                        if (i == 0 || (beforeIsSpe && chunkStr[i - 1] != c_nCharRPA))
                        {
                            Token t = new Token(true, c_nMinus, null);
                            res.Add(t);
                        }
                        else
                        {
                            Token t = new Token(true, c_nSub, null);
                            res.Add(t);
                        }
                    }
                        break;
                    case c_nCharMul:
                    {
                        Token t = new Token(true, c_nMul, null);
                        res.Add(t);
                    }
                        break;
                    case c_nCharDiv:
                    {
                        Token t = new Token(true, c_nDiv, null);
                        res.Add(t);
                    }
                        break;
                    case c_nCharNO:
                    {
                        if ((i + 1) < imax && chunkStr[i + 1] == c_nCharEQ)
                        {
                            Token t = new Token(true, c_nNEQ, null);
                            res.Add(t);
                            i++;
                        }
                        else
                        {
                            Token t = new Token(true, c_nNOT, null);
                            res.Add(t);
                        }
                    }
                        break;
                    case c_nCharEQ:
                    {
                        if ((i + 1) < imax && chunkStr[i + 1] == c_nCharEQ)
                        {
                            Token t = new Token(true, c_nEQ, null);
                            res.Add(t);
                            i++;
                        }
                    }
                        break;
                    case c_nCharBig:
                    {
                        if ((i + 1) < imax && chunkStr[i + 1] == c_nCharEQ)
                        {
                            Token t = new Token(true, c_nBigEQ, null);
                            res.Add(t);
                            i++;
                        }
                        else
                        {
                            Token t = new Token(true, c_nBig, null);
                            res.Add(t);
                        }
                    }
                        break;
                    case c_nCharLess:
                    {
                        if ((i + 1) < imax && chunkStr[i + 1] == c_nCharEQ)
                        {
                            Token t = new Token(true, c_nLessEQ, null);
                            res.Add(t);
                            i++;
                        }
                        else
                        {
                            Token t = new Token(true, c_nLess, null);
                            res.Add(t);
                        }
                    }
                        break;
                    case c_nCharAND:
                    {
                        if ((i + 1) < imax && chunkStr[i + 1] == c_nCharAND)
                        {
                            Token t = new Token(true, c_nAND, null);
                            res.Add(t);
                            i++;
                        }
                    }
                        break;
                    case c_nCharOR:
                    {
                        if ((i + 1) < imax && chunkStr[i + 1] == c_nCharOR)
                        {
                            Token t = new Token(true, c_nOR, null);
                            res.Add(t);
                            i++;
                        }
                    }
                        break;
                    case c_nCharLPA:
                    {
                        Token t = new Token(true, c_nLPa, null);
                        res.Add(t);
                    }
                        break;
                    case c_nCharRPA:
                    {
                        Token t = new Token(true, c_nRPa, null);
                        res.Add(t);
                    }
                        break;
                    case c_nCharQu:
                    {
                        Token t = new Token(true, c_nConL, null);
                        res.Add(t);
                    }
                        break;
                    case c_nCharAns:
                    {
                        Token t = new Token(true, c_nConR, null);
                        res.Add(t);
                    }
                        break;
                    case c_nCharMod:
                    {
                        Token t = new Token(true, c_nMod, null);
                        res.Add(t);
                    }
                        break;
                    default:
                    {
                        int _stop = imax;
                        for (int j = i + 1; j < imax; j++)
                        {
                            if (__IsSpeChar(chunkStr[j]))
                            {
                                _stop = j;
                                break;
                            }
                        }

                        Token t = new Token(false, 0, chunkStr.Substring(i, _stop - i));
                        res.Add(t);
                        i = _stop - 1;
                        __beforeIsSpe = false;
                    }
                        break;
                }

                beforeIsSpe = __beforeIsSpe;
            }

            _tokenNotes.TryAdd(chunkStr, res);
            return res;
        }

        private void __Stack2Res(Stack<INode> paramStack, Stack<ushort> opeStack)
        {
            ushort ope = opeStack.Pop();
            switch (ope)
            {
                case c_nLPa:
                case c_nRPa:
                    break;
                case c_nPlus:
                {
                    PlusNode _t = new PlusNode();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nMinus:
                {
                    MinusNode _t = new MinusNode();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nMul:
                {
                    MulNode _t = new MulNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nDiv:
                {
                    DivNode _t = new DivNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nNOT:
                {
                    NotNode _t = new NotNode();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nAdd:
                {
                    AddNode _t = new AddNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nSub:
                {
                    SubNode _t = new SubNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nBig:
                {
                    BigNode _t = new BigNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nLess:
                {
                    LessNode _t = new LessNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nBigEQ:
                {
                    BigEQNode _t = new BigEQNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nLessEQ:
                {
                    LessEQNode _t = new LessEQNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nEQ:
                {
                    EQNode _t = new EQNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nNEQ:
                {
                    NEQNode _t = new NEQNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nAND:
                {
                    AndNode _t = new AndNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nOR:
                {
                    OrNode _t = new OrNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nConL:
                {
                    ConditionNode _t = paramStack.Pop() as ConditionNode;
                    _t.condition = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nConR:
                {
                    ConditionNode _t = new ConditionNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }

                case c_nMod:
                {
                    ModNode _t = new ModNode();
                    _t.r = paramStack.Pop();
                    _t.l = paramStack.Pop();
                    paramStack.Push(_t);
                    break;
                }
            }
        }

        protected virtual INode _GetNumNode(string token, object doParam)
        {
            NumNode res = new NumNode(token);
            return res;
        }
    }
}