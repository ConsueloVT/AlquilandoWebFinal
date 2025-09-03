using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
namespace AL.Repositorios;

public class TarjetaRepositorio : ITarjetaRepositorio
{
    public int Agregar(Tarjeta t)
    {
        using (var db = new EntidadesContext())
        {
            db.Tarjetas.Add(t);
            db.SaveChanges();
            return t.Id;
        }
    }

    public String ObtenerPorIdUltimosCuatro(int id)
    {
        using (var db = new EntidadesContext())
        {
            String numero = "****";
            var tarjeta = db.Tarjetas.Find(id);
            if (tarjeta != null && tarjeta.Numero != null)
            {
                numero += new string(tarjeta.Numero.TakeLast(4).ToArray());
            }
            return numero;
        }
    }

    public Tarjeta? ObtenerPorId(int id)
    {
        using (var db = new EntidadesContext())
        {
            return db.Tarjetas.Find(id);
        }
    }
    public void Actualizar(Tarjeta t)
    {
        using (var db = new EntidadesContext())
        {
            db.Tarjetas.Update(t);
            db.SaveChanges();
        }
    }

}
