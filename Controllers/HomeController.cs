using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using autbearer.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using autbearer.Services;
using autbearer.Repositories;

[HttpPost]
[Route("login")]
 async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
{
    // Recupera o usuário
    var user = UserRepository.Get(model.Username, model.Password);

    // Verifica se o usuário existe
    if (user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

    // Gera o Token
    var token = TokenService.GenerateToken(user);

    // Oculta a senha
    user.Password = "";
    
    // Retorna os dados
    return new
    {
        user = user,
        token = token
    };
}

ActionResult<dynamic> NotFound(object p)
{
    throw new NotImplementedException();
}