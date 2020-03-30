using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using SmartGrocery.UseCase.Customer;
using Microsoft.Web.Http;

namespace SmartGrocery.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{api-version:apiVersion}/customers")]
    public class CustomersController : ApiController
    {
    }
}