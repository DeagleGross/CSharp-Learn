using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://leetcode.com/problems/basic-calculator-ii/

namespace LeetCodeSolutions.RandomTasks.Claculator
{
	[TestClass]
	public class BasicCalculator2
	{
		[TestMethod]
		public void Solve()
		{
			var s = "3+2*2";
			var result = Calculate(s);

			result.Should().Be(7);
		}

		[TestMethod]
		public void Solve2()
		{
			var s = " 3+5 / 2 ";
			var result = Calculate(s);

			result.Should().Be(5);
		}

		[TestMethod]
		public void Solve3()
		{
			var s = "3+2*2+2";
			var result = Calculate(s);

			result.Should().Be(9);
		}

		[TestMethod]
		public void Solve4()
		{
			var s = "3/2";
			var result = Calculate(s);

			result.Should().Be(1);
		}

		[TestMethod]
		public void Solve5()
		{
			var s = "0-2147483647";
			var result = Calculate(s);

			result.Should().Be(-2147483647);
		}

		[TestMethod]
		public void Solve6()
		{
			var s = "1 - 1 + 1";
			var result = Calculate(s);

			result.Should().Be(1);
		}

		public int Calculate_FromSolution(String s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0;
			}

			int expressionLength = s.Length;

			Stack<int> evaluationStack = new();

			int currentNumber = 0;
			char operation = '+';

			for (int i = 0; i < expressionLength; i++)
			{
				char currentChar = s[i];
				if (char.IsDigit(currentChar))
				{
					// this is int.Parse done manually and digit by digit
					currentNumber = (currentNumber * 10) + (currentChar - '0');
					continue;
				}

				if (!char.IsDigit(currentChar) 
					&& !char.IsWhiteSpace(currentChar) 
					|| i == expressionLength - 1)
				{
					if (operation == '-')
					{
						evaluationStack.Push(-currentNumber);
					}
					else if (operation == '+')
					{
						evaluationStack.Push(currentNumber);
					}
					else if (operation == '*')
					{
						evaluationStack.Push(evaluationStack.Pop() * currentNumber);
					}
					else if (operation == '/')
					{
						evaluationStack.Push(evaluationStack.Pop() / currentNumber);
					}
					operation = currentChar;
					currentNumber = 0;
				}
			}

			int result = 0;

			while (evaluationStack.Count > 0)
			{
				result += evaluationStack.Pop();
			}
			return result;
		}

		#region My Solution (works but a bit too long)

		public int Calculate(string s)
		{
			var expression = s.Trim();

			// this stack holds only ints that we need to add in the end
			// if current operation is "-" - we add -curentNumber to this stack
			// if current operation is "+" - we simply add currentNumber to this stack
			Stack<int> evaluationStack = new();

			int position = 0;

			var token = GetNextToken(expression, ref position);

			while (token.tokenType != TokenType.End)
			{
				if (token.tokenType == TokenType.Number)
				{
					evaluationStack.Push(int.Parse(token.token));
					token = GetNextToken(expression, ref position);
					continue;
				}

				if (token.tokenType == TokenType.Operation)
				{
					if (token.token == "+"
						|| token.token == "-")
					{
						var nextNumber = GetNextToken(expression, ref position);
						var value = int.Parse(nextNumber.token);

						if (token.token == "-")
						{
							evaluationStack.Push(-value);
						}
						else
						{
							evaluationStack.Push(value);
						}

						token= GetNextToken(expression, ref position);
						continue;
					}
					else
					{
						var operand1 = evaluationStack.Pop();
						var operand2 = GetNextToken(expression, ref position);

						var result = token.token switch {
							"*" => operand1 * int.Parse(operand2.token),
							"/" => operand1 / int.Parse(operand2.token),
							_ => throw new ArgumentOutOfRangeException()
						};

						evaluationStack.Push(result);

						token = GetNextToken(expression, ref position);
					}
				}
			}

			if (evaluationStack.Count == 1)
			{
				return evaluationStack.Pop();
			}

			var ret = 0;
			while (evaluationStack.Count > 0)
			{
				ret += evaluationStack.Pop();
			}

			return ret;
		}

		private enum TokenType
		{
			Number,
			Operation,
			End
		}

		private (string token, TokenType tokenType) GetNextToken(string s, ref int position)
		{
			if (position >= s.Length)
			{
				return (string.Empty, TokenType.End);
			}

			var c = s[position];
			position++;

			while (c == ' ' && position < s.Length)
			{
				c = s[position];
				position++;
			}

			if (c == '*'
				|| c == '/'
				|| c == '+'
				|| c == '-')
			{
				return (c.ToString(), TokenType.Operation);
			}

			StringBuilder number = new();
			number.Append(c);

			while (position < s.Length && char.IsDigit(s[position]))
			{
				number.Append(s[position]);
				position++;
			}

			return (number.ToString(), TokenType.Number);
		} 

		#endregion
	}
}
