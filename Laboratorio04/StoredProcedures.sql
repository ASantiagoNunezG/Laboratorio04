USE Neptuno
/****** Listaddo de StoredProcedures, Procedimientos almacenados ******/
CREATE PROC ListarProductos
AS
BEGIN
SELECT idproducto
      ,nombreProducto
      ,idProveedor
      ,idCategoria
      ,cantidadPorUnidad
      ,precioUnidad
      ,unidadesEnExistencia
      ,unidadesEnPedido
      ,nivelNuevoPedido
      ,suspendido
      ,categoriaProducto
  FROM productos

END

CREATE PROC ListarCategorias
AS
BEGIN
SELECT idcategoria
      ,nombrecategoria
      ,descripcion
      ,Activo
      ,CodCategoria
  FROM categorias
END


CREATE PROC ListarProveedores
AS
BEGIN
SELECT idProveedor
      ,nombreCompañia
      ,nombrecontacto
      ,cargocontacto
      ,direccion
      ,ciudad
      ,region
      ,codPostal
      ,pais
      ,telefono
      ,fax
      ,paginaprincipal
  FROM proveedores
END


CREATE OR ALTER PROC ListarProveedoresFiltrado
    @nombreContacto NVARCHAR(100) = NULL,
    @ciudad NVARCHAR(100) = NULL
AS
BEGIN
    SELECT idProveedor,
           nombreCompañia,
           nombrecontacto,
           cargocontacto,
           direccion,
           ciudad,
           region,
           codPostal,
           pais,
           telefono,
           fax,
           paginaprincipal
    FROM proveedores
    WHERE (@nombreContacto IS NULL OR nombrecontacto LIKE '%' + @nombreContacto + '%')
      AND (@ciudad IS NULL OR ciudad LIKE '%' + @ciudad + '%')
END
/******
ListarProveedoresFiltrado 'Mayumi'
ListarProveedoresFiltrado ' ','Tokyo'******/

CREATE PROC ListarPedidosConDetallesPorFecha
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SELECT p.IdPedido,
           p.IdCliente,
           p.IdEmpleado,
           p.FechaPedido,
           p.FechaEntrega,
           p.FechaEnvio,
           p.FormaEnvio,
           p.Cargo,
           p.Destinatario,
           p.DireccionDestinatario,
           p.CiudadDestinatario,
           p.RegionDestinatario,
           p.CodPostalDestinatario,
           p.PaisDestinatario,
           d.IdProducto,
           d.PrecioUnidad,
           d.Cantidad,
           d.Descuento
    FROM Pedidos p
    INNER JOIN DetallesDePedidos d ON p.IdPedido = d.IdPedido
    WHERE p.FechaPedido BETWEEN @FechaInicio AND @FechaFin
END
