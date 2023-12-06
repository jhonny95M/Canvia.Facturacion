import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClienteListComponent } from './cliente-list/cliente-list.component';

const routes: Routes = [
  {
    path:'',
    component:ClienteListComponent,
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
export class ClienteRoutingModule { }
