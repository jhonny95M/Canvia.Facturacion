USE [PruebaTecnicaCanvia]
GO
/****** Object:  Table [dbo].[CLIENTE]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTE](
	[ClienteID] [int] NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACTURA_CABECERA]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACTURA_CABECERA](
	[ClienteID] [int] NULL,
	[FechaEmision] [date] NULL,
	[Total] [decimal](10, 2) NULL,
	[FacturaID] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [tinyint] NOT NULL,
	[Descripcion] [varchar](299) NULL,
PRIMARY KEY CLUSTERED 
(
	[FacturaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACTURA_DETALLE]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACTURA_DETALLE](
	[DetalleID] [int] NOT NULL,
	[FacturaID] [int] NOT NULL,
	[Descripcion] [varchar](255) NULL,
	[Cantidad] [int] NULL,
	[PrecioUnitario] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[DetalleID] ASC,
	[FacturaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FACTURA_CABECERA] ADD  CONSTRAINT [DF_ESTADO_FACTURA_CABECERA]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[FACTURA_CABECERA]  WITH CHECK ADD FOREIGN KEY([ClienteID])
REFERENCES [dbo].[CLIENTE] ([ClienteID])
GO
ALTER TABLE [dbo].[FACTURA_DETALLE]  WITH CHECK ADD FOREIGN KEY([FacturaID])
REFERENCES [dbo].[FACTURA_CABECERA] ([FacturaID])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCliente]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarCliente]
    @ClienteID INT,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Email VARCHAR(100)
AS
BEGIN
    UPDATE CLIENTE
    SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email
    WHERE ClienteID = @ClienteID;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarFacturaCabecera]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarFacturaCabecera]
    @FacturaID INT,
    @ClienteID INT,
    @FechaEmision DATE,
    @Total DECIMAL(10, 2),
    @Descripcion VARCHAR(299) = null
AS
BEGIN
    --SET NOCOUNT ON;

    UPDATE [dbo].[FACTURA_CABECERA]
    SET
        [ClienteID] = @ClienteID,
        [FechaEmision] = @FechaEmision,
        [Total] = @Total,
        [Descripcion] = @Descripcion
    WHERE
        [FacturaID] = @FacturaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[AnularFactura]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AnularFactura]
    @FacturaID INT
AS
BEGIN
    UPDATE [dbo].[FACTURA_CABECERA]
   SET [estado] = 0
	  WHERE FacturaID = @FacturaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarClienteYFacturas]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarClienteYFacturas]
    @ClienteID INT
AS
BEGIN
    DELETE FROM CLIENTE_FACTURA WHERE ClienteID = @ClienteID;
    DELETE FROM CLIENTE WHERE ClienteID = @ClienteID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarDetallePorFacturaID]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarDetallePorFacturaID]
    @FacturaID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[FACTURA_DETALLE]
    WHERE [FacturaID] = @FacturaID;
END;



GO
/****** Object:  StoredProcedure [dbo].[InsertarCliente]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarCliente]
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Email VARCHAR(100)
AS
BEGIN
    INSERT INTO CLIENTE (Nombre, Apellido, Email)
    VALUES (@Nombre, @Apellido, @Email);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertarDetalleFactura]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarDetalleFactura]
	@DetalleID INT,
    @FacturaID INT,
    @Descripcion VARCHAR(255),
    @Cantidad INT,
    @PrecioUnitario DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO FACTURA_DETALLE (DetalleID,FacturaID, Descripcion, Cantidad, PrecioUnitario)
    VALUES (@DetalleID,@FacturaID, @Descripcion, @Cantidad, @PrecioUnitario);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertarFactura]    Script Date: 06/12/2023 9:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarFactura]
    @ClienteID INT,
    @FechaEmision DATE,
    @Total DECIMAL(10, 2),
	@Descripcion VARCHAR(299)
AS
BEGIN
    INSERT INTO FACTURA_CABECERA (ClienteID, FechaEmision, Total,Descripcion)
    VALUES (@ClienteID, @FechaEmision, @Total,@Descripcion);
	SELECT SCOPE_IDENTITY();

END;
GO
