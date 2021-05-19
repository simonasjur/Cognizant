using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using System.Net.Http;
using AutoMapper;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly ChallengeContext _context;
        private readonly IMapper _mapper;

        public ChallengeController(ChallengeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // POST: api/Challenge/submitTask
        [HttpPost("submitTask")]
        public async Task<ActionResult<Challenge>> SubmitChallenge(ChallengeCreateDTO challengeCreateDTO)
        {
            var result = await PostSolutionAsync(challengeCreateDTO.SolutionCode);

            if (result != null)
            {
                var challenge = _mapper.Map<Challenge>(challengeCreateDTO);
                challenge.Output = result.Output;

                _context.Challenges.Add(challenge);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetChallenge", "Challenge", new { id = challenge.Id }, challenge);
            }
            return NotFound();
        }

        // GET: api/Challenges/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Challenge>> GetChallenge(long id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
                return NotFound();

            return challenge;
        }

        public async Task<Result> PostSolutionAsync(string code)
        {
            var solutionCode = "using System;class Program{static void Main(string[] args){" + code + "}}";

            using var client = new HttpClient();

            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            var input = new CompilerInput("ec6919b7761460c82cf0d5721bba8a24",
                                            "78c43200e045eaeebde1a588ac224349742832877d2b4948360e1d42b1894db9",
                                            solutionCode,
                                            "csharp",
                                            "0");
            var requestBody = System.Text.Json.JsonSerializer.Serialize(input, options);
            var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("https://api.jdoodle.com/v1/execute", httpContent);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Result>(responseContent);
            }
            return null;
        }
    }
}
