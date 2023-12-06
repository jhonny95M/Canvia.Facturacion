import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CustomTitleService } from '@shared/services/custom-title.service';
import { fadeInRight400ms } from 'src/@vex/animations/fade-in-right.animation';
import { scaleFadeIn400ms } from 'src/@vex/animations/scale-fade-in.animation';
import { stagger40ms } from 'src/@vex/animations/stagger.animation';
import { FacturaService } from 'src/app/services/factura.service';
import { componentSettings } from './factura-list-config';
import { CommonApi } from 'src/app/responses/common/common.response';
import Swal from 'sweetalert2';
import { DatesFilter } from '@shared/functions/actions';
import { FacturaManageComponent } from '../factura-manage/factura-manage.component';

@Component({
  selector: 'vex-factura-list',
  templateUrl: './factura-list.component.html',
  styleUrls: ['./factura-list.component.scss'],
  animations: [
    stagger40ms,
    scaleFadeIn400ms,
    fadeInRight400ms
  ]
})
export class FacturaListComponent implements OnInit {
  component
  constructor(customTitle: CustomTitleService,
    public categoryService: FacturaService,
    public _dialog:MatDialog) {
      customTitle.set('Facturas')
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
      this._dialog.open(FacturaManageComponent,{
        disableClose:true,
        width:'1000px'
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
      // const dialogConfig=new MatDialogConfig()
      // dialogConfig.data=category
      // let dialogRef=this._dialog.open(CategoryManageComponent,{
      //   data:dialogConfig,
      //   disableClose:true,
      //   width:'400px'
      // })
      // dialogRef.afterClosed().subscribe(res=>{
      //   if(res)this.formatGetInputs()
      // })
    }
    categoryRemove(category: any) {
      // Swal.fire({
      //   title:`¿Realmente  deseas eliminar la categoria ${category.name}?`,
      //   text:'Se borrara de forma  permanente!',
      //   icon:'warning',
      //   showCancelButton:true,
      //   focusCancel:true,
      //   confirmButtonColor:'rgb(210,155,253)',
      //   cancelButtonColor:'rgb(79,109,253)',
      //   confirmButtonText:'Sí, eliminar',
      //   cancelButtonText:'Cancelar',
      //   width:430
      // }).then(result=>{
      //   if(result.isConfirmed){
      //     this.categoryService.CategoryRemove(category.categoryId).subscribe(()=>this.formatGetInputs())
      //   }
      // })
    }

}
