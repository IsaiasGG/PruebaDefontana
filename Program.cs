using PruebaDefontana.PruebaDefontana.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontana
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new VentasModel())
            {
                var fechaLimite = DateTime.Now.AddDays(-30);
                var fechaActual = DateTime.Now;

                var datosVentas = (from vd in dbContext.VentaDetalles
                                   join v in dbContext.Ventas on vd.ID_Venta equals v.ID_Venta
                                   join p in dbContext.Productos on vd.ID_Producto equals p.ID_Producto
                                   join l in dbContext.Locales on v.ID_Local equals l.ID_Local
                                   join m in dbContext.Marcas on p.ID_Marca equals m.ID_Marca
                                   where v.Fecha >= fechaLimite && v.Fecha <= fechaActual 
                                   select new {
                                       vd.TotalLinea,
                                       vd.Cantidad,
                                       v.Fecha,
                                       Producto = p.Nombre,
                                       Local = l.Nombre,
                                       Marca = m.Nombre
                                   }).ToList();
                                   

                var montoTotal30 = datosVentas.Sum(mt => mt.TotalLinea);
                Console.WriteLine("Total Ventas Ultimos 30 dias: " + montoTotal30);

                var CantidadTotal30 = datosVentas.Sum(ct => ct.Cantidad);
                Console.WriteLine("Cantidad Ventas Ultimos 30 dias: " + CantidadTotal30);

                var ventaMasAlta30 = datosVentas.OrderByDescending(va => va.TotalLinea).FirstOrDefault();
                Console.WriteLine("Venta mas alta Ultimos 30 dias: " + ventaMasAlta30.TotalLinea);

                var FechaVentaMasAlta = datosVentas.OrderByDescending(va => va.TotalLinea).FirstOrDefault();
                Console.WriteLine("Fecha Venta mas alta Ultimos 30 dias: " + FechaVentaMasAlta.Fecha);

                var ProductoMontoMasAlto = datosVentas.OrderByDescending(va => va.TotalLinea).FirstOrDefault();
                Console.WriteLine("Producto Venta mas alta Ultimos 30 dias: " + ProductoMontoMasAlto.Producto);

                var LocalMontoMasAlto = datosVentas.OrderByDescending(va => va.TotalLinea).FirstOrDefault();
                Console.WriteLine("Local Venta mas alta Ultimos 30 dias: " + LocalMontoMasAlto.Local);

                var MarcaMontoMasAlto = datosVentas.OrderByDescending(va => va.TotalLinea).FirstOrDefault();
                Console.WriteLine("Marca Venta mas alta Ultimos 30 dias: " + MarcaMontoMasAlto.Marca);

                var productosMasVendidosPorLocal = datosVentas
                                                    .GroupBy(v => v.Local)
                                                    .Select(g => new
                                                    {
                                                        Local = g.Key,
                                                        ProductoMasVendido = g.GroupBy(v => v.Producto)
                                                            .OrderByDescending(gp => gp.Count())
                                                            .FirstOrDefault()?.Key
                                                    })
                                                    .ToList();

                foreach (var item in productosMasVendidosPorLocal)
                {
                    Console.WriteLine("Local: " + item.Local);
                    Console.WriteLine("Producto más vendido: " + item.ProductoMasVendido);
                    Console.WriteLine("------------------------------------");
                }

                Console.ReadKey();
            }

          

        }
    }
}
