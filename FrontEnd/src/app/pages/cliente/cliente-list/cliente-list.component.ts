import { Component, OnInit } from '@angular/core';
import { CustomTitleService } from '@shared/services/custom-title.service';
import { fadeInRight400ms } from 'src/@vex/animations/fade-in-right.animation';
import { scaleFadeIn400ms } from 'src/@vex/animations/scale-fade-in.animation';
import { stagger40ms } from 'src/@vex/animations/stagger.animation';
import { ClienteService } from 'src/app/services/cliente.service';
import { componentSettings } from './cliente-list-config';
import { DatesFilter } from '@shared/functions/actions';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ClienteManageComponent } from '../cliente-manage/cliente-manage.component';
import Swal from 'sweetalert2';
import { CommonApi } from 'src/app/responses/common/common.response';

@Component({
  selector: 'vex-category-list',
  templateUrl: './cliente-list.component.html',
  styleUrls: ['./cliente-list.component.scss'],
  animations: [
    stagger40ms,
    scaleFadeIn400ms,
    fadeInRight400ms
  ]
})
export class ClienteListComponent implements OnInit {

  component
  constructor(
    customTitle: CustomTitleService,
    public clienteService: ClienteService,
    public _dialog:MatDialog
  ) {
    customTitle.set('Categorias')
  }

  ngOnInit(): void {
    this.component = componentSettings
  }
  
  setData(data: any = null) {
    this.component.filters.stateFilter = data.value
    this.component.menuOpen = false
    this.formatGetInputs()
  }
  search(data: any) {
    this.component.filters.numFilter = data.searchValue
    this.component.filters.textFilter = data.searchString
    this.formatGetInputs()
  }
  dateFilterOpen(){
    DatesFilter(this)
  }
  formatGetInputs() {
    let inputs = {
      numFilter: 0,
      textFilter: "",
      stateFilter: null,
      startDate: null,
      endDate: null
    }
    if(this.component.filters.numFilter!=""){
      inputs.numFilter=this.component.filters.numFilter
      inputs.textFilter=this.component.filters.textFilter
    }
    if (this.component.filters.stateFilter != null) {
      inputs.stateFilter = this.component.filters.stateFilter
    }
    if(this.component.filters.startDate!="" && this.component.filters.endDate!=""){
      inputs.startDate=this.component.filters.startDate
      inputs.endDate=this.component.filters.endDate
    }
    this.component.getInputs = inputs
  }
  openDialogRegister(){
    this._dialog.open(ClienteManageComponent,{
      disableClose:true,
      width:'400px'
    }).afterClosed().subscribe(res=>{
      if(res)this.formatGetInputs()
    })
  }
  rowClick(e: any) {
    let action = e.action
    let category = e.row
    switch (action) {
      case 'edit':
        this.categoryEdit(category)
        break;
      case 'remove':
        this.categoryRemove(category)
        break;
    }
  }
  categoryEdit(category: CommonApi) {
    const dialogConfig=new MatDialogConfig()
    dialogConfig.data=category
    let dialogRef=this._dialog.open(ClienteManageComponent,{
      data:dialogConfig,
      disableClose:true,
      width:'400px'
    })
    dialogRef.afterClosed().subscribe(res=>{
      if(res)this.formatGetInputs()
    })
  }
  categoryRemove(category: any) {
    Swal.fire({
      title:`¿Realmente  deseas eliminar la categoria ${category.name}?`,
      text:'Se borrara de forma  permanente!',
      icon:'warning',
      showCancelButton:true,
      focusCancel:true,
      confirmButtonColor:'rgb(210,155,253)',
      cancelButtonColor:'rgb(79,109,253)',
      confirmButtonText:'Sí, eliminar',
      cancelButtonText:'Cancelar',
      width:430
    }).then(result=>{
      if(result.isConfirmed){
        this.clienteService.CategoryRemove(category.categoryId).subscribe(()=>this.formatGetInputs())
      }
    })
  }
}
