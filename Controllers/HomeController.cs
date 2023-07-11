﻿using Microsoft.AspNetCore.Mvc;

namespace TP06Elecciones.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.ListaPartidos = BD.ListarPartidos();
        return View();
    }   

    public IActionResult VerDetallePartido(int idPartido){
        ViewBag.Partido = BD.VerInfoPartido(idPartido);
        ViewBag.ListaCandidatos = BD.ListarCandidatos(idPartido);
        return View();
    }

    public IActionResult VerDetalleCandidato(int idCandidato){
        ViewBag.Candidato = BD.VerInfoCandidato(idCandidato);
        return View();
    }

    public IActionResult AgregarCandidato(int idPartido){
        ViewBag.IdPartido = idPartido;
        return View();
    }

    [HttpPost] public IActionResult GuardarCandidato(Candidato can){
        BD.AgregarCandidato(can);
        ViewBag.Partido = BD.VerInfoPartido(can.IdPartido);
        ViewBag.ListaCandidatos = BD.ListarCandidatos(can.IdPartido);
        return View("VerDetallePartido");
    }

    public IActionResult EliminarCandidato(int idCandidato, int idPartido){
        BD.EliminarCandidato(idCandidato);
        ViewBag.Partido = BD.VerInfoPartido(idPartido);
        ViewBag.ListaCandidatos = BD.ListarCandidatos(idPartido);
        return View("VerDetallePartido");
    }

    public IActionResult AgregarPartido(){
        return View();
    }

    [HttpPost]public IActionResult GuardarPartido(Partido par){
        BD.AgregarPartido(par);
        ViewBag.ListaPartidos = BD.ListarPartidos();
        return View("Index");
    }

    public IActionResult EliminarPartido(int idPartido){
        BD.EliminarPartido(idPartido);
        ViewBag.ListaPartidos = BD.ListarPartidos();
        return View("Index");
    }

    public IActionResult Elecciones(){
        return View();
    }

    public IActionResult Creditos(){
        return View();
    }

}
