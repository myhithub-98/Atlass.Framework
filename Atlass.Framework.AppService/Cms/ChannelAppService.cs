﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Website: http://www.freesql.net
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atlass.Framework.Common;
using Atlass.Framework.Models;
using Atlass.Framework.ViewModels.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Atlass.Framework.AppService.Cms
{


    public partial class ChannelAppService {
        private readonly IFreeSql Sqldb;
        public ChannelAppService(IServiceProvider service)
        {
            Sqldb = service.GetRequiredService<IFreeSql>();
        }


        /// <summary>
        ///获取数据列表
        /// </summary>
        /// <param name="param"></param>

        public List<cms_channel> GetData()
        {
            var query = Sqldb.Select<cms_channel>()
                    .OrderBy(s => s.sort_num).ToList();
            return query;
        }

        /// <summary>
        ///新增数据
        /// </summary>
        /// <param name="dto"></param>

        public void Insert(cms_channel dto)
        {
            Sqldb.Insert(dto).ExecuteAffrows();
        }

        /// <summary>
        ///更新数据
        /// </summary>
        /// <param name="dto"></param>

        public void Update(cms_channel dto)
        {
            Sqldb.Update<cms_channel>().SetSource(dto).IgnoreColumns(s=>new { s.insert_time,s.insert_id}).ExecuteAffrows();
        }

        /// <summary>
        ///获取单条数据
        /// </summary>
        /// <param name="id"></param>

        public cms_channel GetDataById(int id)
        {
            return Sqldb.Select<cms_channel>().Where(s => s.id == id).First();
        }

        /// <summary>
        ///批量删除
        /// </summary>
        /// <param name="ids"></param>

        public void DelByIds(int id)
        {
            Sqldb.Delete<cms_channel>().Where(s => s.id==id).ExecuteAffrows();
        }

        public List<ZtreeSelIntDto> ChannelZtree()
        {
            var list = Sqldb.Select<cms_channel>().ToList(s => new ZtreeSelIntDto
            {
                id = s.id,
                name = s.channel_name,
                pId = s.parent_id
            });
            list.Insert(0, new ZtreeSelIntDto { id = 0, name = "请选择" });
            return list;
        }
    }

}


