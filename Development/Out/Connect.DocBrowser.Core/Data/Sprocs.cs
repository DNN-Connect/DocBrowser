
using System;
using System.Collections.Generic;
using DotNetNuke.Data;

namespace Connect.DocBrowser.Core.Data
{
    public class Sprocs
    {
        // SELECT
        //  i2.*
        // FROM dbo.Connect_DocBrowser_Items i2
        // INNER JOIN
        // (SELECT
        //  i.Topic,
        //  MAX(i.Version) MaxVersion
        // FROM dbo.Connect_DocBrowser_Items i
        // GROUP BY i.ModuleId, i.Locale, i.Version, i.Edition, i.Topic
        // HAVING
        //  i.ModuleId=@ModuleId
        //  AND i.Locale=@Locale
        //  AND i.Version <= @Version
        //  AND (i.Edition & @Edition > 0 OR i.Edition=0)) x ON i2.Topic=x.Topic AND i2.Version=x.MaxVersion
        // WHERE
        //  i2.ModuleId=@ModuleId
        //  AND i2.Locale=@Locale
        //  AND (i2.Edition & @Edition > 0 OR i2.Edition=0)
        //  AND i2.Topic=@Topic
        // ;  
        public static IEnumerable<Item> GetTopic(int moduleId, string locale, string version, int edition, string topic)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<Item>(System.Data.CommandType.StoredProcedure,
                    "Connect_DocBrowser_GetTopic",
                    moduleId, locale, version, edition, topic);
            }
        }

        // SELECT
        //  i2.Topic
        // FROM dbo.Connect_DocBrowser_Items i2
        // INNER JOIN
        // (SELECT
        //  i.Topic,
        //  MAX(i.Version) MaxVersion
        // FROM dbo.Connect_DocBrowser_Items i
        // GROUP BY i.ModuleId, i.Locale, i.Version, i.Edition, i.Topic
        // HAVING
        //  i.ModuleId=@ModuleId
        //  AND i.Locale=@Locale
        //  AND i.Version <= @Version
        //  AND (i.Edition & @Edition > 0 OR i.Edition=0)) x ON i2.Topic=x.Topic AND i2.Version=x.MaxVersion
        // WHERE
        //  i2.ModuleId=@ModuleId
        //  AND i2.Locale=@Locale
        //  AND (i2.Edition & @Edition > 0 OR i2.Edition=0)
        // ;  
        public static IEnumerable<object> GetTopicList(int moduleId, string locale, string version, int edition)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<object>(System.Data.CommandType.StoredProcedure,
                    "Connect_DocBrowser_GetTopicList",
                    moduleId, locale, version, edition);
            }
        }

    }
}
