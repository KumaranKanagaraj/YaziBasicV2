using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Helpers
{
    public static class ResourceUri
    {
        public static string CreateResourceUri(ResourceParameters resourceParameters,
            IUrlHelper _urlHelper,
            string methodName,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link(methodName,
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link(methodName,
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link(methodName,
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });

            }
        }

    }
}
