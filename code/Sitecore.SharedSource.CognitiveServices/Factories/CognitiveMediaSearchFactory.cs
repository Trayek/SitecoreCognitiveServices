﻿using System;
using Sitecore.Data.Items;
using Sitecore.SharedSource.CognitiveServices.Foundation;
using Sitecore.SharedSource.CognitiveServices.Models;
using Sitecore.SharedSource.CognitiveServices.Models.Search;
using Sitecore.SharedSource.CognitiveServices.Search;

namespace Sitecore.SharedSource.CognitiveServices.Factories
{
    public class CognitiveMediaSearchFactory : ICognitiveMediaSearchFactory
    {
        protected readonly IReflectionUtilWrapper ReflectionUtil;

        public CognitiveMediaSearchFactory(IReflectionUtilWrapper reflectionUtil)
        {
            ReflectionUtil = reflectionUtil;
        }

        public virtual ICognitiveMediaSearch Create()
        {
            return ReflectionUtil.CreateObjectFromSettings<ICognitiveMediaSearch>("CognitiveService.Types.ICognitiveMediaSearch");
        }

        public virtual ICognitiveMediaSearch Create(string db, string language)
        {
            var r = Create();
            r.Database = db;
            r.Language = language;

            return r;
        }

        public virtual ICognitiveMediaSearchJsonResult CreateMediaSearchJsonResult(ISitecoreDataService dataService, ICognitiveSearchResult searchResult)
        {
            var obj = ReflectionUtil.CreateObjectFromSettings<ICognitiveMediaSearchJsonResult>("CognitiveService.Types.ICognitiveMediaSearchJsonResult");
            
            MediaItem m = dataService.GetItemByUri(searchResult.UniqueId);

            try
            {
                obj.Url = $"-/media/{m.ID.Guid:N}.ashx";
            }
            catch (Exception ex)
            {
                obj.Url = string.Empty;
            }
            try
            {
                obj.Alt = m.Alt;
            }
            catch (Exception ex)
            {
                obj.Alt = string.Empty;
            }

            return obj;
        }
    }
}