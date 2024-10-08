﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServicesInterface
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> listaProductos();
        void AgregarProducto(Producto producto);
        Producto EditarProducto(Producto producto);
        Producto ObtenerProductoPorID(int id);
        void CambiarEstado(int id);
        List<Restaurante> listaRestaurantes();
        List<TipoProducto> listaTipoProducto();
    }
}
