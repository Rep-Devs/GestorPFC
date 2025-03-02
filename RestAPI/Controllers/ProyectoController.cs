using AutoMapper;
using RestAPI.Models.DTOs.ProyectoDTO;
using RestAPI.Models.Entity;  
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPI.Controllers;

using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : BaseController<Proyecto, ProyectoDto, CreateProyectoDTO>
    {
        public ProyectoController(IProyectoRepository proyectoRepository, IMapper mapper, ILogger<ProyectoController> logger)
            : base(proyectoRepository, mapper, logger)
        {
        }
    }
}
