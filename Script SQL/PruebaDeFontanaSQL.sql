-- El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).


SELECT SUM(VD.TotalLinea) AS 'TOTAL VENTA 30 DIAS', SUM(VD.Cantidad) AS 'CANTIDAD TOTAL 30 DIAS' FROM VentaDetalle VD
INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
WHERE V.Fecha BETWEEN DATEADD(DAY, -30, GETDATE()) AND GETDATE()

-- El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
-- Indicar cuál es el producto con mayor monto total de ventas.
-- Indicar el local con mayor monto de ventas.
-- ¿Cuál es la marca con mayor margen de ganancias?

SELECT TOP 1 V.Fecha AS 'FECHA', MAX(TOTALLINEA) AS 'MONTO MAS ALTO', P.Nombre AS 'PRODUCTO MONTO MAS ALTO', L.Nombre 'LOCAL MAYOR MONTO VENTA', M.Nombre 'MARCA GANANCIA MAS ALTA' FROM VentaDetalle VD
INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
INNER JOIN Producto P ON VD.ID_Producto = P.ID_Producto
INNER JOIN Local L ON V.ID_Local = L.ID_Local
INNER JOIN Marca M ON P.ID_Marca = M.ID_Marca
WHERE V.Fecha BETWEEN DATEADD(DAY, -30, GETDATE()) AND GETDATE()
GROUP BY V.Fecha, VD.TotalLinea, P.Nombre, L.Nombre, M.Nombre
ORDER BY VD.TotalLinea DESC


-- ¿Cómo obtendrías cuál es el producto que más se vende en cada local?

SELECT Local , Producto
FROM (
    SELECT L.Nombre as Local, P.Nombre AS PRODUCTO,
        ROW_NUMBER() OVER (PARTITION BY L.Nombre ORDER BY COUNT(*) DESC) AS Ranking
    FROM VentaDetalle VD
	INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
    INNER JOIN Producto P ON VD.ID_Producto = P.ID_Producto
    INNER JOIN Local L ON V.ID_Local = L.ID_Local
    WHERE V.Fecha BETWEEN DATEADD(DAY, -30, GETDATE()) AND GETDATE()
    GROUP BY L.Nombre, P.Nombre
) AS VentasRanking
WHERE Ranking = 1



