using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

/// <summary>
/// By BlazingTwist#
/// </summary>
/// <typeparam name="T"></typeparam>
public class PropertyLogger<T>
{
	private readonly string callerName;
	private readonly int callerLine;
	private readonly T instanceValue;
	private readonly Expression<Func<T>> instanceExpr;
	private readonly List<Tuple<string, object, Exception>> expressions = new List<Tuple<string, object, Exception>>();

	public PropertyLogger(Expression<Func<T>> instanceExpr, [CallerMemberName] string callerMember = "unknown", [CallerLineNumber] int lineNumber = -1)
	{
		instanceValue = instanceExpr.Compile().Invoke();
		this.instanceExpr = instanceExpr;
		callerName = callerMember;
		callerLine = lineNumber;
	}

	public PropertyLogger<T> Expression<TVal>(string displayText, Expression<Func<T, TVal>> expression)
	{
		try
		{
			TVal result = expression.Compile().Invoke(instanceValue);
			expressions.Add(new Tuple<string, object, Exception>(displayText, result, null));
		}
		catch (Exception e)
		{
			expressions.Add(new Tuple<string, object, Exception>(displayText, "<ERROR>", e));
		}
		return this;
	}

	public PropertyLogger<T> Expression<TVal>(Expression<Func<T, TVal>> expression)
	{
		string expressionString;
		try
		{
			expressionString = Simplify(expression).ToString();
		}
		catch (Exception)
		{
			expressionString = "" + expression;
		}
		return Expression(expressionString, expression);
	}

	public override string ToString()
	{
		StringBuilder builder = new StringBuilder();
		builder.Append($"{callerName} #{callerLine} => properties of '{((MemberExpression)instanceExpr.Body).Member.Name}' ({typeof(T).AssemblyQualifiedName})");

		List<List<string>> columns = new List<List<string>> {
				new List<string> { "Expression" },
				new List<string> { "Value" },
				new List<string> { "Type" },
				new List<string> { "Exception" }
		};
		foreach (Tuple<string, object, Exception> expressionInfo in expressions)
		{
			columns[0].Add("" + expressionInfo.Item1);
			columns[1].Add("" + expressionInfo.Item2);
			if (expressionInfo.Item3 != null)
			{ // threw exception
				columns[2].Add("<ERROR>");
			}
			else if (expressionInfo.Item2 == null)
			{
				columns[2].Add("<NULL>");
			}
			else
			{
				columns[2].Add(expressionInfo.Item2.GetType().FullName);
			}
			columns[3].Add("" + expressionInfo.Item3?.Message.Replace("\r", "\\r").Replace("\n", "\\n "));
		}
		List<int> columnWidths = columns.Select(col => col.Select(str => str.Length).Max() + 2).ToList();
		int rowCount = columns[0].Count;
		foreach (List<string> row in columns.SelectMany((col, colIndex) =>
		{
			int targetWidth = columnWidths[colIndex];
			return col.Select(str => str + new string(' ', targetWidth - str.Length)).ToList();
		})
				.Select((str, index) => new { str, index })
				.GroupBy(pair => pair.index % rowCount)
				.Select(group => @group.Select(pair => pair.str).ToList()))
		{
			builder.Append("\n  ").Append(string.Join("|  ", row));
		}
		return builder.ToString();
	}

	/**
     * The following code was liberated from https://stackoverflow.com/questions/53676292/how-can-i-get-a-string-from-linq-expression
     */
	private static Expression Simplify(Expression expression)
	{
		ParameterlessExpressionSearcher searcher = new ParameterlessExpressionSearcher();
		searcher.Visit(expression);
		return new ParameterlessExpressionEvaluator(searcher.ParameterlessExpressions).Visit(expression);
	}

	private static Expression<TExpr> Simplify<TExpr>(Expression<TExpr> expression)
	{
		return (Expression<TExpr>)Simplify((Expression)expression);
	}

	private class ParameterlessExpressionSearcher : ExpressionVisitor
	{
		public HashSet<Expression> ParameterlessExpressions { get; } = new HashSet<Expression>();
		private bool containsParameter;

		public override Expression Visit(Expression node)
		{
			bool originalContainsParameter = containsParameter;
			containsParameter = false;
			base.Visit(node);
			if (!containsParameter)
			{
				if (node?.NodeType == ExpressionType.Parameter)
					containsParameter = true;
				else
					ParameterlessExpressions.Add(node);
			}
			containsParameter |= originalContainsParameter;

			return node;
		}
	}

	private class ParameterlessExpressionEvaluator : ExpressionVisitor
	{
		private readonly HashSet<Expression> parameterlessExpressions;

		public ParameterlessExpressionEvaluator(HashSet<Expression> parameterlessExpressions)
		{
			this.parameterlessExpressions = parameterlessExpressions;
		}

		public override Expression Visit(Expression node)
		{
			return parameterlessExpressions.Contains(node) ? Evaluate(node) : base.Visit(node);
		}

		private Expression Evaluate(Expression node)
		{
			if (node.NodeType == ExpressionType.Constant)
			{
				return node;
			}
			object value = System.Linq.Expressions.Expression.Lambda(node).Compile().DynamicInvoke();
			return System.Linq.Expressions.Expression.Constant(value, node.Type);
		}
	}
}
