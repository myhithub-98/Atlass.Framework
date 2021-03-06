﻿
using Atlass.Framework.Cache;
using Atlass.Framework.Common.IdHelper;
using Atlass.Framework.DbContext;
using Atlass.Framework.Generate;
using Atlass.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atlass.Framework.Generate
{
    public class GenerateContentAppService
    {
        private readonly  IFreeSql Sqldb;
        public GenerateContentAppService() {
            Sqldb =FreesqlDbInstance.GetInstance();
        }

        /// <summary>
        /// 获取新闻详情数据
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public ContentModel GetContentInfo(int contentId)
        {
            var model = Sqldb.Select<cms_content>()
                .Where(s=>s.id==contentId)
                .First(s => new ContentModel
                {
                    id = s.id,
                    title = s.title,
                    sub_title = s.sub_title,
                    content = s.content,
                    summary = s.summary,
                    cover_image = s.cover_image,
                    author = s.author,
                    source = s.source,
                    channel_id = s.channel_id,
                    ip_limit = s.ip_limit,
                    is_top = s.is_top,
                    tags = s.tags,
                    content_href = s.content_href,
                    hit_count = s.hit_count,
                    publish_time = s.insert_time,
                    last_edit_time = s.update_time
                });
            if (model != null)
            {
                model.navigation = GetChannels(model.channel_id);
            }
            return model;
        }


        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        private string GetChannels(int channelId)
        {
            List<string> channelNames = new List<string>();

            var channel = ChannelManagerCache.GetChannel(channelId);
            if (channel != null)
            {
                channelNames.Insert(0, channel.channel_name);
                int channelPid = channel.parent_id;
                while (channelPid > 0)
                {
                    channel= ChannelManagerCache.GetChannel(channelPid);
                    if (channel != null)
                    {
                        channelNames.Insert(0, channel.channel_name);
                        channelPid = channel.parent_id;
                    }
                }

            }

            if (channelNames.Count == 0)
            {
                return "首页";
            }

            return string.Join('>', channelNames);
        }
    }
}
