interface FacturaDetallada {
    facturaID: number;
    clienteID: number;
    fechaEmision: string;
    descripcion: string;
    total: number;
    estado: number;
    items: DetalleFactura[];
}

interface DetalleFactura {
    detalleID: number;
    descripcion: string;
    cantidad: number;
    precioUnitario: number;
}
