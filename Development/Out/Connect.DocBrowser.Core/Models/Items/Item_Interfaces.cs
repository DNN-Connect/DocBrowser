
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Connect.DocBrowser.Core.Models.Items
{
    public partial class Item : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
   ModuleId = Convert.ToInt32(Null.SetNull(dr["ModuleId"], ModuleId));
   Topic = Convert.ToString(Null.SetNull(dr["Topic"], Topic));
   Locale = Convert.ToString(Null.SetNull(dr["Locale"], Locale));
   Edition = Convert.ToInt32(Null.SetNull(dr["Edition"], Edition));
   Version = Convert.ToString(Null.SetNull(dr["Version"], Version));
   Title = Convert.ToString(Null.SetNull(dr["Title"], Title));
   ParentTopic = Convert.ToString(Null.SetNull(dr["ParentTopic"], ParentTopic));
   PreviousTopic = Convert.ToString(Null.SetNull(dr["PreviousTopic"], PreviousTopic));
   NextTopic = Convert.ToString(Null.SetNull(dr["NextTopic"], NextTopic));
   Contents = Convert.ToString(Null.SetNull(dr["Contents"], Contents));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return Null.NullInteger; }
            set { }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "moduleid": // Int
     return ModuleId.ToString(strFormat, formatProvider);
    case "topic": // VarChar
     return PropertyAccess.FormatString(Topic, strFormat);
    case "locale": // VarChar
     return PropertyAccess.FormatString(Locale, strFormat);
    case "edition": // Int
     return Edition.ToString(strFormat, formatProvider);
    case "version": // VarChar
     return PropertyAccess.FormatString(Version, strFormat);
    case "title": // NVarChar
     return PropertyAccess.FormatString(Title, strFormat);
    case "parenttopic": // VarChar
     if (ParentTopic == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(ParentTopic, strFormat);
    case "previoustopic": // VarChar
     if (PreviousTopic == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(PreviousTopic, strFormat);
    case "nexttopic": // VarChar
     if (NextTopic == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(NextTopic, strFormat);
    case "contents": // NVarCharMax
     if (Contents == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Contents, strFormat);
                default:
                    propertyNotFound = true;
                    break;
            }

            return Null.NullString;
        }

        [IgnoreColumn()]
        public CacheLevel Cacheability
        {
            get { return CacheLevel.fullyCacheable; }
        }
        #endregion

    }
}

