using System;
using System.Collections.Generic;
using System.Linq;

namespace pvc.Projections
{
	public class TypeFinder
	{
		public static IEnumerable<Type> GetTypesMatching<TAttribute>() where TAttribute : Attribute 
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return (from assembly in assemblies
			        from item in assembly.GetTypes()
			        where !item.IsAbstract && item.IsClass && item.GetCustomAttributes(typeof (TAttribute), false).Length > 0
			        select item);
		}
	}
}