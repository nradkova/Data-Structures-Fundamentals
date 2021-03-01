namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            return Solve(parentheses);
        }

        private bool Solve(string parentheses)
        {
            var stack = new Stack<char>();
            for (int i = 0; i < parentheses.Length; i++)
            {
                var current = parentheses[i];
                if (current == '('
                    || current == '{'
                    || current == '[')
                {
                    stack.Push(current);
                }

                if (stack.Count > 0)
                {
                    var prev = stack.Peek();
                    if (isValid(current, prev))
                    {
                        stack.Pop();
                    }
                }
                else 
                {
                    return false;
                }
            }
            if (stack.Count == 0)
            {
                return true;
            }
            return false;
        }

        private bool isValid(char current, char prev)
        {
            if (current == '}' && prev == '{')
            {
                return true;
            }
            if (current == ')' && prev == '(')
            {
                return true;
            }
            if (current == ']' && prev == '[')
            {
                return true;
            }
            return false;
        }
    }
}
