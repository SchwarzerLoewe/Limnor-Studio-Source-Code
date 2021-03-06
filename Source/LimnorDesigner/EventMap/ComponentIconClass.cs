﻿/*
 
 * Author:	Bob Limnor (info@limnor.com)
 * Project: Limnor Studio
 * Item:	Visual Programming Language Implement
 * License: GNU General Public License v3.0
 
 */
using System;
using System.Collections.Generic;
using System.Text;
using XmlSerializer;
using System.Xml;
using VSPrj;
using XmlUtility;
using System.Windows.Forms;
using System.Drawing;
using LimnorDesigner.MenuUtil;
using MathExp;
using VPL;
using System.Globalization;
using WindowsUtility;

namespace LimnorDesigner.EventMap
{
	/// <summary>
	/// represents a custom class as an action executer for executing static methods
	/// </summary>
	public class ComponentIconClass : ComponentIconEvent
	{
		#region fields and constructors
		private LimnorProject _prj;
		private ClassPointer _root;
		public ComponentIconClass()
		{
			OnSetImage();
		}
		#endregion

		#region Properties
		public override LimnorProject Project
		{
			get
			{
				return _prj;
			}
		}
		/// <summary>
		/// not for creating context menu
		/// </summary>
		public override bool IsForComponent
		{
			get
			{
				return false;
			}
		}
		public override ClassPointer RootClassPointer
		{
			get
			{
				return _root;
			}
		}
		public ClassPointer OwnerClassPointer
		{
			get
			{
				if (this.ClassId != 0)
				{
					if (_prj == null)
					{
						if (Designer != null && Designer.ObjectMap != null)
						{
							_prj = Designer.ObjectMap.Project;
						}
					}
					if (_prj != null)
					{
						return LimnorDesigner.ClassPointer.CreateClassPointer(this.ClassId, _prj);
					}
				}
				return null;
			}
		}
		protected override bool IsClassType
		{
			get
			{
				return true;
			}
		}
		#endregion

		#region Methods
		protected override void OnCreateContextMenu(ContextMenu mnu, Point location)
		{
			base.OnCreateContextMenu(mnu, location);
			if (_root.ClassId != this.ClassId)
			{
				if (mnu.MenuItems.Count > 0)
				{
					mnu.MenuItems.Add("-");
				}
				MenuItem mi = new MenuItemWithBitmap("Delete", mi_delete, Resources._cancel.ToBitmap());
				mnu.MenuItems.Add(mi);
			}
		}
		public override void OnWriteToXmlNode(IXmlCodeWriter writer, XmlNode node)
		{
			base.OnWriteToXmlNode(writer, node);
		}
		public override void OnReadFromXmlNode(IXmlCodeReader reader0, XmlNode node)
		{
			base.OnReadFromXmlNode(reader0, node);
			XmlObjectReader reader = (XmlObjectReader)reader0;
			_prj = reader.ObjectList.Project;
			_root = reader.ObjectList.RootPointer as ClassPointer;
			ClassPointer cp = OwnerClassPointer;
			if (cp != null)
			{
				this.ClassPointer = cp;
				SetLabelText(cp.Name);
				OnSetImage();
			}
		}
		protected override void OnInit(ILimnorDesigner designer, IClass pointer)
		{
			_prj = designer.Project;
			_root = designer.GetRootId();
			ClassPointer cp = OwnerClassPointer;
			if (cp != null)
			{
				this.ClassPointer = cp;
				SetLabelText(cp.Name);
				OnSetImage();
			}
		}
		protected override void OnSetImage()
		{
			ClassPointer cp = OwnerClassPointer;
			if (cp != null)
			{
				if (cp.ImageIcon != null)
				{
					SetIconImage(cp.ImageIcon);
				}
			}
		}
		protected override LimnorContextMenuCollection CreateMenuData()
		{
			return LimnorContextMenuCollection.GetStaticMenuCollection(this.ClassPointer);
		}
		public override bool IsForThePointer(IObjectPointer pointer)
		{
			ClassPointer cp = pointer as ClassPointer;
			if (cp != null)
			{
				return (cp.ClassId == this.ClassId);
			}
			return false;
		}
		public override bool IsActionExecuter(IAction act, ClassPointer root)
		{
			ClassPointer cp = root.GetExternalExecuterClass(act);
			if (cp != null)
			{
				return (cp.ClassId == this.ClassId);
			}
			return false;
		}
		public override void OnPostSerialize(ObjectIDmap objMap, XmlNode objectNode, bool saved, object serializer)
		{
			_prj = objMap.Project;
			_root = objMap.RootPointer as ClassPointer;
			ClassPointer cp = OwnerClassPointer;
			if (cp != null)
			{
				SetLabelText(cp.Name);
			}
		}
		public override bool OnDeserialize(ClassPointer root, ILimnorDesigner designer)
		{
			_prj = root.Project;
			_root = root;
			ClassPointer cp = OwnerClassPointer;
			if (cp != null)
			{
				SetLabelText(cp.Name);
				return true;
			}
			return false;
		}
		#endregion

		#region private methods
		private void mi_delete(object sender, EventArgs e)
		{
			XmlNodeList nl = _root.XmlData.SelectNodes(string.Format(CultureInfo.InvariantCulture,
				"//{0}/{1}//*[@{2}='{3}']", XmlTags.XML_ACTIONS, XmlTags.XML_ACTION, XmlTags.XMLATT_ClassID, this.ClassPointer.ClassId));
			if (nl != null && nl.Count > 0)
			{
				MessageBox.Show(this.FindForm(), "Cannot delete this component. It is being used in actions", "Delete component", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				this.RemoveIcon();
				if (DataXmlNode != null)
				{
					XmlNode np = DataXmlNode.ParentNode;
					np.RemoveChild(DataXmlNode);
				}
				ILimnorDesignPane dp = _prj.GetTypedData<ILimnorDesignPane>(_root.ClassId);
				if (dp != null)
				{
					dp.Loader.NotifyChanges();
				}
			}
		}
		#endregion

	}
}
