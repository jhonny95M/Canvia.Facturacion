import { Component, Inject, OnInit } from '@angular/core';
import icClose from '@iconify/icons-ic/twotone-close';
import * as configs from '../../../../static-data/configs'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AlertService } from '@shared/services/alert.service';
import { ClienteService } from 'src/app/services/cliente.service';

@Component({
  selector: 'vex-category-manage',
  templateUrl: './cliente-manage.component.html',
  styleUrls: ['./cliente-manage.component.scss']
})
export class ClienteManageComponent implements OnInit {

  icClose = icClose
  configs = configs
  form: FormGroup
  constructor(@Inject(MAT_DIALOG_DATA) public data,
    private fb: FormBuilder,
    private alert: AlertService,
    private categoryService: ClienteService,
    public dialogRef: MatDialogRef<ClienteManageComponent>
  ) { 
    this.initForm()
  }

  ngOnInit(): void {
    if(this.data!=null){
      console.log(this.data)
      this.CategoryById(this.data.data.clienteID)
    }
  }
  CategoryById(categoryId: number):void {
    this.categoryService.CategoryById(categoryId).subscribe(
      (res)=>{
        this.form.reset({
          clienteID:res.clienteID,
          nombre:res.nombre,
          apellido:res.apellido
        })
      }
    )
  }
  initForm(): void {

    this.form = this.fb.group({
      categoryId: [0, [Validators.required]],
      name: ['', Validators.required],
      description: [''],
      state: ['', [Validators.required]]
    })
  }
  CategorySave():void{
    if(this.form.invalid)
      return Object.values(this.form.controls).forEach(controls=>controls.markAllAsTouched())
      const categoryId=this.form.get('categoryId').value
      if(categoryId>0)
        this.categoryEdit(categoryId)
      else
        this.categoryRegister()
      
    }
  
  categoryRegister():void {
    this.categoryService.CategoryRegister(this.form.value).subscribe(resp=>{
      if(resp.isSucces){
        this.alert.success('Excelente',resp.message)
        this.dialogRef.close(true)
      }else{
        this.alert.warn('Atencion',resp.message)
      }
    })
  }
  categoryEdit(categoryId: number) {
    this.categoryService.CategoryEdit(categoryId,this.form.value).subscribe((resp=>{
      if(resp.isSucces){
      this.alert.success('Excelente',resp.message)
      this.dialogRef.close(true)
      }else
      this.alert.warn('Atencion',resp.message)
    }))
  }

}
