﻿/*
 
 * Author:	Bob Limnor (info@limnor.com)
 * Project: Limnor Studio
 * Item:	Visual Expression Display and Edit
 * License: GNU General Public License v3.0
 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MathExp
{
	public interface INamedMathNode
	{
		string Name { get; }
		string GetChildNameByIndex(int idx);
	}
	public interface IPropertyMathNode : IPropertyPointer
	{
		string PropertyName { get; }
	}
}
