-- El total de ventas de los �ltimos 30 d�as (monto total y cantidad total de ventas).


SELECT SUM(VD.TotalLinea) AS 'TOTAL VENTA 30 DIAS', SUM(VD.Cantidad) AS 'CANTIDAD TOTAL 30 DIAS' FROM VentaDetalle VD
INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
WHERE V.Fecha BETWEEN DATEADD(DAY, -30, GETDATE()) AND GETDATE()

-- El d�a y hora en que se realiz� la venta con el monto m�s alto (y cu�l es aquel monto).
-- Indicar cu�l es el producto con mayor monto total de ventas.
-- Indicar el local con mayor monto de ventas.
-- �Cu�l es la marca con mayor margen de ganancias?

SELECT TOP 1 V.Fecha AS 'FECHA', MAX(TOTALLINEA) AS 'MONTO MAS ALTO', P.Nombre AS 'PRODUCTO MONTO MAS ALTO', L.Nombre 'LOCAL MAYOR MONTO VENTA', M.Nombre 'MARCA GANANCIA MAS ALTA' FROM VentaDetalle VD
INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
INNER JOIN Producto P ON VD.ID_Producto = P.ID_Producto
INNER JOIN Local L ON V.ID_Local = L.ID_Local
INNER JOIN Marca M ON P.ID_Marca = M.ID_Marca
WHERE V.Fecha BETWEEN DATEADD(DAY, -30, GETDATE()) AND GETDATE()
GROUP BY V.Fecha, VD.TotalLinea, P.Nombre, L.Nombre, M.Nombre
ORDER BY VD.TotalLinea DESC


-- �C�mo obtendr�as cu�l es el producto que m�s se vende en cada local?

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



