using AutoMapper;
using RestAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPI.Controllers;
using RestAPI.Models.DTOs.PropuestaDTO;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : BaseController<Propuesta, PropuestaDto, CreatePropuestaDTO>
    {
        public PropuestaController(IPropuestaRepository propuestaRepository, IMapper mapper, ILogger<PropuestaController> logger)
            : base(propuestaRepository, mapper, logger)
        {
        }
    }
}
