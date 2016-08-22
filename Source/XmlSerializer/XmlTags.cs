﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XmlSerializer
{
    sealed class XmlTags
    {
        //Resource Aux
        public const string XML_BINS = "Binaries";
        public const string XMLATT_ResId = "resId";
        public const string XMLATT_filename = "filename";
        //
        public const string XML_TEXTDATA = "TextData";
        //
        public const string XML_HANDLER = "Handler";
        public const string XML_HANDLERLISTS = "HandlerList";
        public const string XML_DESCRIPT = "Description";
        public const string XML_SHORTDESCRIPT = "ShortDesc";
        public const string XML_LIBTYPE = "LibType";
        //public const string XMLATT_VARID = "varId";
        public const string XMLATT_ValueID = "valueId";
        public const string XMLATT_ComponentID = "objectID";
        public const string XMLATT_ClassID = "ClassID";
        public const string XMLATT_ScopeId = "scopeId";
        public const string XMLATT_Type = "type";

        public const string XML_TYPE = "Type";
        public const string XML_ICON = "Icon";
        public const string XML_DATATYPE = "DataType";
        public const string XML_RETURNTYPE = "ReturnType";
        public const string XML_RETURNPORT = "ReturnPort";
        public const string XML_ACTION = "Action";
        public const string XML_ACTIONLIST = "Actions";
        public const string XML_METHOD = "Method";
        public const string XML_METHODS = "Methods";
        public const string XML_METHODOWNER = "MethodOwner";
        public const string XML_METHODREF = "MethodRef";
        public const string XML_EVENT = "Event";
        public const string XML_PARAM = "Parameter";
        public const string XML_PARAMLIST = "Parameters";
        public const string XML_PROPERTYLIST = "PropertyList";
        public const string XML_EVENTLIST = "EventList";
        public const string XML_PROPERTY = "Property";
        public const string XML_CONSTRUCTORS = "Constructors";
        public const string XML_ATTRIBUTES = "Attributes";
        public const string XML_INTERFACES = "Interfaces";
        public const string XML_PORT = "Port";
        public const string XML_DataFlowViewer = "DataFlowView";
        public const string XML_DataLink = "DataLink";
        public const string XML_Source = "Source";
        public const string XML_Destination = "Destination";
        public const string XML_Owner = "Owner";
        //
        public const string XMLATT_Creator = "creator";
        public const string XMLATT_DataLinkType = "flowType";
        //public const string XML_CUSTOMPROPERTY = "Property";
        //
        public const string XMLATT_NAME = "name";
        //public const string XMLATT_type = "type";
        public const string XMLATT_Guid = "guid";
        public const string XMLATT_NAMESPACE = "namespace";
        public const string XMLATT_STATIC = "static";
        public const string XMLATT_CONSTRUCTOR = "isConstructor";
        public const string XMLATT_EDITORRECT = "editorRect";
        public const string XMLATT_IsType = "isType";
        public const string XMLATT_ActionID = "ActionId";
        public const string XMLATT_MethodID = "MethodID";
        public const string XMLATT_PropID = "propertyID";
        public const string XMLATT_IsNull = "isNull";
        //
        //serialize
        public const string XML_Binary = "Binary";
        public const string XML_InstanceDescriptor = "InstanceDescriptor";
        //public const string XML_Property = "Property";
        public const string XML_ObjProperty = "ObjProperty";
        public const string XML_Argument = "Argument";
        public const string XML_Reference = "Reference";
        public const string XML_Item = "Item";
        public const string XML_Object = "Object";
        public const string XML_Data = "Data"; //for IXmlNodeSerialization/IXmlNodeSerializable value
        public const string XML_External = "External";
        public const string XML_ClassRef = "ClassRef";
        public const string XML_Content = "Content";
        public const string XML_StaticList = "StaticList";
        public const string XML_StaticValue = "StaticValue";
        public const string XMLATT_member = "member";
        public const string XMLATT_memberId = "memberId";
        public const string XMLATT_designMode = "designMode";
        //
        public const string STARTERCLASS = "Starter";
        public const string CONSTRUCTOR_METHOD = ".ctor";
        public const string RAIS_R = "R";
        public const string RAIS_A = "A";
        public const string RAIS_I = "I";
        public const string RAIS_S = "S";
        public const string RAIS_B = "BaseType";
        public const string RAIS_PRJ = "Project";
        private XmlTags()
        {
        }
    }
}
