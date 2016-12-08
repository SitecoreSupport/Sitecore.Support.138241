using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Data.Wrappers;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Web;

namespace Sitecore.Support.Forms.Mvc.Services
{
    public class FormRepository : IRepository<FormModel>
    {
        private readonly Dictionary<Guid, FormModel> models = new Dictionary<Guid, FormModel>();

        public IRenderingContext RenderingContext
        {
            get;
            private set;
        }

        public FormRepository(IRenderingContext renderingContext)
        {
            Sitecore.Diagnostics.Assert.ArgumentNotNull(renderingContext, "renderingContext");
            this.RenderingContext = renderingContext;
        }

        public FormModel GetModel(Guid uniqueId)
        {
            if (uniqueId != Guid.Empty && this.models.ContainsKey(uniqueId))
            {
                return (FormModel)this.models[uniqueId].Clone();
            }
            string dataSource = this.RenderingContext.Rendering.DataSource;
            string text;
            if (!string.IsNullOrEmpty(dataSource) && Sitecore.Data.ID.IsID(dataSource))
            {
                text = dataSource;
            }
            else
            {
                text = this.RenderingContext.Rendering.Parameters[Sitecore.Forms.Mvc.Constants.FormId];
            }
            if (String.IsNullOrEmpty(text))
            {
                text = Sitecore.Configuration.Settings.GetSetting("WFM.EmptyForm");
            }
            if (!Sitecore.Data.ID.IsID(text))
            {
                return null;
            }
            Sitecore.Data.ID id = Sitecore.Data.ID.Parse(text);
            Sitecore.Data.Items.Item item = this.RenderingContext.Database.GetItem(id);
            Sitecore.Diagnostics.Assert.IsNotNull(item, "Form item is absent");
            FormModel formModel = new FormModel(uniqueId, item)
            {
                ReadQueryString = Sitecore.MainUtil.GetBool(this.RenderingContext.Rendering.Parameters[Sitecore.Forms.Mvc.Constants.ReadQueryString], false),
                QueryParameters = HttpUtility.ParseQueryString(Sitecore.Web.WebUtil.GetQueryString())
            };
            this.models.Add(uniqueId, formModel);
            return formModel;
        }

        public FormModel GetModel()
        {
            return this.GetModel(this.RenderingContext.Rendering.UniqueId);
        }
    }
}