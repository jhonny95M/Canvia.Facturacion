export interface FacturaRequest{
    clienteID: number;
    fechaEmision: string;
    descripcion: string;
    items: FacturaRequestItem[];
}
interface FacturaRequestItem {
    descripcion: string;
    cantidad: number;
    precioUnitario: number;
}