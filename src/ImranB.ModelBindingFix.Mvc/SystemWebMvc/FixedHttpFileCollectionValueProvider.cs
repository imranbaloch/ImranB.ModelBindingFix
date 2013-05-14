using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImranB.ModelBindingFix.Mvc.SystemWebMvc
{
    public sealed class FixedHttpFileCollectionValueProvider : FixedDictionaryValueProvider<HttpPostedFileBase[]>
    {
        private static readonly Dictionary<string, HttpPostedFileBase[]> _emptyDictionary = new Dictionary<string, HttpPostedFileBase[]>();

        public FixedHttpFileCollectionValueProvider(ControllerContext controllerContext)
            : base(GetHttpPostedFileDictionary(controllerContext), CultureInfo.InvariantCulture)
        {
        }

        private static Dictionary<string, HttpPostedFileBase[]> GetHttpPostedFileDictionary(ControllerContext controllerContext)
        {
            HttpFileCollectionBase files = controllerContext.HttpContext.Request.Files;

            // fast-track common case of no files
            if (files.Count == 0)
            {
                return _emptyDictionary;
            }

            // build up the 1:many file mapping
            List<KeyValuePair<string, HttpPostedFileBase>> mapping = new List<KeyValuePair<string, HttpPostedFileBase>>();
            string[] allKeys = files.AllKeys;
            for (int i = 0; i < files.Count; i++)
            {
                string key = allKeys[i];
                if (key != null)
                {
                    HttpPostedFileBase file = ChooseFileOrNull(files[i]);
                    mapping.Add(new KeyValuePair<string, HttpPostedFileBase>(key, file));
                }
            }

            // turn the mapping into a 1:many dictionary
            var grouped = mapping.GroupBy(el => el.Key, el => el.Value, StringComparer.OrdinalIgnoreCase);
            return grouped.ToDictionary(g => g.Key, g => g.ToArray(), StringComparer.OrdinalIgnoreCase);
        }

        // helper that returns the original file if there was content uploaded, null if empty
        internal static HttpPostedFileBase ChooseFileOrNull(HttpPostedFileBase rawFile)
        {
            // case 1: there was no <input type="file" ... /> element in the post
            if (rawFile == null)
            {
                return null;
            }

            // case 2: there was an <input type="file" ... /> element in the post, but it was left blank
            if (rawFile.ContentLength == 0 && String.IsNullOrEmpty(rawFile.FileName))
            {
                return null;
            }

            // case 3: the file was posted
            return rawFile;
        }
    }
}
