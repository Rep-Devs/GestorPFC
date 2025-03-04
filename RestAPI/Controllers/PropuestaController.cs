using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestAPI.Data;
using RestAPI.Models.DTOs.PropuestaDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "alumno, profesor")]
    public class PropuestaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPropuestaRepository _propuestaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PropuestaController> _logger;

        public PropuestaController(IPropuestaRepository propuestaRepository, IMapper mapper, ILogger<PropuestaController> logger, ApplicationDbContext context)
        {
            _propuestaRepository = propuestaRepository;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        // GET: api/Propuesta
        // Si el usuario es alumno, se filtra por AlumnoId.
        // Si es profesor, se filtra comparando el nombre del departamento almacenado en la propuesta
        // con el nombre del departamento obtenido a partir del DepartamentoId en el token.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (User.IsInRole("alumno"))
            {
                var alumnoClaim = User.FindFirst("AlumnoId")?.Value;
                if (!int.TryParse(alumnoClaim, out int alumnoId))
                    return Unauthorized("No se encontró el AlumnoId en las claims.");

                var propuestas = await _propuestaRepository.GetAllAsync();
                var propuestasFiltradas = propuestas.Where(p => p.AlumnoId == alumnoId);
                var dtos = _mapper.Map<IEnumerable<PropuestaDto>>(propuestasFiltradas);
                return Ok(dtos);
            }
            else if (User.IsInRole("profesor"))
            {
                var deptClaim = User.FindFirst("DepartamentoId")?.Value;
                if (!int.TryParse(deptClaim, out int deptId))
                    return Unauthorized("No se encontró el DepartamentoId en las claims.");

                // Consultar el nombre del departamento a partir del DepartamentoId
                var dept = await _context.Departamentos.FirstOrDefaultAsync(d => d.Id == deptId);
                if (dept == null)
                    return NotFound("Departamento no encontrado.");

                var propuestas = await _propuestaRepository.GetAllAsync();
                var propuestasFiltradas = propuestas.Where(p => p.Departamento == dept.Nombre);
                var dtos = _mapper.Map<IEnumerable<PropuestaDto>>(propuestasFiltradas);
                return Ok(dtos);
            }
            else
            {
                return Forbid();
            }
        }

        // POST: api/Propuesta
        // Para alumnos, se asigna el AlumnoId.
        // Para profesores, se asigna el nombre del departamento (consultado a partir de DepartamentoId).
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropuestaDTO createDto)
        {
            if (User.IsInRole("alumno"))
            {
                var alumnoClaim = User.FindFirst("AlumnoId")?.Value;
                if (!int.TryParse(alumnoClaim, out int alumnoId))
                    return Unauthorized("No se encontró el AlumnoId en las claims.");

                var propuesta = _mapper.Map<Propuesta>(createDto);
                propuesta.AlumnoId = alumnoId;
                propuesta.FechaEnvio = System.DateTime.UtcNow;
                propuesta.Estado = EstadoPropuesta.StandBy;
                _context.Propuestas.Add(propuesta);
                await _context.SaveChangesAsync();

                var propuestaCreadaDto = _mapper.Map<PropuestaDto>(propuesta);
                return CreatedAtAction(nameof(GetAll), new { id = propuesta.Id }, propuestaCreadaDto);
            }
            else if (User.IsInRole("profesor"))
            {
                var deptClaim = User.FindFirst("DepartamentoId")?.Value;
                if (!int.TryParse(deptClaim, out int deptId))
                    return Unauthorized("No se encontró el DepartamentoId en las claims.");

                var dept = await _context.Departamentos.FirstOrDefaultAsync(d => d.Id == deptId);
                if (dept == null)
                    return NotFound("Departamento no encontrado.");

                var propuesta = _mapper.Map<Propuesta>(createDto);
                // Asignar el nombre del departamento a la propuesta
                propuesta.Departamento = dept.Nombre;
                propuesta.FechaEnvio = System.DateTime.UtcNow;
                propuesta.Estado = EstadoPropuesta.StandBy;
                _context.Propuestas.Add(propuesta);
                await _context.SaveChangesAsync();

                var propuestaCreadaDto = _mapper.Map<PropuestaDto>(propuesta);
                return CreatedAtAction(nameof(GetAll), new { id = propuesta.Id }, propuestaCreadaDto);
            }
            else
            {
                return Forbid();
            }
        }

        // PUT: api/Propuesta/{id}
        // Permite a profesores modificar propuestas de su departamento y a alumnos modificar sus propias propuestas.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PropuestaDto propuestaDto)
        {
            if (User.IsInRole("alumno"))
            {
                var alumnoClaim = User.FindFirst("AlumnoId")?.Value;
                if (!int.TryParse(alumnoClaim, out int alumnoId))
                    return Unauthorized("No se encontró el AlumnoId en las claims.");

                var propuesta = await _context.Propuestas.FirstOrDefaultAsync(p => p.Id == id && p.AlumnoId == alumnoId);
                if (propuesta == null)
                    return NotFound("Propuesta no encontrada o no pertenece al alumno.");

                _mapper.Map(propuestaDto, propuesta);
                _context.Propuestas.Update(propuesta);
                await _context.SaveChangesAsync();
                var updatedDto = _mapper.Map<PropuestaDto>(propuesta);
                return Ok(updatedDto);
            }
            else if (User.IsInRole("profesor"))
            {
                var deptClaim = User.FindFirst("DepartamentoId")?.Value;
                if (!int.TryParse(deptClaim, out int deptId))
                    return Unauthorized("No se encontró el DepartamentoId en las claims.");

                var dept = await _context.Departamentos.FirstOrDefaultAsync(d => d.Id == deptId);
                if (dept == null)
                    return NotFound("Departamento no encontrado.");

                var propuesta = await _context.Propuestas.FirstOrDefaultAsync(p => p.Id == id && p.Departamento == dept.Nombre);
                if (propuesta == null)
                    return NotFound("Propuesta no encontrada o no pertenece al departamento del profesor.");

                _mapper.Map(propuestaDto, propuesta);
                _context.Propuestas.Update(propuesta);
                await _context.SaveChangesAsync();
                var updatedDto = _mapper.Map<PropuestaDto>(propuesta);
                return Ok(updatedDto);
            }
            else
            {
                return Forbid();
            }
        }
    }
}
