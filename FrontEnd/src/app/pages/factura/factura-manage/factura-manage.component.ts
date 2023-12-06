import { Component, Inject, OnInit } from '@angular/core';
import icClose from '@iconify/icons-ic/twotone-close';
import * as configs from '../../../../static-data/configs'
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AlertService } from '@shared/services/alert.service';
import { FacturaService } from 'src/app/services/factura.service';

@Component({
  selector: 'vex-factura-manage',
  templateUrl: './factura-manage.component.html',
  styleUrls: ['./factura-manage.component.scss']
})
export class FacturaManageComponent implements OnInit {

  icClose = icClose
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
      clienteID: [0, [Validators.required]],
      fechaEmision: ['', Validators.required],
      descripcion: [''],
      items: this.fb.array([{
        descripcion: ["string",[Validators.required]],
        cantidad: [0,[Validators.required]],
        precioUnitario: [0,[Validators.required]]
      }]) 
    })
  }
  get f() { return this.form.controls; }
    get t() { return this.f.items as FormArray; }
  get detalles(): FormGroup[] {
    return this.t.controls as FormGroup[];;
  }
  CategorySave(): void {
    if (this.form.invalid)
      return Object.values(this.form.controls).forEach(controls => controls.markAllAsTouched())
    const categoryId = this.form.get('categoryId').value
    if (categoryId > 0)
      this.facturaEdit(categoryId)
    else
      this.categoryRegister()

  }

  categoryRegister(): void {
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
  addItem(){

  }
}
