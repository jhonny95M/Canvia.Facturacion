import { Component, Inject, OnInit } from '@angular/core';
import icClose from '@iconify/icons-ic/twotone-close';
import * as configs from '../../../../static-data/configs'
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AlertService } from '@shared/services/alert.service';
import { FacturaService } from 'src/app/services/factura.service';
import icDelete from '@iconify/icons-ic/round-delete';


@Component({
  selector: 'vex-factura-manage',
  templateUrl: './factura-manage.component.html',
  styleUrls: ['./factura-manage.component.scss']
})
export class FacturaManageComponent implements OnInit {

  icClose = icClose
  icDelete = icDelete
  configs = configs
  form: FormGroup
  constructor(@Inject(MAT_DIALOG_DATA) public data,
    private fb: FormBuilder,
    private alert: AlertService,
    private facturaService: FacturaService,
    public dialogRef: MatDialogRef<FacturaManageComponent>) {
    this.initForm()

  }

  ngOnInit(): void {
    if (this.data != null) {
      console.log(this.data)
      this.CategoryById(this.data.data.FacturaId)
    }
  }
  CategoryById(categoryId: number): void {
    this.facturaService.FacturaById(categoryId).subscribe(
      (res) => {
        this.form.reset({
          facturaID:res.facturaID,
          clienteID: res.clienteID,
          fechaEmision: res.fechaEmision,
          descripcion: res.descripcion,
          items: res.items
        })
      }
    )
  }
  initForm(): void {

    this.form = this.fb.group({
      facturaID:[0],
      clienteID: [0, [Validators.required]],
      fechaEmision: ['', Validators.required],
      descripcion: [''],
      items: new FormArray([])
    })
  }
  get f() { return this.form.controls; }
  get t() { return this.f.items as FormArray; }
  get detalles(): FormGroup[] {
    return this.t.controls as FormGroup[];;
  }
  facturaSave(): void {
    console.log(this.form.value)
    if (this.form.invalid)
      return Object.values(this.form.controls).forEach(controls => controls.markAllAsTouched())
    const facturaId = this.form.get('facturaID').value
    if (facturaId > 0)
      this.facturaEdit(facturaId)
    else
      this.facturaRegister()

  }

  facturaRegister(): void {
    this.facturaService.CategoryRegister(this.form.value).subscribe(resp => {
      if (resp.isSucces) {
        this.alert.success('Excelente', resp.message)
        this.dialogRef.close(true)
      } else {
        this.alert.warn('Atencion', resp.message)
      }
    })
  }
  facturaEdit(id: number) {
    this.facturaService.FacturaEdit(id, this.form.value).subscribe((resp => {
      if (resp.isSucces) {
        this.alert.success('Excelente', resp.message)
        this.dialogRef.close(true)
      } else
        this.alert.warn('Atencion', resp.message)
    }))
  }
  addItem() {

    this.t.push(this.fb.group({
      descripcion: ['', [Validators.required]],
      cantidad: [1, [Validators.required, Validators.pattern(/^-?\d+$/)]],
      precioUnitario: ['0', [Validators.required]]
    }));
  }
  removeItem(indice: number) {
    this.t.removeAt(indice);
  }
  resetearItems() {
    this.t.reset();
  }
  validarNumeroEntero(indice: number, event: any) {
    const input = event.target as HTMLInputElement;
    const valor = input.value;

    // Eliminar cualquier caracter no numérico o punto decimal
    const valorSinDecimal = valor.replace(/[^0-9]/g, '');

    // Asignar el valor ajustado de vuelta al input
    input.value = valorSinDecimal;
    var valores = this.detalles[indice].value as DetalleFactura
    valores.cantidad = parseInt(valorSinDecimal)
    // Actualizar el valor del FormControl
    this.detalles[indice].setValue(valores);
  }
}
