using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ShareOnSocialsLib.Expressions
{
	/// <summary>
	/// Extenders for expressions.
	/// </summary>
	public static class ExpressionHelpers
	{
		/// <summary>
		/// Gets the property value by compiling the given expression
		/// and returns the functions value.
		/// </summary>
		/// <typeparam name="T">Return type of the function</typeparam>
		/// <param name="expression">The expression to compile</param>
		/// <returns></returns>
		public static T GetPropertyValue<T>(this Expression<Func<T>> expression)
		{
			return expression.Compile().Invoke();
		}

		/// <summary>
		/// Sets the value of a given property.
		/// This is achieved by taking the expression that contains the property
		/// and compiling it and get the property. Then the property is set.
		/// </summary>
		/// <typeparam name="T">Type of the property to set</typeparam>
		/// <param name="action">Expression that contains the property</param>
		/// <param name="value">Value to set the property to</param>
		public static void SetPropertyValue<T>(this Expression<Func<T>> action, T value)
		{
			MemberExpression expression = action.Body as MemberExpression;

			PropertyInfo info = (PropertyInfo) expression.Member;
			var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

			info.SetValue(target, value);
		}
	}
}
