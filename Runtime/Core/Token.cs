/*
 * Token.cs
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

namespace CHLang.Core
{
    public struct Token
    {
        public bool isFlag;
        public ushort flagID;
        public string detail;

        public Token(bool isFlag, ushort flagID, string detail)
        {
            this.isFlag = isFlag;
            this.flagID = flagID;
            this.detail = detail;
        }

        public override string ToString()
        {
            if (isFlag)
            {
                switch (flagID)
                {
                    case InterpreterFactor.c_nLPa:
                        return "(";
                    case InterpreterFactor.c_nRPa:
                        return ")";
                    case InterpreterFactor.c_nPlus:
                        return " +";
                    case InterpreterFactor.c_nMinus:
                        return " -";
                    case InterpreterFactor.c_nMul:
                        return "*";
                    case InterpreterFactor.c_nDiv:
                        return "/";
                    case InterpreterFactor.c_nNOT:
                        return "!";
                    case InterpreterFactor.c_nMod:
                        return "%";
                    case InterpreterFactor.c_nAdd:
                        return "+";
                    case InterpreterFactor.c_nSub:
                        return "-";
                    case InterpreterFactor.c_nBig:
                        return ">";
                    case InterpreterFactor.c_nLess:
                        return "<";
                    case InterpreterFactor.c_nBigEQ:
                        return ">=";
                    case InterpreterFactor.c_nLessEQ:
                        return "<=";
                    case InterpreterFactor.c_nEQ:
                        return "==";
                    case InterpreterFactor.c_nNEQ:
                        return "!=";
                    case InterpreterFactor.c_nAND:
                        return "&&";
                    case InterpreterFactor.c_nOR:
                        return "||";
                    case InterpreterFactor.c_nConR:
                        return ":";
                    case InterpreterFactor.c_nConL:
                        return "?";
                }
            }

            return detail;
        }
    }
}