import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FacturaListComponent } from './factura-list/factura-list.component';

const routes: Routes = [
  {
    path:'',
    component:FacturaListComponent,
    data:{
      scrollDisabled:true,
      toolbarShadowEnabled:true
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FacturaRoutingModule { }
