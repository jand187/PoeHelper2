using System;
using System.Runtime.Serialization;

namespace PoeHelper.Monitor
{
	public class TypedSerializationBinder : SerializationBinder
	{
		public TypedSerializationBinder(string typeFormat)
		{
			TypeFormat = typeFormat;
		}

		public string TypeFormat { get; private set; }

		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = serializedType.Name;
		}

		public override Type BindToType(string assemblyName, string typeName)
		{
			string resolvedTypeName = string.Format(TypeFormat, typeName);

			return Type.GetType(resolvedTypeName, true);
		}
	}
}