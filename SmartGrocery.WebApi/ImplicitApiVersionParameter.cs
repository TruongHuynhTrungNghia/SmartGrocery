using Microsoft.Web.Http.Description;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace SmartGrocery.WebApi
{
    public class ImplicitApiVersionParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var description = apiDescription as VersionedApiDescription;

            if (description?.ApiVersion == null)
            {
                return;
            }

            var parameters = operation.parameters;

            if (parameters == null)
            {
                operation.parameters = parameters = new List<Parameter>();
            }

            var parameter = parameters.SingleOrDefault(x => x.name == "api-version");

            if (parameter == null)
            {
                parameter = new Parameter()
                {
                    name = "api-version",
                    required = true,
                    @in = "query",
                    type = "string"
                };

                parameters.Add(parameter);
            }

            parameter.@default = description.ApiVersion.ToString();
            parameter.description = "The reuqest API Version";
        }
    }
}