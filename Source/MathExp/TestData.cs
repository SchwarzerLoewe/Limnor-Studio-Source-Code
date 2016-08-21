/*
 
 * Author:	Bob Limnor (info@limnor.com)
 * Project: Limnor Studio
 * Item:	Visual Expression Display and Edit
 * License: GNU General Public License v3.0
 
 */
/*
 * All rights reserved by Longflow Enterprises Ltd
 * 
 * Formula Editor
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.IO;

namespace MathExp
{
	public class CompileResult
	{
		#region fields and constructors
		private object _returnValue;
		private MethodInfo _mi;
		public CompileResult()
		{
		}
		public CompileResult(string classNamespace, string className, string methodName,
			CodeCompileUnit code, List<IPropertyPointer> pointers, VariableList vars, AssemblyRefList importedAssemblies)
		{
			ClassNamespace = classNamespace;
			ClassName = className;
			MethodName = methodName;
			CodeUnit = code;
			PropertyPointers = pointers;
			Variables = vars;
			Imports = importedAssemblies;
		}
		#endregion
		#region properties
		private string _cns;
		public string ClassNamespace { get { return _cns; } set { _cns = value; } }
		private string _cn;
		public string ClassName { get { return _cn; } set { _cn = value; } }
		private string _mn;
		public string MethodName { get { return _mn; } set { _mn = value; } }
		private CodeCompileUnit _cu;
		public CodeCompileUnit CodeUnit { get { return _cu; } set { _cu = value; } }
		private List<IPropertyPointer> _ps;
		public List<IPropertyPointer> PropertyPointers { get { return _ps; } set { _ps = value; } }
		private VariableList _vs;
		public VariableList Variables { get { return _vs; } set { _vs = value; } }
		private AssemblyRefList _imps;
		public AssemblyRefList Imports { get { return _imps; } set { _imps = value; } }
		public object ReturnValue
		{
			get
			{
				return _returnValue;
			}
		}
		public MethodInfo Method
		{
			get
			{
				return _mi;
			}
		}
		#endregion
		#region methods
		private bool _dc;
		public bool DebugCompile { get { return _dc; } set { _dc = value; } }
		public void compile()
		{
			StringCollection importLocations = new StringCollection();
			CSharpCodeProvider cs = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters(new string[] { });
			cp.GenerateInMemory = true;
			cp.OutputAssembly = "AutoGenerated";

			foreach (AssemblyRef ar in Imports)
			{
				importLocations.Add(ar.Location);
			}
			foreach (string s in importLocations)
			{
				cp.ReferencedAssemblies.Add(s);
			}
			cp.GenerateExecutable = false;
			if (DebugCompile)
			{
				CodeGeneratorOptions o = new CodeGeneratorOptions();
				o.BlankLinesBetweenMembers = false;
				o.BracingStyle = "C";
				o.ElseOnClosing = false;
				o.IndentString = "    ";
				StringWriter sw = new StringWriter();
				string sErr = "";
				try
				{
					cs.GenerateCodeFromCompileUnit(CodeUnit, sw, o);
				}
				catch (Exception err)
				{
					sErr = err.Message;
				}
				sw.Flush();
				sw.Close();
				string sCode = sw.ToString();
				if (string.IsNullOrEmpty(sErr))
				{
					FormMessage.ShowMessage(null, sCode);
				}
				else
				{
					FormMessage.ShowMessage(null, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\r\n{1}", sErr, sCode));
				}
			}
			else
			{
				CompilerResults crs = cs.CompileAssemblyFromDom(cp, CodeUnit);
				if (crs.Errors.HasErrors)
				{
					FormCompilerError dlg = new FormCompilerError();
					for (int i = 0; i < crs.Errors.Count; i++)
					{
						dlg.AddItem(crs.Errors[i]);
					}
					dlg.Show();
					dlg.TopMost = true;
					dlg.BringToFront();
				}
				else
				{
					Type[] types = crs.CompiledAssembly.GetExportedTypes();
					if (types != null)
					{
						for (int i = 0; i < types.Length; i++)
						{
							if (types[i].Name == ClassName)
							{
								_mi = types[i].GetMethod(MethodName);
								if (_mi != null)
								{
									break;
								}
							}
						}
					}
					if (_mi == null)
					{
						FormCompilerError dlg = new FormCompilerError();
						dlg.AddItem("Method not found in the compiled class");
						dlg.Show();
						dlg.TopMost = true;
						dlg.BringToFront();
					}
				}
			}
		}
		public void Execute(object[] vs)
		{
			if (_mi == null)
			{
				compile();
			}
			if (_mi != null)
			{
				_returnValue = _mi.Invoke(null, vs);
			}
		}
		public void Execute()
		{
			if (_mi == null)
			{
				compile();
			}
			if (_mi != null)
			{
				List<object> ps = new List<object>();
				for (int i = 0; i < Variables.Count; i++)
				{
					ps.Add(null);
				}
				foreach (IPropertyPointer p in PropertyPointers)
				{
					ps.Add(p.ObjectInstance);
				}
				object[] vs = new object[ps.Count];
				ps.CopyTo(vs);
				_returnValue = _mi.Invoke(null, vs);
			}
		}
		#endregion
	}
	/// <summary>
	/// data needed for static testing
	/// </summary>
	public class TestData : ITestData
	{
		#region fields and constructors
		public CompileResult _code;
		private bool _finished; //notification signal
		public TestData(CompileResult compiled)
		{
			_code = compiled;
		}
		#endregion
		#region properties
		//test class
		public string ClassName
		{
			get
			{
				return _code.ClassName;
			}
		}
		//test method (static)
		public string MethodName
		{
			get
			{
				return _code.MethodName;
			}
		}
		//method parameters
		public VariableList Parameters
		{
			get
			{
				return _code.Variables;
			}
		}
		public List<IPropertyPointer> Pointers
		{
			get
			{
				return _code.PropertyPointers;
			}
		}
		//compile unit already constructed
		public CodeCompileUnit CU
		{
			get
			{
				return _code.CodeUnit;
			}
		}
		//referenced assemblies (libraries)
		public AssemblyRefList Assemblies
		{
			get
			{
				return _code.Imports;
			}
		}
		#endregion
		#region ITestData Members

		public bool Finished
		{
			get
			{
				return _finished;
			}
			set
			{
				_finished = value;
			}
		}

		#endregion
	}
}
