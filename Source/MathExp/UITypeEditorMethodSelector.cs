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
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace MathExp
{
	public class UITypeEditorMethodSelector : UITypeEditor
	{
		public UITypeEditorMethodSelector()
		{
		}
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			IMethodSelector ts = (IMethodSelector)MathNode.GetService(typeof(IMethodSelector));
			if (ts != null)
			{
				return ts.GetUIEditorStyle(context);
			}
			return UITypeEditorEditStyle.None;
		}
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (edSvc != null)
			{
				edSvc.CloseDropDown();
				IMethodSelector ts = (IMethodSelector)MathNode.GetService(typeof(IMethodSelector));
				if (ts != null)
				{
					UITypeEditorEditStyle style = ts.GetUIEditorStyle(context);
					if (style == UITypeEditorEditStyle.DropDown)
					{
						IDataSelectionControl dropdown = ts.GetUIEditorDropdown(context, provider, value);
						if (dropdown != null)
						{
							edSvc.DropDownControl((Control)dropdown);
							object v = dropdown.UITypeEditorSelectedValue;
							if (v != null)
							{
								return v;
							}
						}
					}
					else if (style == UITypeEditorEditStyle.Modal)
					{
						IDataSelectionControl modal = ts.GetUIEditorModal(context, provider, value);
						if (modal != null)
						{
							if (edSvc.ShowDialog((Form)modal) == DialogResult.OK)
							{
								IMethodNode mn = context.Instance as IMethodNode;
								if (mn != null)
								{
									mn.SetFunction(modal.UITypeEditorSelectedValue);
									return mn.GetFunctionName();
								}
								return modal.UITypeEditorSelectedValue;
							}
						}
					}
				}
			}
			return value;
		}
		// Indicates whether the UITypeEditor supports painting a 
		// representation of a property's value.
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			IMethodSelector ts = (IMethodSelector)MathNode.GetService(typeof(IMethodSelector));
			if (ts != null)
			{
				return ts.GetPaintValueSupported(context);
			}
			return false;
		}
		// Draws a representation of the property's value.
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{
			IMethodSelector ts = (IMethodSelector)MathNode.GetService(typeof(IMethodSelector));
			if (ts != null)
			{
				ts.PaintValue(e);
			}
		}
	}
}
