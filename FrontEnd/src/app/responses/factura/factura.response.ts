export interface Factura{
    facturaId: number;
    clienteId:number;
    nombre:string;
    apellido:string;
    fechaEmision:Date;
    descripcion: string;
    total: number;
    auditCreateDate: Date;
    estado: number;
    estadoFactura?: string;
}